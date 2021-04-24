// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Graph.Internal;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    internal class ListSerializer : IGraphSONDeserializer, IGraphSONSerializer
    {
        private static readonly IReadOnlyList<object> EmptyList = new object[0];
        
        public dynamic Objectify(JToken graphsonObject, IGraphSONReader reader)
        {
            var jArray = graphsonObject as JArray;
            if (jArray == null)
            {
                return ListSerializer.EmptyList;
            }
            var result = new object[jArray.Count];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = reader.ToObject(jArray[i]);
            }
            // object[] implements IList<object>
            return result;
        }

        public Dictionary<string, dynamic> Dictify(dynamic objectData, IGraphSONWriter writer)
        {
            return GraphSONUtil.ToCollection(objectData, writer, "List");
        }
    }
}
