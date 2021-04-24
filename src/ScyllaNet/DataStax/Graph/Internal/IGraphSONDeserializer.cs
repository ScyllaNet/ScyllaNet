// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

using Newtonsoft.Json.Linq;

namespace Scylla.Net.DataStax.Graph.Internal
{
    /// <summary>
    ///     Supports deserializing GraphSON into an object.
    /// </summary>
    public interface IGraphSONDeserializer
    {
        /// <summary>
        ///     Deserializes GraphSON to an object.
        /// </summary>
        /// <param name="graphsonObject">The GraphSON object to objectify.</param>
        /// <param name="reader">A <see cref="IGraphSONReader" /> that can be used to objectify properties of the GraphSON object.</param>
        /// <returns>The deserialized object.</returns>
        dynamic Objectify(JToken graphsonObject, IGraphSONReader reader);
    }
}
