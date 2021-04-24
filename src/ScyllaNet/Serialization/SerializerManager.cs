// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.Serialization
{
    /// <summary>
    /// Handles types serialization from binary form to objects and the other way around.
    /// The instance is aware of protocol version, custom codecs, UDT mappers
    /// </summary>
    internal class SerializerManager : ISerializerManager
    {
        internal static readonly ISerializerManager Default = new SerializerManager(ProtocolVersion.V1);

        /// <summary>
        /// An instance of a buffer that represents the value Unset
        /// </summary>
        internal static readonly byte[] UnsetBuffer = new byte[0];

        private readonly GenericSerializer _genericSerializer;
        private volatile ISerializer _serializer;

        internal SerializerManager(ProtocolVersion protocolVersion, IEnumerable<ITypeSerializer> typeSerializers = null)
        {
            _genericSerializer = new GenericSerializer(typeSerializers);
            _serializer = new Serializer(protocolVersion, _genericSerializer);
        }

        public ProtocolVersion CurrentProtocolVersion => _serializer.ProtocolVersion;

        public void ChangeProtocolVersion(ProtocolVersion version)
        {
            _serializer = new Serializer(version, _genericSerializer);
        }

        public ISerializer GetCurrentSerializer()
        {
            return _serializer;
        }

        public object Deserialize(ProtocolVersion version, byte[] buffer, int offset, int length, ColumnTypeCode typeCode, IColumnInfo typeInfo)
        {
            return _genericSerializer.Deserialize(version, buffer, offset, length, typeCode, typeInfo);
        }

        public byte[] Serialize(ProtocolVersion version, object value)
        {
            return _genericSerializer.Serialize(version, value);
        }
        
        public void SetUdtMap(string name, UdtMap map)
        {
            _genericSerializer.SetUdtMap(name, map);
        }

        public IGenericSerializer GetGenericSerializer()
        {
            return _genericSerializer;
        }
    }
}
