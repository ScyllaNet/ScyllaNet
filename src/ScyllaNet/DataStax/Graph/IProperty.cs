// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// Represents a property in DSE Graph.
    /// </summary>
    public interface IProperty : IEquatable<IProperty>
    {
        /// <summary>
        /// Gets the property name.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Gets the property value.
        /// </summary>
        IGraphNode Value { get; }
    }
}
