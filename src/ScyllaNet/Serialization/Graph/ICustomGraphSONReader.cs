// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.GraphSON2;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph
{
    /// <summary>
    /// <para>
    /// This interface is similar to <see cref="IGraphSONReader"/> but the deserializers do NOT
    /// depend on this one while they depend on <see cref="IGraphSONReader"/>.
    /// </para>
    /// <para>
    /// This interface is implemented by the custom GraphSON reader (<see cref="CustomGraphSON2Reader"/>)
    /// which is an imported version of the Tinkerpop's <see cref="GraphSONReader"/> with a few changes.
    /// </para>
    /// <para>
    /// See XML docs of <see cref="GraphTypeSerializer"/> for more information.
    /// </para>
    /// </summary>
    internal interface ICustomGraphSONReader
    {
        dynamic ToObject(JToken jToken);
    }
}
