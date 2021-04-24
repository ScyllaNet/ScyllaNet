// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Net;

namespace Scylla.Net.Connections
{
    /// <inheritdoc />
    internal class ServerNameResolver : IServerNameResolver
    {
        private readonly ProtocolOptions _protocolOptions;

        public ServerNameResolver(ProtocolOptions protocolOptions)
        {
            _protocolOptions = protocolOptions ?? throw new ArgumentNullException(nameof(protocolOptions));
        }

        /// <inheritdoc />
        public string GetServerName(IPEndPoint socketIpEndPoint)
        {
            try
            {
                return _protocolOptions.SslOptions.HostNameResolver(socketIpEndPoint.Address);
            }
            catch (Exception ex)
            {
                TcpSocket.Logger.Error(
                    $"SSL connection: Can not resolve host name for address {socketIpEndPoint.Address}." +
                    " Using the IP address instead of the host name. This may cause RemoteCertificateNameMismatch " +
                    "error during Cassandra host authentication. Note that the Cassandra node SSL certificate's " +
                    "CN(Common Name) must match the Cassandra node hostname.", ex);
                return socketIpEndPoint.Address.ToString();
            }
        }
    }
}
