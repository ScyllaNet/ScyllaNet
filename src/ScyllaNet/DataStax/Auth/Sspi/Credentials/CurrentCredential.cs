// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Runtime.CompilerServices;

namespace Scylla.Net.DataStax.Auth.Sspi.Credentials
{
    /// <summary>
    /// Acquires a handle to the credentials of the user associated with the current process.
    /// </summary>
    internal class CurrentCredential : Credential
    {
        /// <summary>
        /// Initializes a new instance of the CurrentCredential class.
        /// </summary>
        /// <param name="securityPackage">The security package to acquire the credential handle
        /// from.</param>
        /// <param name="use">The manner in which the credentials will be used - Inbound typically
        /// represents servers, outbound typically represent clients.</param>
        public CurrentCredential( string securityPackage, CredentialUse use ) :
            base( securityPackage )
        {
            Init( use );
        }

        private void Init( CredentialUse use )
        {
            string packageName;
            var rawExpiry = new TimeStamp();
            var status = SecurityStatus.InternalError;

            // -- Package --
            // Copy off for the call, since this.SecurityPackage is a property.
            packageName = SecurityPackage;

            Handle = new SafeCredentialHandle();

            // The finally clause is the actual constrained region. The VM pre-allocates any stack space,
            // performs any allocations it needs to prepare methods for execution, and postpones any 
            // instances of the 'uncatchable' exceptions (ThreadAbort, StackOverflow, OutOfMemory).
            RuntimeHelpers.PrepareConstrainedRegions();
            try { }
            finally
            {
                status = CredentialNativeMethods.AcquireCredentialsHandle(
                   null,
                   packageName,
                   use,
                   IntPtr.Zero,
                   IntPtr.Zero,
                   IntPtr.Zero,
                   IntPtr.Zero,
                   ref Handle.rawHandle,
                   ref rawExpiry
               );
            }

            if ( status != SecurityStatus.OK )
            {
                throw new SspiException( "Failed to call AcquireCredentialHandle", status );
            }

            Expiry = rawExpiry.ToDateTime();
        }

    }
}
