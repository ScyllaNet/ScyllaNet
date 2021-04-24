// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.GraphSON2;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph
{
    /// <summary>
    ///     Supports deserializing GraphSON into an object that requires a graph node factory.
    /// This is an adaptation of the Tinkerpop's <see cref="IGraphSONDeserializer"/> interface. The
    /// <see cref="CustomGraphSON2Reader"/> adds support for these custom deserializers on top of the imported
    /// functionality from <see cref="GraphSONReader"/> that handles the standard deserializers.
    /// </summary>
    internal interface IGraphSONStructureDeserializer
    {
        /// <summary>
        ///     Deserializes GraphSON to an object.
        /// </summary>
        /// <param name="graphsonObject">The GraphSON object to objectify.</param>
        /// <param name="graphNodeFactory">Graph Node factory that can be used to build graph node objects.</param>
        /// <param name="reader">A <see cref="GraphSONReader" /> that can be used to objectify properties of the GraphSON object.</param>
        /// <returns>The deserialized object.</returns>
        dynamic Objectify(JToken graphsonObject, Func<JToken, GraphNode> graphNodeFactory, IGraphSONReader reader);
    }
}
