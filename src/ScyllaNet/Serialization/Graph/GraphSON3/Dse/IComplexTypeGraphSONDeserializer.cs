// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.GraphSON3.Dse
{
    internal interface IComplexTypeGraphSONDeserializer
    {
        /// <summary>
        ///     Deserializes GraphSON UDT to an object.
        /// </summary>
        /// <param name="graphsonObject">The GraphSON udt object to objectify.</param>
        /// <param name="type">Target type.</param>
        /// <param name="serializer">The graph type serializer instance.</param>
        /// <param name="genericSerializer">Generic serializer instance from which UDT Mappings can be obtained.</param>
        /// <returns>The deserialized object.</returns>
        dynamic Objectify(JToken graphsonObject, Type type, IGraphTypeSerializer serializer, IGenericSerializer genericSerializer);
    }
}
