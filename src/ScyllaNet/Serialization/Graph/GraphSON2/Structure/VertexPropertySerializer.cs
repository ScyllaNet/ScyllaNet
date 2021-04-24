// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Structure
{
    internal class VertexPropertySerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, IGraphSONWriter writer)
        {
            VertexProperty vertexProperty = objectData;
            var valueDict = new Dictionary<string, dynamic>
            {
                {"id", writer.ToDict(vertexProperty.Id)},
                {"label", vertexProperty.Label},
                {"value", writer.ToDict(vertexProperty.Value)}
            };
            if (vertexProperty.Vertex != null)
            {
                valueDict.Add("vertex", writer.ToDict(vertexProperty.Vertex.To<IVertex>().Id));
            }
            return GraphSONUtil.ToTypedValue(nameof(VertexProperty), valueDict);
        }
    }
}
