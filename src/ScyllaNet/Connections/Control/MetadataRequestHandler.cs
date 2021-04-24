// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using Scylla.Net.Requests;
using Scylla.Net.Responses;
using Scylla.Net.Serialization;

namespace Scylla.Net.Connections.Control
{
    /// <inheritdoc />
    internal class MetadataRequestHandler : IMetadataRequestHandler
    {
        public async Task<Response> SendMetadataRequestAsync(
            IConnection connection, ISerializer serializer, string cqlQuery, QueryProtocolOptions queryProtocolOptions)
        {
            var request = new QueryRequest(serializer, cqlQuery, queryProtocolOptions, false, null);
            Response response;
            try
            {
                response = await connection.Send(request).ConfigureAwait(false);
            }
            catch (SocketException ex)
            {
                ControlConnection.Logger.Error(
                    $"There was an error while executing on the host {cqlQuery} the query '{connection.EndPoint.EndpointFriendlyName}'", ex);
                throw;
            }
            return response;
        }

        public Task<Response> UnsafeSendQueryRequestAsync(
            IConnection connection, ISerializer serializer, string cqlQuery, QueryProtocolOptions queryProtocolOptions)
        {
            return connection.Send(new QueryRequest(serializer, cqlQuery, queryProtocolOptions, false, null));
        }

        /// <inheritdoc />
        public IEnumerable<IRow> GetRowSet(Response response)
        {
            if (response == null)
            {
                throw new NullReferenceException("Response can not be null");
            }
            if (!(response is ResultResponse))
            {
                throw new DriverInternalError("Expected rows, obtained " + response.GetType().FullName);
            }
            var result = (ResultResponse)response;
            if (!(result.Output is OutputRows))
            {
                throw new DriverInternalError("Expected rows output, obtained " + result.Output.GetType().FullName);
            }
            return ((OutputRows)result.Output).RowSet;
        }
    }
}
