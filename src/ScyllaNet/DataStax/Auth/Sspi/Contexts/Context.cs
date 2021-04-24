// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Runtime.CompilerServices;
using Scylla.Net.DataStax.Auth.Sspi.Buffers;
using Scylla.Net.DataStax.Auth.Sspi.Credentials;

namespace Scylla.Net.DataStax.Auth.Sspi.Contexts
{
    /// <summary>
    /// Represents a security context and provides common functionality required for all security 
    /// contexts.
    /// </summary>
    /// <remarks>
    /// This class is abstract and has a protected constructor and Initialize method. The exact 
    /// initialization implementation is provided by a subclasses, which may perform initialization 
    /// in a variety of manners.
    /// </remarks>
    internal abstract class Context : IDisposable
    {
        /// <summary>
        /// Produce a header or trailer but do not encrypt the message. See: KERB_WRAP_NO_ENCRYPT.
        /// </summary>
        private const uint WrapNoEncrypt = 0x80000001;

        /// <summary>
        /// Performs basic initialization of a new instance of the Context class.
        /// Initialization is not complete until the ContextHandle property has been set
        /// and the Initialize method has been called.
        /// </summary>
        /// <param name="cred"></param>
        protected Context( Credential cred )
        {
            Credential = cred;

            ContextHandle = new SafeContextHandle();

            Disposed = false;
            Initialized = false;
        }

        /// <summary>
        /// Whether or not the context is fully formed.
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// The credential being used by the context to authenticate itself to other actors.
        /// </summary>
        protected Credential Credential { get; private set; }

        /// <summary>
        /// A reference to the security context's handle.
        /// </summary>
        public SafeContextHandle ContextHandle { get; private set; }
        
        /// <summary>
        /// The UTC time when the context expires.
        /// </summary>
        public DateTime Expiry { get; private set; }

        /// <summary>
        /// Whether the context has been disposed.
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Marks the context as having completed the initialization process, ie, exchanging of authentication tokens.
        /// </summary>
        /// <param name="expiry">The date and time that the context will expire.</param>
        protected void Initialize( DateTime expiry )
        {
            Expiry = expiry;
            Initialized = true;
        }

        /// <summary>
        /// Releases all resources associated with the context.
        /// </summary>
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// Releases resources associated with the context.
        /// </summary>
        /// <param name="disposing">If true, release managed resources, else release only unmanaged resources.</param>
        protected virtual void Dispose( bool disposing )
        {
            if( Disposed ) { return; }

            if( disposing )
            {
                ContextHandle.Dispose();
            }

            Disposed = true;
        }

        /// <summary>
        /// Encrypts the byte array using the context's session key.
        /// </summary>
        /// <remarks>
        /// The structure of the returned data is as follows:
        ///  - 2 bytes, an unsigned big-endian integer indicating the length of the trailer buffer size
        ///  - 4 bytes, an unsigned big-endian integer indicating the length of the message buffer size.
        ///  - 2 bytes, an unsigned big-endian integer indicating the length of the encryption padding buffer size.
        ///  - The trailer buffer
        ///  - The message buffer
        ///  - The padding buffer.
        /// </remarks>
        /// <param name="input">The raw message to encrypt.</param>
        /// <returns>The packed and encrypted message.</returns>
        public byte[] Encrypt( byte[] input )
        {
            // The message is encrypted in place in the buffer we provide to Win32 EncryptMessage
            SecPkgContext_Sizes sizes;

            SecureBuffer trailerBuffer;
            SecureBuffer dataBuffer;
            SecureBuffer paddingBuffer;
            SecureBufferAdapter adapter;

            var status = SecurityStatus.InvalidHandle;
            CheckLifecycle();

            sizes = QueryBufferSizes();

            trailerBuffer = new SecureBuffer( new byte[sizes.SecurityTrailer], BufferType.Token );
            dataBuffer = new SecureBuffer( new byte[input.Length], BufferType.Data );
            paddingBuffer = new SecureBuffer( new byte[sizes.BlockSize], BufferType.Padding );

            Array.Copy( input, dataBuffer.Buffer, input.Length );

            using( adapter = new SecureBufferAdapter( new[] { trailerBuffer, dataBuffer, paddingBuffer } ) )
            {
                status = ContextNativeMethods.SafeEncryptMessage(
                    ContextHandle,
                    Context.WrapNoEncrypt,
                    adapter,
                    0
                );
            }

            if( status != SecurityStatus.OK )
            {
                throw new SspiException( "Failed to encrypt message", status );
            }

            var position = 0;
            
            // Return 1 buffer with the 3 buffers joined
            var result = new byte[trailerBuffer.Length + dataBuffer.Length + paddingBuffer.Length];

            Array.Copy( trailerBuffer.Buffer, 0, result, position, trailerBuffer.Length );
            position += trailerBuffer.Length;

            Array.Copy( dataBuffer.Buffer, 0, result, position, dataBuffer.Length );
            position += dataBuffer.Length;

            Array.Copy( paddingBuffer.Buffer, 0, result, position, paddingBuffer.Length );

            return result;
        }

        /// <summary>
        /// Decrypts a previously encrypted message.
        /// Assumes that the message only contains the "hash" integrity validation.
        /// </summary>
        /// <remarks>
        /// The expected format of the buffer is as follows (order is not important):
        ///  - The trailer buffer
        ///  - The message buffer
        /// </remarks>
        /// <param name="input">The packed and encrypted data.</param>
        /// <returns>Null</returns>
        /// <exception cref="SspiException">Exception thrown when hash validation fails.</exception>
        public byte[] Decrypt( byte[] input )
        {
            SecurityStatus status;

            CheckLifecycle();

            var trailerLength = input.Length;

            var dataBuffer = new SecureBuffer( new byte[0], BufferType.Data );
            var trailerBuffer = new SecureBuffer( new byte[trailerLength], BufferType.Stream );

            Array.Copy( input, 0, trailerBuffer.Buffer, 0, trailerBuffer.Length );

            using (var adapter = new SecureBufferAdapter( new[] { dataBuffer, trailerBuffer } ) )
            {
                status = ContextNativeMethods.SafeDecryptMessage(
                    ContextHandle,
                    0,
                    adapter,
                    0
                );
            }

            if( status != SecurityStatus.OK )
            {
                throw new SspiException( "Failed to decrypt message", status );
            }

            return null;
        }

        /// <summary>
        /// Queries the security package's expectations regarding message/token/signature/padding buffer sizes.
        /// </summary>
        /// <returns></returns>
        private SecPkgContext_Sizes QueryBufferSizes()
        {
            var sizes = new SecPkgContext_Sizes();
            var status = SecurityStatus.InternalError;
            var gotRef = false;

            RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                ContextHandle.DangerousAddRef( ref gotRef );
            }
            catch ( Exception )
            {
                if ( gotRef )
                {
                    ContextHandle.DangerousRelease();
                    gotRef = false;
                }

                throw;
            }
            finally
            {
                if ( gotRef )
                {
                    status = ContextNativeMethods.QueryContextAttributes_Sizes(
                        ref ContextHandle.rawHandle,
                        ContextQueryAttrib.Sizes,
                        ref sizes
                    );
                    ContextHandle.DangerousRelease();
                }
            }

            if( status != SecurityStatus.OK )
            {
                throw new SspiException( "Failed to query context buffer size attributes", status );
            }

            return sizes;
        }

        /// <summary>
        /// Verifies that the object's lifecycle (initialization / disposition) state is suitable for using the 
        /// object.
        /// </summary>
        private void CheckLifecycle()
        {
            if( Initialized == false )
            {
                throw new InvalidOperationException( "The context is not yet fully formed." );
            }
            else if( Disposed )
            {
                throw new ObjectDisposedException( "Context" );
            }
        }
    }
}
