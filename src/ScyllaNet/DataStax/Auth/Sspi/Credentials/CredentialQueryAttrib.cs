// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.DataStax.Auth.Sspi.Credentials
{
    /// <summary>
    /// Identifies credential query types.
    /// </summary>
    internal enum CredentialQueryAttrib : uint
    {
        /// <summary>
        /// Queries the credential's principle name.
        /// </summary>
        Names = 1,
    }
}
