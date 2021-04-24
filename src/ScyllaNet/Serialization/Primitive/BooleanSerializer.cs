// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Serialization.Primitive
{
    internal class BooleanSerializer : TypeSerializer<bool>
    {
        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Boolean; }
        }

        public override bool Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            return buffer[offset] == 1;
        }

        public override byte[] Serialize(ushort protocolVersion, bool value)
        {
            return new [] { (byte)(value ? 1 : 0) };
        }
    }
}
