// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Net;

using Scylla.Net.Connections;

// ReSharper disable once CheckNamespace
namespace Scylla.Net
{
    /// <summary>
    /// Exception that thrown on a client-side timeout, when the client didn't hear back from the server within <see cref="SocketOptions.ReadTimeoutMillis"/>.
    /// </summary>
    public class OperationTimedOutException : DriverException
    {
        public OperationTimedOutException(IPEndPoint address, int timeout) :
            base($"The host {address} did not reply before timeout {timeout}ms")
        {
        }

        internal OperationTimedOutException(IConnectionEndPoint endPoint, int timeout) :
            base($"The host {endPoint} did not reply before timeout {timeout}ms")
        {
        }
    }
}
