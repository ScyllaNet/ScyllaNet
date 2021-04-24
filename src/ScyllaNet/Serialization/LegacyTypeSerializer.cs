// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Serialization
{
    /// <summary>
    /// Legacy <see cref="ITypeSerializer"/> to support <see cref="ITypeAdapter"/>.
    /// </summary>
    internal class LegacyTypeSerializer : ITypeSerializer
    {
        private readonly ColumnTypeCode _typeCode;
        private readonly ITypeAdapter _adapter;
        private readonly bool _reverse;

        public Type Type
        {
            get { return _adapter.GetDataType(); }
        }

        public IColumnInfo TypeInfo
        {
            get { return null; }
        }

        public ColumnTypeCode CqlType 
        {
            get { return _typeCode; }
        }

        internal LegacyTypeSerializer(ColumnTypeCode typeCode, ITypeAdapter adapter, bool reverse)
        {
            _typeCode = typeCode;
            _adapter = adapter;
            _reverse = reverse;
        }

        public object Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            buffer = Utils.SliceBuffer(buffer, offset, length);
            if (_reverse)
            {
                Array.Reverse(buffer);   
            }
            return _adapter.ConvertFrom(buffer);
        }

        public byte[] Serialize(ushort protocolVersion, object obj)
        {
            var buffer = _adapter.ConvertTo(obj);
            if (_reverse)
            {
                Array.Reverse(buffer);
            }
            return buffer;
        }
    }
}
