// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Net;
using System.Net.Security;

namespace Scylla.Net.Connections
{
    /// <summary>
    /// Reverse resolver. Used to obtain the server name from a given IP endpoint. The returned server
    /// name will then be used in <see cref="SslStream.AuthenticateAsClientAsync(string)"/> for SSL and SNI for example.
    /// </summary>
    internal interface IServerNameResolver
    {
        /// <summary>
        /// Performs reverse resolution to obtain the server name from the given ip endpoint.
        /// </summary>
        string GetServerName(IPEndPoint socketIpEndPoint);
    }
}
