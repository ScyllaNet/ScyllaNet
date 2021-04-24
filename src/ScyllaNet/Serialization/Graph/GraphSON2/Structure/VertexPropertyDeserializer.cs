// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Structure
{
    internal class VertexPropertyDeserializer : BaseStructureDeserializer, IGraphSONStructureDeserializer
    {
        private const string Prefix = "g";
        private const string TypeKey = "VertexProperty";
        
        public static string TypeName => 
            GraphSONUtil.FormatTypeName(VertexPropertyDeserializer.Prefix, VertexPropertyDeserializer.TypeKey);

        public dynamic Objectify(JToken token, Func<JToken, GraphNode> factory, IGraphSONReader reader)
        {
            var graphNode = ToGraphNode(factory, token);

            return new VertexProperty(
                graphNode.Get<GraphNode>("id", true), 
                graphNode.Get<string>("label"),
                graphNode.Get<GraphNode>("value", true),
                graphNode.Get<GraphNode>("vertex"),
                new Dictionary<string, GraphNode>(0));
        }
    }
}
