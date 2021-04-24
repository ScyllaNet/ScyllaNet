// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Structure
{
    internal class EdgeSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, IGraphSONWriter writer)
        {
            Edge edge = objectData;
            var inV = edge.InV.To<IVertex>();
            var outV = edge.OutV.To<IVertex>();
            var edgeDict = new Dictionary<string, dynamic>
            {
                {"id", writer.ToDict(edge.Id)},
                {"outV", writer.ToDict(outV.Id)},
                {"outVLabel", outV.Label},
                {"label", edge.Label},
                {"inV", writer.ToDict(inV.Id)},
                {"inVLabel", inV.Label}
            };
            return GraphSONUtil.ToTypedValue(nameof(Edge), edgeDict);
        }
    }
}
