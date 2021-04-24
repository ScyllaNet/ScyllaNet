// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.GraphSON3.Dse
{
    /// <inheritdoc />
    internal class TupleGraphSONDeserializer : IComplexTypeGraphSONDeserializer
    {
        /// <inheritdoc />
        public dynamic Objectify(
            JToken graphsonObject, Type type, IGraphTypeSerializer serializer, IGenericSerializer genericSerializer)
        {
            if (!Utils.IsTuple(type))
            {
                throw new InvalidOperationException($"Can not deserialize a tuple to {type.FullName}.");
            }

            var values = (JArray)graphsonObject["value"];

            var genericArguments = type.GetGenericArguments();

            if (genericArguments.Length != values.Count)
            {
                throw new InvalidOperationException(
                    "Could not deserialize tuple, number of elements don't match " +
                    $"(expected {genericArguments.Length} but the server returned {values.Count}).");
            }
            var tupleValues = new object[values.Count];
            for (var i = 0; i < tupleValues.Length; i++)
            {
                tupleValues[i] = serializer.FromDb(values[i], genericArguments[i], false);
            }

            return Activator.CreateInstance(type, tupleValues);
        }
    }
}
