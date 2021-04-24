// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using Scylla.Net.DataStax.Graph.Internal;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    /// <summary>
    /// Handles serialization of GraphSON2 data.
    /// </summary>
    internal class GraphSON2Writer : GraphSONWriter
    {
        /// <summary>
        /// Creates a new instance of <see cref="GraphSON2Writer"/>.
        /// </summary>
        public GraphSON2Writer()
        {
            
        }
        
        /// <summary>
        /// Creates a new instance of <see cref="GraphSON2Writer"/>.
        /// </summary>
        public GraphSON2Writer(IReadOnlyDictionary<Type, IGraphSONSerializer> customSerializerByType) : 
            base(customSerializerByType)
        {
            
        }
    }
}
