// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.DataStax.Auth.Sspi.Credentials
{
    /// <summary>
    /// Indicates the manner in which a credential will be used for SSPI authentication.
    /// </summary>
    internal enum CredentialUse : uint
    {
        /// <summary>
        /// The credentials will be used for establishing a security context with an inbound request, eg,
        /// the credentials will be used by a server building a security context with a client.
        /// </summary>
        Inbound = 1,

        /// <summary>
        /// The credentials will be used for establishing a security context as an outbound request,
        /// eg, the credentials will be used by a client to build a security context with a server.
        /// </summary>
        Outbound = 2,

        /// <summary>
        /// The credentials may be used to to either build a client's security context or a server's
        /// security context.
        /// </summary>
        Both = 3,
    }
}
