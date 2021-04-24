// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// Represents a property with element in DSE Graph. Element will always be null when GraphSON2 is NOT used.
    /// </summary>
    public interface IPropertyWithElement : IProperty, IEquatable<IPropertyWithElement>
    {
        /// <summary>
        /// Gets the property element.
        /// </summary>
        IGraphNode Element { get; }
    }
}
