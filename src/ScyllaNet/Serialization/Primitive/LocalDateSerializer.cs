// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Serialization.Primitive
{
    internal class LocalDateSerializer : TypeSerializer<LocalDate>
    {
        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Date; }
        }

        public override LocalDate Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            var days = unchecked((uint)((buffer[offset] << 24)
                   | (buffer[offset + 1] << 16)
                   | (buffer[offset + 2] << 8)
                   | (buffer[offset + 3])));
            return new LocalDate(days);
        }

        public override byte[] Serialize(ushort protocolVersion, LocalDate value)
        {
            var val = value.DaysSinceEpochCentered;
            return new[]
            {
                (byte) ((val & 0xFF000000) >> 24),
                (byte) ((val & 0xFF0000) >> 16),
                (byte) ((val & 0xFF00) >> 8),
                (byte) (val & 0xFF)
            };
        }
    }
}
