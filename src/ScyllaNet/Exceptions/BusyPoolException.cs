// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Net;
using Scylla.Net.Connections;

namespace Scylla.Net
{
    /// <summary>
    /// Represents a client-side error indicating that all connections to a certain host have reached
    /// the maximum amount of in-flight requests supported.
    /// </summary>
    public class BusyPoolException : DriverException
    {
        /// <summary>
        /// Gets the host address.
        /// </summary>
        public IPEndPoint Address { get; }

        /// <summary>
        /// Gets the maximum amount of requests per connection.
        /// </summary>
        public int MaxRequestsPerConnection { get; }

        /// <summary>
        /// Gets the size of the pool.
        /// </summary>
        public int ConnectionLength { get; }

        /// <summary>
        /// Creates a new instance of <see cref="BusyPoolException"/>.
        /// </summary>
        public BusyPoolException(IPEndPoint address, int maxRequestsPerConnection, int connectionLength)
            : base(BusyPoolException.GetMessage(address, maxRequestsPerConnection, connectionLength))
        {
            Address = address;
            MaxRequestsPerConnection = maxRequestsPerConnection;
            ConnectionLength = connectionLength;
        }
        
        private static string GetMessage(IPEndPoint address, int maxRequestsPerConnection, int connectionLength)
        {
            return $"All connections to host {address} are busy, {maxRequestsPerConnection} requests " +
                   $"are in-flight on {(connectionLength > 0 ? "each " : "")}{connectionLength} connection(s)";
        }
        
        private static string GetMessage(IConnectionEndPoint endPoint, int maxRequestsPerConnection, int connectionLength)
        {
            return $"All connections to host {endPoint.EndpointFriendlyName} are busy, {maxRequestsPerConnection} requests " +
                   $"are in-flight on {(connectionLength > 0 ? "each " : "")}{connectionLength} connection(s)";
        }
    }
}
