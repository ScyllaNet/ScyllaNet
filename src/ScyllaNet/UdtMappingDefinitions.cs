﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Scylla.Net.Serialization;
using Scylla.Net.SessionManagement;
using Scylla.Net.Tasks;

namespace Scylla.Net
{
    /// <summary>
    /// Allows configuration of user defined types.
    /// </summary>
    public class UdtMappingDefinitions
    {
        private readonly ConcurrentDictionary<Type, UdtMap> _udtByNetType;
        private readonly IInternalCluster _cluster;
        private readonly IInternalSession _session;
        private readonly ISerializerManager _serializer;

        internal UdtMappingDefinitions(IInternalSession session, ISerializerManager serializer)
        {
            _udtByNetType = new ConcurrentDictionary<Type, UdtMap>();
            _cluster = session.InternalCluster;
            _session = session;
            _serializer = serializer;
        }

        /// <summary>
        /// Add mapping definition(s) for UDTs, specifying how UDTs should be mapped to .NET types and vice versa.
        /// </summary>
        /// <exception cref="ArgumentException" />
        public void Define(params UdtMap[] udtMaps)
        {
            TaskHelper.WaitToComplete(DefineAsync(udtMaps), _cluster.Configuration.DefaultRequestOptions.QueryAbortTimeout);
        }

        /// <summary>
        /// Add mapping definition(s) for UDTs, specifying how UDTs should be mapped to .NET types and vice versa.
        /// </summary>
        /// <exception cref="ArgumentException" />
        public async Task DefineAsync(params UdtMap[] udtMaps)
        {
            if (udtMaps == null)
            {
                throw new ArgumentNullException("udtMaps");
            }
            var sessionKeyspace = _session.Keyspace;
            if (string.IsNullOrEmpty(sessionKeyspace) && udtMaps.Any(map => map.Keyspace == null))
            {
                throw new ArgumentException("It is not possible to define a mapping when no keyspace is specified. " +
                                            "You can specify it while creating the UdtMap, while creating the Session and" +
                                            " while creating the Cluster (default keyspace config setting).");
            }
            if (_session.BinaryProtocolVersion < 3)
            {
                throw new NotSupportedException("User defined type mapping is supported with C* 2.1+ and protocol version 3+");
            }
            // Add types to both indexes
            foreach (var map in udtMaps)
            {
                var udtDefition = await GetDefinitionAsync(map.Keyspace ?? sessionKeyspace, map).ConfigureAwait(false);
                map.SetSerializer(_serializer.GetCurrentSerializer());
                map.Build(udtDefition);
                _serializer.SetUdtMap(udtDefition.Name, map);
                _udtByNetType.AddOrUpdate(map.NetType, map, (k, oldValue) => map);
            }
        }

        /// <summary>
        /// Gets the definition and validates the fields
        /// </summary>
        /// <exception cref="InvalidTypeException" />
        private async Task<UdtColumnInfo> GetDefinitionAsync(string keyspace, UdtMap map)
        {
            var caseSensitiveUdtName = map.UdtName;
            if (map.IgnoreCase)
            {
                //identifiers are lower cased in Cassandra
                caseSensitiveUdtName = caseSensitiveUdtName.ToLowerInvariant();
            }
            var udtDefinition = await _cluster.Metadata.GetUdtDefinitionAsync(keyspace, caseSensitiveUdtName).ConfigureAwait(false);
            if (udtDefinition == null)
            {
                throw new InvalidTypeException($"{caseSensitiveUdtName} UDT not found on keyspace {keyspace}");
            }
            return udtDefinition;
        }

        internal UdtMap GetUdtMap<T>(string keyspace)
        {
            return GetUdtMap(keyspace, typeof(T));
        }

        internal UdtMap GetUdtMap(string keyspace, Type netType)
        {
            return _udtByNetType.TryGetValue(netType, out UdtMap map) ? map : null;
        }
    }
}
