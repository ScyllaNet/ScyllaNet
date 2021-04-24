// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// Represents an edge in DSE graph.
    /// </summary>
    public class Edge : Element, IEdge
    {
        /// <summary>
        /// Gets the incoming/head vertex.
        /// </summary>
        public GraphNode InV { get; }

        IGraphNode IEdge.InV => InV;

        /// <summary>
        /// Gets the label of the incoming/head vertex.
        /// </summary>
        public string InVLabel { get; }

        /// <summary>
        /// Gets the outgoing/tail vertex.
        /// </summary>
        public GraphNode OutV { get; }

        IGraphNode IEdge.OutV => OutV;

        /// <summary>
        /// Gets the label of the outgoing/tail vertex.
        /// </summary>
        public string OutVLabel { get; }

        /// <summary>
        /// Creates a new instance of <see cref="Edge"/>.
        /// </summary>
        public Edge(GraphNode id, string label, IDictionary<string, GraphNode> properties, 
            GraphNode inV, string inVLabel, GraphNode outV, string outVLabel)
            : base(id, label, properties)
        {
            InV = inV;
            InVLabel = inVLabel;
            OutV = outV;
            OutVLabel = outVLabel;
        }
    }
}
