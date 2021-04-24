// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.DataStax.Graph.Internal
{
    /// <summary>
    ///     Supports serializing of an object to GraphSON.
    /// </summary>
    public interface IGraphSONSerializer
    {
        /// <summary>
        ///     Transforms an object into a dictionary that resembles its GraphSON representation.
        /// </summary>
        /// <param name="objectData">The object to dictify.</param>
        /// <param name="writer">A <see cref="IGraphSONWriter" /> that can be used to dictify properties of the object.</param>
        /// <returns>The GraphSON representation.</returns>
        Dictionary<string, dynamic> Dictify(dynamic objectData, IGraphSONWriter writer);
    }
}
