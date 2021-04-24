// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Numerics;

namespace Scylla.Net.Serialization.Primitive
{
    /// <summary>
    /// A serializer for CQL type varint, CLR type BigInteger.
    /// </summary>
    internal class BigIntegerSerializer : TypeSerializer<BigInteger>
    {
        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Varint; }
        }

        public override BigInteger Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            buffer = Utils.SliceBuffer(buffer, offset, length);
            //Cassandra uses big endian encoding
            Array.Reverse(buffer);
            return new BigInteger(buffer);
        }

        public override byte[] Serialize(ushort protocolVersion, BigInteger value)
        {
            var buffer = value.ToByteArray();
            //Cassandra expects big endian encoding
            Array.Reverse(buffer);
            return buffer;
        }
    }
}
