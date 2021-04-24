// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Net;

namespace Scylla.Net
{
    /// <summary>
    ///  Indicates an error during the authentication phase while connecting to a node.
    /// </summary>
    public class AuthenticationException : DriverException
    {
        /// <summary>
        ///  Gets the host for which the authentication failed. 
        /// </summary>
        public IPEndPoint Host { get; private set; }

        public AuthenticationException(string message)
            : base(message)
        {
        }

        public AuthenticationException(string message, IPEndPoint host)
            : base(string.Format("Authentication error on host {0}: {1}", host, message))
        {
            Host = host;
        }
    }
}
