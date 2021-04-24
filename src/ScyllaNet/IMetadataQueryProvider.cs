// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Scylla.Net.Connections;
using Scylla.Net.Responses;
using Scylla.Net.Serialization;

namespace Scylla.Net
{
    /// <summary>
    /// Represents an object that can execute metadata queries
    /// </summary>
    internal interface IMetadataQueryProvider
    {
        ProtocolVersion ProtocolVersion { get; }

        /// <summary>
        /// The address of the endpoint used by the ControlConnection
        /// </summary>
        IConnectionEndPoint EndPoint { get; }
        
        /// <summary>
        /// The local address of the socket used by the ControlConnection
        /// </summary>
        IPEndPoint LocalAddress { get; }

        ISerializerManager Serializer { get; }

        Task<IEnumerable<IRow>> QueryAsync(string cqlQuery, bool retry = false);

        Task<Response> SendQueryRequestAsync(string cqlQuery, bool retry, QueryProtocolOptions queryProtocolOptions);

        /// <summary>
        /// Send request without any retry or reconnection logic. Also exceptions are not caught or logged.
        /// </summary>
        Task<Response> UnsafeSendQueryRequestAsync(string cqlQuery, QueryProtocolOptions queryProtocolOptions);

        IEnumerable<IRow> Query(string cqlQuery, bool retry = false);
    }
}
