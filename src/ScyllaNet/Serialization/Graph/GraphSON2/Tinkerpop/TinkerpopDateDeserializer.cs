// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Tinkerpop
{
    internal class TinkerpopDateDeserializer : IGraphSONDeserializer
    {
        private const string Prefix = "g";
        private const string TypeKey = "Date";
        
        private static readonly DateTimeOffset UnixStart = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
        
        public static string TypeName =>
            GraphSONUtil.FormatTypeName(TinkerpopDateDeserializer.Prefix, TinkerpopDateDeserializer.TypeKey);

        public dynamic Objectify(JToken graphsonObject, IGraphSONReader reader)
        {
            var milliseconds = graphsonObject.ToObject<long>();
            return TinkerpopDateDeserializer.UnixStart.AddTicks(TimeSpan.TicksPerMillisecond * milliseconds);
        }
    }
}
