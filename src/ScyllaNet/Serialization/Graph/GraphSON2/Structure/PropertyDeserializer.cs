// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Structure
{
    internal class PropertyDeserializer : BaseStructureDeserializer, IGraphSONStructureDeserializer
    {
        private const string Prefix = "g";
        private const string TypeKey = "Property";
        
        public static string TypeName => 
            GraphSONUtil.FormatTypeName(PropertyDeserializer.Prefix, PropertyDeserializer.TypeKey);

        public dynamic Objectify(JToken token, Func<JToken, GraphNode> factory, IGraphSONReader reader)
        {
            return new Property(
                ToString(token, "key", true),
                ToGraphNode(factory, token, "value"),
                ToGraphNode(factory, token, "element"));
        }
    }
}
