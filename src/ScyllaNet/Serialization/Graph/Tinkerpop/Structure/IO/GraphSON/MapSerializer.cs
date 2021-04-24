// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using Scylla.Net.DataStax.Graph.Internal;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    internal class MapSerializer : IGraphSONDeserializer, IGraphSONSerializer
    {
        public dynamic Objectify(JToken graphsonObject, IGraphSONReader reader)
        {
            var jArray = graphsonObject as JArray;
            if (jArray == null)
            {
                return new Dictionary<object, object>(0);
            }
            var result = new Dictionary<object, object>(jArray.Count / 2);
            for (var i = 0; i < jArray.Count; i += 2)
            {
                result[reader.ToObject(jArray[i])] = reader.ToObject(jArray[i + 1]);
            }
            // IDictionary<object, object>
            return result;
        }
        
        public Dictionary<string, dynamic> Dictify(dynamic objectData, IGraphSONWriter writer)
        {
            var map = objectData as IDictionary;
            if (map == null)
            {
                throw new InvalidOperationException("Object must implement IDictionary");
            }
            var result = new object[map.Count * 2];
            var index = 0;
            foreach (var key in map.Keys)
            {
                result[index++] = writer.ToDict(key);
                result[index++] = writer.ToDict(map[key]);
            }
            return GraphSONUtil.ToTypedValue("Map", result);
        }
    }
}
