﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Scylla.Net.MetadataHelpers
{
    internal class SimpleStrategy : IReplicationStrategy, IEquatable<SimpleStrategy>
    {
        private readonly ReplicationFactor _replicationFactor;

        public SimpleStrategy(ReplicationFactor replicationFactor)
        {
            _replicationFactor = replicationFactor;
        }

        public Dictionary<IToken, ISet<Host>> ComputeTokenToReplicaMap(
            IReadOnlyList<IToken> ring, 
            IReadOnlyDictionary<IToken, Host> primaryReplicas,
            int numberOfHostsWithTokens,
            IReadOnlyDictionary<string, DatacenterInfo> datacenters)
        {
            return ComputeTokenToReplicaSimple(numberOfHostsWithTokens, ring, primaryReplicas);
        }

        public bool Equals(IReplicationStrategy other)
        {
            return Equals(other as SimpleStrategy);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SimpleStrategy);
        }

        public bool Equals(SimpleStrategy other)
        {
            return other != null 
                   && _replicationFactor.Equals(other._replicationFactor);
        }

        public override int GetHashCode()
        {
            return _replicationFactor.GetHashCode();
        }

        /// <summary>
        /// Converts token-primary to token-replicas
        /// </summary>
        private Dictionary<IToken, ISet<Host>> ComputeTokenToReplicaSimple(
            int numberOfHostsWithTokens, 
            IReadOnlyList<IToken> ring, 
            IReadOnlyDictionary<IToken, Host> primaryReplicas)
        {
            var rf = Math.Min(_replicationFactor.FullReplicas, numberOfHostsWithTokens);
            var tokenToReplicas = new Dictionary<IToken, ISet<Host>>(ring.Count);
            for (var i = 0; i < ring.Count; i++)
            {
                var token = ring[i];
                var replicas = new HashSet<Host>();
                for (var j = 0; replicas.Count < rf && j < ring.Count; j++)
                {
                    // circle back if necessary
                    var nextReplicaIndex = (i + j) % ring.Count;
                    var nextReplica = primaryReplicas[ring[nextReplicaIndex]];
                    
                    // not necessary to check if already added this replica,
                    // because it's an HashSet and Equals + GetHashCode are overriden in Host class
                    replicas.Add(nextReplica);
                }
               
                tokenToReplicas.Add(token, replicas);
            }
            return tokenToReplicas;
        }
    }
}
