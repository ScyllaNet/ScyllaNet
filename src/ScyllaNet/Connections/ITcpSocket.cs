// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.IO;

namespace Scylla.Net.Connections
{
    internal interface ITcpSocket : IDisposable
    {
        IConnectionEndPoint EndPoint { get; }

        SocketOptions Options { get; }

        SSLOptions SSLOptions { get; set; }

        /// <summary>
        /// Event that gets fired when new data is received.
        /// </summary>
        event Action<byte[], int> Read;

        /// <summary>
        /// Event that gets fired when a write async request have been completed.
        /// </summary>
        event Action WriteCompleted;

        /// <summary>
        /// Event that is fired when the host is closing the connection.
        /// </summary>
        event Action Closing;

        event Action<Exception, SocketError?> Error;

        /// <summary>
        /// Get this socket's local address.
        /// </summary>
        /// <returns>The socket's local address.</returns>
        IPEndPoint GetLocalIpEndPoint();

        /// <summary>
        /// Initializes the socket options
        /// </summary>
        void Init();

        /// <summary>
        /// Connects asynchronously to the host and starts reading
        /// </summary>
        /// <exception cref="SocketException">Throws a SocketException when the connection could not be established with the host</exception>
        Task<bool> Connect();

        /// <summary>
        /// Sends data asynchronously
        /// </summary>
        void Write(RecyclableMemoryStream stream, Action onBufferFlush);

        void Kill();
    }
}
