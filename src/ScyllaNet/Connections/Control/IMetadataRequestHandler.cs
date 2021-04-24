// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scylla.Net.Responses;
using Scylla.Net.Serialization;

namespace Scylla.Net.Connections.Control
{
    /// <summary>
    /// Handles metadata queries (usually sent by the control connection).
    /// </summary>
    internal interface IMetadataRequestHandler
    {
        Task<Response> SendMetadataRequestAsync(
            IConnection connection, ISerializer serializer, string cqlQuery, QueryProtocolOptions queryProtocolOptions);

        Task<Response> UnsafeSendQueryRequestAsync(
            IConnection connection, ISerializer serializer, string cqlQuery, QueryProtocolOptions queryProtocolOptions);

        /// <summary>
        /// Validates that the result contains a RowSet and returns it.
        /// </summary>
        /// <exception cref="NullReferenceException" />
        /// <exception cref="DriverInternalError" />
        IEnumerable<IRow> GetRowSet(Response response);
    }
}
