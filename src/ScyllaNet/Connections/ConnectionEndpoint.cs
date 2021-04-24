// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Net;
using System.Threading.Tasks;
using Scylla.Net.Connections.Control;

namespace Scylla.Net.Connections
{
    /// <inheritdoc />
    internal class ConnectionEndPoint : IConnectionEndPoint
    {
        private readonly IServerNameResolver _serverNameResolver;
        
        public ConnectionEndPoint(IPEndPoint hostIpEndPoint, IServerNameResolver serverNameResolver, IContactPoint contactPoint)
        {
            _serverNameResolver = serverNameResolver ?? throw new ArgumentNullException(nameof(serverNameResolver));
            ContactPoint = contactPoint;
            SocketIpEndPoint = hostIpEndPoint ?? throw new ArgumentNullException(nameof(hostIpEndPoint));
            EndpointFriendlyName = hostIpEndPoint.ToString();
        }

        /// <inheritdoc />
        public IContactPoint ContactPoint { get; }

        /// <inheritdoc />
        public IPEndPoint SocketIpEndPoint { get; }

        /// <inheritdoc />
        public string EndpointFriendlyName { get; }

        /// <inheritdoc />
        public Task<string> GetServerNameAsync()
        {
            return Task.Factory.StartNew(() => _serverNameResolver.GetServerName(SocketIpEndPoint));
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return EndpointFriendlyName;
        }

        /// <inheritdoc />
        public IPEndPoint GetHostIpEndPointWithFallback()
        {
            return SocketIpEndPoint;
        }

        /// <inheritdoc />
        public IPEndPoint GetHostIpEndPoint()
        {
            return SocketIpEndPoint;
        }

        public bool Equals(IConnectionEndPoint other)
        {
            return Equals((object)other);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ConnectionEndPoint point))
            {
                return false;
            }

            return object.Equals(SocketIpEndPoint, point.SocketIpEndPoint);
        }

        public override int GetHashCode()
        {
            return SocketIpEndPoint.GetHashCode();
        }
    }
}
