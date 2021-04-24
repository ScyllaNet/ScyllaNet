// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Serialization
{
    /// <inheritdoc />
    internal class Serializer : ISerializer
    {
        private readonly IGenericSerializer _serializer;
        
        public Serializer(ProtocolVersion version, IGenericSerializer serializer)
        {
            ProtocolVersion = version;
            _serializer = serializer;
        }

        public ProtocolVersion ProtocolVersion { get; }

        public object Deserialize(byte[] buffer, int offset, int length, ColumnTypeCode typeCode, IColumnInfo typeInfo)
        {
            return _serializer.Deserialize(ProtocolVersion, buffer, offset, length, typeCode, typeInfo);
        }

        public byte[] Serialize(object value)
        {
            return _serializer.Serialize(ProtocolVersion, value);
        }

        public ISerializer CloneWithProtocolVersion(ProtocolVersion version)
        {
            return new Serializer(version, _serializer);
        }

        public object Deserialize(ProtocolVersion version, byte[] buffer, int offset, int length, ColumnTypeCode typeCode, IColumnInfo typeInfo)
        {
            return _serializer.Deserialize(version, buffer, offset, length, typeCode, typeInfo);
        }

        public byte[] Serialize(ProtocolVersion version, object value)
        {
            return _serializer.Serialize(version, value);
        }

        public Type GetClrType(ColumnTypeCode typeCode, IColumnInfo typeInfo)
        {
            return _serializer.GetClrType(typeCode, typeInfo);
        }

        public Type GetClrTypeForGraph(ColumnTypeCode typeCode, IColumnInfo typeInfo)
        {
            return _serializer.GetClrTypeForGraph(typeCode, typeInfo);
        }

        public Type GetClrTypeForCustom(IColumnInfo typeInfo)
        {
            return _serializer.GetClrTypeForCustom(typeInfo);
        }

        public ColumnTypeCode GetCqlType(Type type, out IColumnInfo typeInfo)
        {
            return _serializer.GetCqlType(type, out typeInfo);
        }

        public bool IsAssignableFrom(CqlColumn column, object value)
        {
            return _serializer.IsAssignableFrom(column, value);
        }

        public UdtMap GetUdtMapByName(string name)
        {
            return _serializer.GetUdtMapByName(name);
        }

        public UdtMap GetUdtMapByType(Type type)
        {
            return _serializer.GetUdtMapByType(type);
        }
    }
}
