// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Scylla.Net.DataStax.Graph.Internal;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    internal class SetSerializer : IGraphSONDeserializer, IGraphSONSerializer
    {
        public dynamic Objectify(JToken graphsonObject, IGraphSONReader reader)
        {
            var jArray = graphsonObject as JArray;
            if (jArray == null)
            {
                return new HashSet<object>();
            }
            // ISet<object>
            return new HashSet<object>(jArray.Select(reader.ToObject));
        }

        public Dictionary<string, dynamic> Dictify(dynamic objectData, IGraphSONWriter writer)
        {
            return GraphSONUtil.ToCollection(objectData, writer, "Set");
        }
    }
}
