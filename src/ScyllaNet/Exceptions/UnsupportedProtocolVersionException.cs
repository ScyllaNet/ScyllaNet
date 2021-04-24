// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    internal class UnsupportedProtocolVersionException : DriverException
    {
        /// <summary>
        /// The version that was not supported by the server.
        /// </summary>
        public ProtocolVersion ProtocolVersion { get; }
        
        /// <summary>
        /// The version with which the server replied.
        /// </summary>
        public ProtocolVersion ResponseProtocolVersion { get; }

        public UnsupportedProtocolVersionException(ProtocolVersion protocolVersion, ProtocolVersion responseProtocolVersion, Exception innerException) : 
            base($"Protocol version {protocolVersion} not supported", innerException)
        {
            ProtocolVersion = protocolVersion;
            ResponseProtocolVersion = responseProtocolVersion;
        }
    }
}
