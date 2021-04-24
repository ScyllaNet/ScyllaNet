// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// Represents an element in DSE Graph.
    /// </summary>
    public interface IElement : IEquatable<IElement>
    {
        /// <summary>
        /// Gets the label of the element.
        /// </summary>
        string Label { get; }

        /// <summary>
        /// Gets the identifier as an instance of <see cref="IGraphNode"/>.
        /// </summary>
        IGraphNode Id { get; }

        /// <summary>
        /// Gets a property by name.
        /// </summary>
        IProperty GetProperty(string name);

        /// <summary>
        /// Gets all properties of an element.
        /// </summary>
        IEnumerable<IProperty> GetProperties();
    }
}
