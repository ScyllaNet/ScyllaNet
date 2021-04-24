// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Graph.Internal;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    /// <summary>
    /// Handles deserialization of GraphSON3 data.
    /// </summary>
    internal class GraphSON2Reader : GraphSONReader
    {
        /// <summary>
        /// Creates a new instance of <see cref="GraphSON2Reader"/>.
        /// </summary>
        public GraphSON2Reader()
        {
            
        }

        /// <summary>
        /// Creates a new instance of <see cref="GraphSON2Reader"/>.
        /// </summary>
        public GraphSON2Reader(IReadOnlyDictionary<string, IGraphSONDeserializer> deserializerByGraphSONType) : 
            base(deserializerByGraphSONType)
        {
            
        }
    }
}
