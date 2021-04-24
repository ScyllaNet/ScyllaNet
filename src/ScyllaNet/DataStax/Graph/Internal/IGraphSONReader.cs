// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.DataStax.Graph.Internal
{
    /// <summary>
    /// <para>
    /// This interface is not meant to be implemented by users. It is part of the public API so that
    /// the C# Graph Extension can provide custom deserializers for the Tinkerpop GLV types.
    /// </para>
    /// <para>
    /// Implementations of <see cref="IGraphSONDeserializer"/> depend on IGraphSONReader objects
    /// to deserialize inner properties.
    /// </para>
    /// <para>
    /// It's basically an interface for the Tinkerpop's <see cref="GraphSONReader"/> abstract class.
    /// </para>
    /// </summary>
    public interface IGraphSONReader
    {
        dynamic ToObject(JToken jToken);
    }
}
