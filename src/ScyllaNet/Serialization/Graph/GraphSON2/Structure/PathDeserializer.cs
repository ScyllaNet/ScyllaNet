// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Structure
{
    internal class PathDeserializer : BaseStructureDeserializer, IGraphSONStructureDeserializer
    {
        private const string Prefix = "g";
        private const string TypeKey = "Path";
        
        public static string TypeName => 
            GraphSONUtil.FormatTypeName(PathDeserializer.Prefix, PathDeserializer.TypeKey);

        public dynamic Objectify(JToken token, Func<JToken, GraphNode> factory, IGraphSONReader reader)
        {
            ICollection<ICollection<string>> labels = null;
            ICollection<GraphNode> objects = null;
            if (token["labels"] is JArray labelsProp)
            {
                // labels prop is a js Array<Array<string>>
                labels = labelsProp
                         .Select(node =>
                         {
                             var arrayNode = node as JArray;
                             if (arrayNode == null)
                             {
                                 throw new InvalidOperationException($"Cannot create a Path from {token}");
                             }
                             return new HashSet<string>(arrayNode.Select(n => n.ToString()));
                         })
                         .ToArray();
            }

            if (token["objects"] is JArray objectsProp)
            {
                // labels prop is a js Array<object>
                objects = objectsProp.Select(o => ToGraphNode(factory, o)).ToArray();
            }
            return new Path(labels, objects);
        }
    }
}
