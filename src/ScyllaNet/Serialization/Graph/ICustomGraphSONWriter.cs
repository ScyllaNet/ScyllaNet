// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.GraphSON2;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph
{
    /// <summary>
    /// <para>
    /// This interface is similar to <see cref="IGraphSONWriter"/> but the serializers do NOT
    /// depend on this one while they depend on <see cref="IGraphSONWriter"/>.
    /// </para>
    /// <para>
    /// This interface is implemented by the custom GraphSON writer (<see cref="CustomGraphSON2Writer"/>)
    /// which is an imported version of the Tinkerpop's <see cref="GraphSONWriter"/> with a few changes.
    /// </para>
    /// <para>
    /// See XML docs of <see cref="GraphTypeSerializer"/> for more information.
    /// </para>
    /// </summary>
    internal interface ICustomGraphSONWriter
    {
        bool TryToDict(dynamic objectData, out dynamic result);
    }
}
