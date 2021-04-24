// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Net.Sockets;
using System.Threading.Tasks;

namespace Scylla.Net.Connections.Control
{
    /// <summary>
    /// Gets the next connection and setup the event listener for the host and connection.
    /// </summary>
    /// <exception cref="SocketException" />
    /// <exception cref="DriverInternalError" />
    internal interface IServerEventsSubscriber
    {
        Task SubscribeToServerEvents(IConnection connection, CassandraEventHandler handler);
    }
}
