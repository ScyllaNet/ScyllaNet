// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.Requests
{
    /// <summary>
    /// Builds graph type serializers according to the protocol version. Also exposes a method
    /// to compute the default graph protocol according to the keyspace metadata.
    /// </summary>
    internal interface IGraphTypeSerializerFactory
    {
        /// <summary>
        /// Resolves the graph protocol version according to the keyspace metadata or language.
        /// See <see cref="GraphOptions.GraphProtocolVersion"/> for an explanation.
        /// </summary>
        GraphProtocol GetDefaultGraphProtocol(IInternalSession session, IGraphStatement statement, GraphOptions options);
        
        /// <summary>
        /// Gets the serializer according to the protocol version.
        /// </summary>
        IGraphTypeSerializer CreateSerializer(
            IInternalSession session,
            IReadOnlyDictionary<GraphProtocol, IReadOnlyDictionary<string, IGraphSONDeserializer>> customDeserializers,
            IReadOnlyDictionary<GraphProtocol, IReadOnlyDictionary<Type, IGraphSONSerializer>> customSerializers,
            GraphProtocol graphProtocolVersion,
            bool deserializeGraphNodes);
    }
}
