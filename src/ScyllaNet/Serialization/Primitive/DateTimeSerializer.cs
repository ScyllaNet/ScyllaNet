// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Serialization.Primitive
{
    internal class DateTimeSerializer : TypeSerializer<DateTime>
    {
        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Timestamp; }
        }

        public override DateTime Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            var dto = DateTimeOffsetSerializer.Deserialize(buffer, offset);
            return dto.DateTime;
        }

        public override byte[] Serialize(ushort protocolVersion, DateTime value)
        {
            // Treat "Unspecified" as UTC (+0) not the default behavior of DateTimeOffset which treats as Local Timezone
            // because we are about to do math against EPOCH which must align with UTC.
            var dateTimeOffset = value.Kind == DateTimeKind.Unspecified
                ? new DateTimeOffset(value, TimeSpan.Zero)
                : new DateTimeOffset(value);
            return DateTimeOffsetSerializer.Serialize(dateTimeOffset);
        }
    }
}
