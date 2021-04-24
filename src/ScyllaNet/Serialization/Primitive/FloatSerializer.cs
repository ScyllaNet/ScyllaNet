// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Serialization.Primitive
{
    internal class FloatSerializer : TypeSerializer<float>
    {
        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Float; }
        }

        public override float Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            return BeConverter.ToSingle(buffer, offset);
        }

        public override byte[] Serialize(ushort protocolVersion, float value)
        {
            return BeConverter.GetBytes(value);
        }
    }
}
