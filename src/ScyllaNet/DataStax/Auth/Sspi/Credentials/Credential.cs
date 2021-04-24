// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Scylla.Net.DataStax.Auth.Sspi.Credentials
{
    /// <summary>
    /// Provides access to the pre-existing credentials of a security principle.
    /// </summary>
    internal class Credential : IDisposable
    {
        /// <summary>
        /// Whether the Credential has been disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// The name of the security package that controls the credential.
        /// </summary>
        private string securityPackage;

        /// <summary>
        /// A safe handle to the credential's handle.
        /// </summary>
        private SafeCredentialHandle safeCredHandle;

        /// <summary>
        /// The UTC time the credentials expire.
        /// </summary>
        private DateTime expiry;

        /// <summary>
        /// Initializes a new instance of the Credential class.
        /// </summary>
        /// <param name="package">The security package to acquire the credential from.</param>
        public Credential( string package )
        {
            disposed = false;
            securityPackage = package;

            expiry = DateTime.MinValue;

            PackageInfo = PackageSupport.GetPackageCapabilities( SecurityPackage );
        }
        
        /// <summary>
        /// Gets metadata for the security package associated with the credential.
        /// </summary>
        public SecPkgInfo PackageInfo { get; private set; }

        /// <summary>
        /// Gets the name of the security package that owns the credential.
        /// </summary>
        public string SecurityPackage
        {
            get
            {
                CheckLifecycle();

                return securityPackage;
            }
        }

        /// <summary>
        /// Returns the User Principle Name of the credential. Depending on the underlying security
        /// package used by the credential, this may not be the same as the Down-Level Logon Name
        /// for the user.
        /// </summary>
        public string PrincipleName
        {
            get
            {
                QueryNameAttribCarrier carrier;
                SecurityStatus status;
                string name = null;
                var gotRef = false;

                CheckLifecycle();

                status = SecurityStatus.InternalError;
                carrier = new QueryNameAttribCarrier();

                RuntimeHelpers.PrepareConstrainedRegions();
                try
                {
                    safeCredHandle.DangerousAddRef( ref gotRef );
                }
                catch( Exception )
                {
                    if( gotRef == true )
                    {
                        safeCredHandle.DangerousRelease();
                        gotRef = false;
                    }
                    throw;
                }
                finally
                {
                    if( gotRef )
                    {
                        status = CredentialNativeMethods.QueryCredentialsAttribute_Name(
                            ref safeCredHandle.rawHandle,
                            CredentialQueryAttrib.Names,
                            ref carrier
                        );

                        safeCredHandle.DangerousRelease();

                        if( status == SecurityStatus.OK && carrier.Name != IntPtr.Zero )
                        {
                            try
                            {
                                name = Marshal.PtrToStringUni( carrier.Name );
                            }
                            finally
                            {
                                NativeMethods.FreeContextBuffer( carrier.Name );
                            }
                        }
                    }
                }

                if( status.IsError() )
                {
                    throw new SspiException( "Failed to query credential name", status );
                }

                return name;
            }
        }

        /// <summary>
        /// Gets the UTC time the credentials expire.
        /// </summary>
        public DateTime Expiry
        {
            get
            {
                CheckLifecycle();

                return expiry;
            }

            protected set
            {
                CheckLifecycle();
                                
                expiry = value;
            }
        }

        /// <summary>
        /// Gets a handle to the credential.
        /// </summary>
        public SafeCredentialHandle Handle
        {
            get
            {
                CheckLifecycle();

                return safeCredHandle;
            }

            protected set
            {
                CheckLifecycle();

                safeCredHandle = value;
            }
        }

        /// <summary>
        /// Releases all resources associated with the credential.
        /// </summary>
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( disposed == false )
            {
                if ( disposing )
                {
                    safeCredHandle.Dispose();
                }

                disposed = true;
            }
        }

        private void CheckLifecycle()
        {
            if( disposed )
            {
                throw new ObjectDisposedException( "Credential" );
            }
        }
    }
}
