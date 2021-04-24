// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Serialization.Primitive
{
    /// <summary>
    /// A serializer for CQL type bigint, CLR type Int64.
    /// </summary>
    internal class LongSerializer : TypeSerializer<long>
    {
        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Bigint; }
        }

        public override long Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            return BeConverter.ToInt64(buffer, offset);
        }

        public override byte[] Serialize(ushort protocolVersion, long value)
        {
            return BeConverter.GetBytes(value);
        }
    }
}
