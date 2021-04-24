// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Scylla.Net.Connections.Control;

namespace Scylla.Net.Connections
{
    /// <inheritdoc />
    internal class SniConnectionEndPoint : IConnectionEndPoint
    {
        private readonly string _serverName;
        private readonly IPEndPoint _hostIpEndPoint;

        public SniConnectionEndPoint(IPEndPoint socketIpEndPoint, string serverName, IContactPoint contactPoint) :
            this(socketIpEndPoint, null, serverName, contactPoint)
        {
        }

        public SniConnectionEndPoint(IPEndPoint socketIpEndPoint, IPEndPoint hostIpEndPoint, string serverName, IContactPoint contactPoint)
        {
            SocketIpEndPoint = socketIpEndPoint ?? throw new ArgumentNullException(nameof(socketIpEndPoint));
            _hostIpEndPoint = hostIpEndPoint;
            _serverName = serverName;
            ContactPoint = contactPoint;

            var stringBuilder = new StringBuilder(hostIpEndPoint?.ToString() ?? socketIpEndPoint.ToString());

            if (hostIpEndPoint == null && serverName != null)
            {
                stringBuilder.Append($" ({serverName})");
            }

            EndpointFriendlyName = stringBuilder.ToString();
        }

        /// <inheritdoc />
        public IContactPoint ContactPoint { get; }

        /// <inheritdoc />
        public IPEndPoint SocketIpEndPoint { get; }

        /// <inheritdoc />
        public string EndpointFriendlyName { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return EndpointFriendlyName;
        }

        /// <inheritdoc />
        public Task<string> GetServerNameAsync()
        {
            return Task.FromResult(_serverName);
        }

        /// <inheritdoc />
        public IPEndPoint GetHostIpEndPointWithFallback()
        {
            return _hostIpEndPoint ?? SocketIpEndPoint;
        }

        /// <inheritdoc />
        public IPEndPoint GetHostIpEndPoint()
        {
            return _hostIpEndPoint;
        }

        public bool Equals(IConnectionEndPoint other)
        {
            return Equals((object)other);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SniConnectionEndPoint point))
            {
                return false;
            }

            if (!object.Equals(_hostIpEndPoint, point._hostIpEndPoint))
            {
                return false;
            }

            if (!object.Equals(SocketIpEndPoint, point.SocketIpEndPoint))
            {
                return false;
            }

            return _serverName == point._serverName;
        }

        public override int GetHashCode()
        {
            return Utils.CombineHashCodeWithNulls(new object[]
            {
                _hostIpEndPoint, _serverName, SocketIpEndPoint
            });
        }
    }
}
