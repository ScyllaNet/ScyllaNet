// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON2
{
    /// <summary>
    /// This is just to grab the tinkerpop's deserializers (fewer code changes to the imported code)
    /// </summary>
    internal class EmptyGraphSON2Reader : GraphSON2Reader
    {
        public Dictionary<string, IGraphSONDeserializer> GetDeserializers() => base.Deserializers;
    }
}
