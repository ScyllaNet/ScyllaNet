// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// Represents a walk through a graph as defined by a traversal.
    /// </summary>
    public interface IPath
    {
        /// <summary>
        /// Gets the sets of labels of the steps traversed by this path, or an empty list, if this path is empty.
        /// </summary>
        ICollection<ICollection<string>> Labels { get; }
        
        /// <summary>
        /// Gets the objects traversed by this path, or an empty list, if this path is empty.
        /// </summary>
        ICollection<IGraphNode> Objects { get; }
    }
}
