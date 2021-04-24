// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Serialization.Primitive
{
    internal class LocalTimeSerializer : TypeSerializer<LocalTime>
    {
        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Time; }
        }

        public override LocalTime Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            return new LocalTime(BeConverter.ToInt64(buffer, offset));
        }

        public override byte[] Serialize(ushort protocolVersion, LocalTime value)
        {
            return BeConverter.GetBytes(value.TotalNanoseconds);
        }
    }
}
