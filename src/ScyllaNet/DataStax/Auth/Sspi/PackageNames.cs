// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.DataStax.Auth.Sspi
{
    /// <summary>
    /// Provides canonical names for security pacakges.
    /// </summary>
    internal static class PackageNames
    {
        /// <summary>
        /// Indicates the Negotiate security package.
        /// </summary>
        public const string Negotiate = "Negotiate";

        /// <summary>
        /// Indicates the Kerberos security package.
        /// </summary>
        public const string Kerberos = "Kerberos";

        /// <summary>
        /// Indicates the NTLM security package.
        /// </summary>
        public const string Ntlm = "NTLM";
    }
}
