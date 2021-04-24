// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// Represents a walk through a graph as defined by a traversal.
    /// </summary>
    public class Path : IPath
    {
        /// <summary>
        /// Returns the sets of labels of the steps traversed by this path, or an empty list, if this path is empty.
        /// </summary>
        public ICollection<ICollection<string>> Labels { get; protected set; }

        /// <summary>
        /// Returns the objects traversed by this path, or an empty list, if this path is empty.
        /// </summary>
        public ICollection<GraphNode> Objects { get; protected set; }

        ICollection<IGraphNode> IPath.Objects => (ICollection<IGraphNode>) Objects;

        /// <summary>
        /// Creates a new instance of <see cref="Path"/>.
        /// </summary>
        /// <param name="labels">The sets of labels of the steps traversed by this path.</param>
        /// <param name="objects">The objects traversed by this path</param>
        public Path(ICollection<ICollection<string>> labels, ICollection<GraphNode> objects)
        {
            Labels = labels;
            Objects = objects;
        }
    }
}
