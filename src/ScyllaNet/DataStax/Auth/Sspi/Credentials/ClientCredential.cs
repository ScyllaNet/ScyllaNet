// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.DataStax.Auth.Sspi.Credentials
{
    /// <summary>
    /// Represents the credentials of the user running the current process, for use as an SSPI client.
    /// </summary>
    internal class ClientCredential : CurrentCredential
    {
        /// <summary>
        /// Initializes a new instance of the ClientCredential class.
        /// </summary>
        /// <param name="package">The security package to acquire the credential handle from.</param>
        public ClientCredential( string package )
            : base( package, CredentialUse.Outbound )
        {
        }
    }
}
