// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Serialization.Primitive
{
    internal class SbyteSerializer : TypeSerializer<sbyte>
    {
        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.TinyInt; }
        }

        public override sbyte Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            return unchecked((sbyte)buffer[offset]);
        }

        public override byte[] Serialize(ushort protocolVersion, sbyte value)
        {
            return new[] { unchecked((byte)value) };
        }
    }
}
