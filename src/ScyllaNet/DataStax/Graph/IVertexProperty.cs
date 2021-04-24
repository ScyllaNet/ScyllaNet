// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// Represents a vertex property in DSE Graph.
    /// <para>
    /// Vertex properties are special because they are also elements, and thus have an identifier; they can also
    /// contain properties of their own (usually referred to as "meta properties").
    /// </para>
    /// </summary>
    public interface IVertexProperty : IProperty, IElement, IEquatable<IVertexProperty>
    {
        /// <summary>
        ///     The <see cref="IVertex" /> that owns this <see cref="IVertexProperty" />.
        /// </summary>
        IGraphNode Vertex { get; }
    }
}
