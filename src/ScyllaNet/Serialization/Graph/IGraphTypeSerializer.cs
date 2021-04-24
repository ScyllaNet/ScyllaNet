// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.GraphSON2;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph
{
    /// <summary>
    /// This is the point of entry for the serialization and deserialization logic
    /// (including the type conversion functionality).
    /// </summary>
    internal interface IGraphTypeSerializer
    {
        /// <summary>
        /// <para>
        /// When this is true, the type serializer will deserialize objects as <see cref="GraphNode"/>
        /// when the requested type is object.
        /// </para>
        /// <para>
        /// This is used by the fluent driver (set to false) to force the deserialization of all
        /// inner properties to the actual types instead of returning GraphNode objects. This is necessary
        /// because the GLV serializers call <see cref="IGraphSONReader.ToObject"/> which is implemented by
        /// <see cref="GraphTypeSerializer"/> with a call to <see cref="FromDb(JToken,Type)"/> (with "object" as the requested type).
        /// No type conversion will be made since the requested type is object.
        /// </para>
        /// </summary>
        bool DefaultDeserializeGraphNodes { get; }

        GraphProtocol GraphProtocol { get; }

        /// <summary>
        /// Returns the row parser according to the <see cref="GraphProtocol"/>. This will be passed
        /// to the graph result set.
        /// </summary>
        Func<Row, GraphNode> GetGraphRowParser();

        /// <summary>
        /// Performs deserialization of the provided token and attempts to convert the
        /// deserialized object to the provided type. 
        /// </summary>
        object FromDb(JToken token, Type type);

        /// <summary>
        /// Overload of <see cref="FromDb(JToken,Type)"/> that allows the caller to override
        /// <see cref="DefaultDeserializeGraphNodes"/>.
        /// </summary>
        object FromDb(JToken token, Type type, bool deserializeGraphNodes);
        
        /// <summary>
        /// Generic version of <see cref="FromDb(JToken,Type)"/>
        /// </summary>
        T FromDb<T>(JToken token);

        /// <summary>
        /// Serializes the provided object to GraphSON.
        /// </summary>
        string ToDb(object obj);

        /// <summary>
        /// Attempts to convert the provided object to the target type.
        /// </summary>
        /// <returns>True if conversion was successful (output in the out parameter).</returns>
        bool ConvertFromDb(object obj, Type targetType, out dynamic result);
    }
}
