// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.DataStax.Graph.Internal;

namespace Scylla.Net.Serialization.Graph.GraphSON3.Dse
{
    internal interface IComplexTypeGraphSONSerializer
    {
        /// <summary>
        ///     Transforms an object into a dictionary that resembles its GraphSON representation.
        /// </summary>
        /// <param name="objectData">The object to dictify.</param>
        /// <param name="serializer">The graph type serializer instance.</param>
        /// <param name="genericSerializer">Generic serializer instance from which UDT Mappings can be obtained.</param>
        /// <param name="result">The GraphSON representation.</param>
        /// <returns>True if this object is a UDT and serialization was successful. False if this object is not a UDT.</returns>
        bool TryDictify(
            dynamic objectData,
            IGraphSONWriter serializer,
            IGenericSerializer genericSerializer,
            out dynamic result);
    }
}
