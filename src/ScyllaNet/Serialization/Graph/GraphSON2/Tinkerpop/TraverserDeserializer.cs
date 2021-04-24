// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.GraphSON2.Structure;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Tinkerpop
{
    internal class TraverserDeserializer : BaseStructureDeserializer, IGraphSONStructureDeserializer
    {
        private const string Prefix = "g";
        private const string TypeKey = "Traverser";
        
        public static string TypeName => 
            GraphSONUtil.FormatTypeName(TraverserDeserializer.Prefix, TraverserDeserializer.TypeKey);

        public dynamic Objectify(JToken graphsonObject, Func<JToken, GraphNode> factory, IGraphSONReader reader)
        {
            long bulkObj = reader.ToObject(graphsonObject["bulk"]);
            var valueObj = ToGraphNode(factory, graphsonObject, "value");
            return new Traverser(valueObj, bulkObj);
        }
    }
}
