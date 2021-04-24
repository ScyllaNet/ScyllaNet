// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Structure
{
    internal class VertexSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, IGraphSONWriter writer)
        {
            Vertex vertex = objectData;
            var vertexDict = new Dictionary<string, dynamic>
            {
                {"id", writer.ToDict(vertex.Id)},
                {"label", writer.ToDict(vertex.Label)}
            };

            if (vertex.Properties != null && vertex.Properties.Count > 0)
            {
                vertexDict["properties"] = vertex.Properties.ToDictionary(kvp => kvp.Key, kvp => writer.ToDict(kvp.Value));
            }

            return GraphSONUtil.ToTypedValue(nameof(Vertex), vertexDict);
        }
    }
}
