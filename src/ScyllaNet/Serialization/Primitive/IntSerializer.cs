// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Serialization.Primitive
{
    internal class IntSerializer : TypeSerializer<int>
    {
        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Int; }
        }

        public override int Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            return BeConverter.ToInt32(buffer, offset);
        }

        public override byte[] Serialize(ushort protocolVersion, int value)
        {
            return BeConverter.GetBytes(value);
        }
    }
}
