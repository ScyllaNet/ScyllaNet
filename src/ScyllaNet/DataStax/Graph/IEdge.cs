// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// Represents an edge in DSE graph.
    /// </summary>
    public interface IEdge : IElement
    {
        /// <summary>
        /// Gets the incoming/head vertex.
        /// </summary>
        IGraphNode InV { get; }

        /// <summary>
        /// Gets the label of the incoming/head vertex.
        /// </summary>
        string InVLabel { get; }

        /// <summary>
        /// Gets the outgoing/tail vertex.
        /// </summary>
        IGraphNode OutV { get; }

        /// <summary>
        /// Gets the label of the outgoing/tail vertex.
        /// </summary>
        string OutVLabel { get; }
    }
}
