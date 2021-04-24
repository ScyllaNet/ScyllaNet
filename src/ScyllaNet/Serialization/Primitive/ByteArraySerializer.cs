// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Serialization.Primitive
{
    internal class ByteArraySerializer : TypeSerializer<byte[]>
    {
        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Blob; }
        }

        public override byte[] Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            return Utils.FromOffset(buffer, offset, length);
        }

        public override byte[] Serialize(ushort protocolVersion, byte[] value)
        {
            return value;
        }
    }
}
