// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.GraphSON3.Dse
{
    internal class Duration3Serializer : IGraphSONSerializer, IGraphSONDeserializer
    {
        private const string Prefix = "dse";
        private const string TypeKey = "Duration";

        public static string TypeName => GraphSONUtil.FormatTypeName(Duration3Serializer.Prefix, Duration3Serializer.TypeKey);

        public Dictionary<string, dynamic> Dictify(dynamic objectData, IGraphSONWriter writer)
        {
            Duration d = objectData;
            var value = new Dictionary<string, dynamic>
            {
                { "months", writer.ToDict(d.Months) },
                { "days", writer.ToDict(d.Days) },
                { "nanos", writer.ToDict(d.Nanoseconds) }
            };
            return GraphSONUtil.ToTypedValue(
                Duration3Serializer.TypeKey, 
                value, 
                Duration3Serializer.Prefix);
        }

        public dynamic Objectify(JToken graphsonObject, IGraphSONReader reader)
        {
            var months = (int) reader.ToObject(graphsonObject["months"]);
            var days = (int) reader.ToObject(graphsonObject["days"]);
            var nanos = (long) reader.ToObject(graphsonObject["nanos"]);
            return new Duration(months, days, nanos);
        }
    }
}
