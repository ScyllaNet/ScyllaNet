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
    internal class VertexDeserializer : BaseStructureDeserializer, IGraphSONStructureDeserializer
    {
        private const string Prefix = "g";
        private const string TypeKey = "Vertex";

        public const string DefaultLabel = "vertex";

        public static string TypeName => GraphSONUtil.FormatTypeName(VertexDeserializer.Prefix, VertexDeserializer.TypeKey);

        public dynamic Objectify(JToken token, Func<JToken, GraphNode> factory, IGraphSONReader reader)
        {
            IDictionary<string, GraphNode> properties = null;
            var tokenProperties = !(token is JObject jobj) ? null : jobj["properties"];
            if (tokenProperties != null && tokenProperties is JObject propertiesJsonProp)
            {
                properties = propertiesJsonProp
                             .Properties()
                             .ToDictionary(prop => prop.Name, prop => ToGraphNode(factory, prop.Value));
            }

            var label = ToString(token, "label", false) ?? VertexDeserializer.DefaultLabel;
            return new Vertex(
                ToGraphNode(factory, token, "id", true),
                label,
                properties ?? new Dictionary<string, GraphNode>(0));
        }
    }
}
