// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Globalization;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Tinkerpop
{
    internal class TimestampSerializer : StringBasedSerializer
    {
        private const string Prefix = "gx";
        private const string TypeKey = "Instant";

        private const string FormatString = "yyyy-MM-ddTHH:mm:ss.fffZ";

        public TimestampSerializer() : base(TimestampSerializer.Prefix, TimestampSerializer.TypeKey)
        {
        }

        public static string TypeName => GraphSONUtil.FormatTypeName(TimestampSerializer.Prefix, TimestampSerializer.TypeKey);

        protected override string ToString(dynamic obj)
        {
            DateTimeOffset dateTimeOffset = obj;
            var ticks = (dateTimeOffset - TypeSerializer.UnixStart).Ticks;
            var instant = TypeSerializer.UnixStart.AddTicks(ticks);
            return instant.ToString(TimestampSerializer.FormatString, CultureInfo.InvariantCulture);
        }

        protected override dynamic FromString(string str)
        {
            return DateTimeOffset.Parse(str, CultureInfo.InvariantCulture);
        }
    }
}
