// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.DataStax.Graph.Internal
{
    /// <summary>
    /// <para>
    /// This interface is not meant to be implemented by users. It is part of the public API so that
    /// the C# Graph Extension can provide custom serializers for the Tinkerpop GLV types.
    /// </para>
    /// <para>
    /// Implementations of <see cref="IGraphSONSerializer"/> depend on IGraphSONWriter objects
    /// to serialize inner properties.
    /// </para>
    /// <para>
    /// It's basically an interface for the Tinkerpop's <see cref="GraphSONWriter"/> abstract class.
    /// </para>
    /// </summary>
    public interface IGraphSONWriter
    {
        dynamic ToDict(dynamic objectData);

        string WriteObject(dynamic objectData);
    }
}
