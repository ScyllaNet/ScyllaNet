// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Threading.Tasks;
using Scylla.Net.Requests;
using Scylla.Net.Responses;

namespace Scylla.Net.Connections.Control
{
    internal class ServerEventsSubscriber : IServerEventsSubscriber
    {
        private const CassandraEventType CassandraEventTypes = CassandraEventType.TopologyChange | CassandraEventType.StatusChange | CassandraEventType.SchemaChange;

        /// <inheritdoc />
        public async Task SubscribeToServerEvents(IConnection connection, CassandraEventHandler handler)
        {
            connection.CassandraEventResponse += handler;

            // Register to events on the connection
            var response = await connection.Send(new RegisterForEventRequest(ServerEventsSubscriber.CassandraEventTypes))
                                           .ConfigureAwait(false);
            if (!(response is ReadyResponse))
            {
                throw new DriverInternalError("Expected ReadyResponse, obtained " + response?.GetType().Name);
            }
        }
    }
}
