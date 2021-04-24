// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.GraphSON2;
using Scylla.Net.Serialization.Graph.GraphSON2.Structure;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.GraphSON3.Structure
{
    internal class Path3Deserializer : BaseStructureDeserializer, IGraphSONStructureDeserializer
    {
        private const string Prefix = "g";
        private const string TypeKey = "Path";
        
        public static string TypeName => 
            GraphSONUtil.FormatTypeName(Path3Deserializer.Prefix, Path3Deserializer.TypeKey);

        public dynamic Objectify(JToken graphsonObject, Func<JToken, GraphNode> factory, IGraphSONReader reader)
        {
            ICollection<ICollection<string>> labels = null;
            ICollection<GraphNode> objects = null;

            if (graphsonObject is JObject jObj)
            {
                labels = ParseLabels(jObj);
                objects = ParseObjects(jObj, factory);
            }
            
            return new Path(labels, objects);
        }

        private ICollection<ICollection<string>> ParseLabels(JObject tokenObj)
        {
            if (tokenObj["labels"] is JObject labelsObj 
                && labelsObj[GraphTypeSerializer.ValueKey] is JArray labelsArray)
            {
                return labelsArray
                       .Select(node =>
                       {
                          if (node is JObject nodeObj
                              && nodeObj[GraphTypeSerializer.ValueKey] is JArray nodeArray)
                          {
                              return new HashSet<string>(nodeArray.Select(n => n.ToString()));
                          }

                          throw new InvalidOperationException($"Cannot create a Path from {tokenObj}");
                      })
                      .ToArray();
            }

            return null;
        }

        private ICollection<GraphNode> ParseObjects(JObject tokenObj, Func<JToken, GraphNode> factory)
        {
            if (tokenObj["objects"] is JObject objectsObj 
                && objectsObj[GraphTypeSerializer.ValueKey] is JArray objectsArray)
            {
                return objectsArray.Select(jt => ToGraphNode(factory, jt)).ToArray();
            }

            return null;
        }
    }
}
