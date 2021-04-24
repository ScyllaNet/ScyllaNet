// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Runtime.Serialization;

namespace Scylla.Net.DataStax.Auth.Sspi
{
    /// <summary>
    /// The exception that is thrown when a problem occurs hwen using the SSPI system.
    /// </summary>
#if NET452
    [Serializable]
#endif
    public class SspiException : Exception
    {
        private SecurityStatus errorCode;
        private string message;

        /// <summary>
        /// Initializes a new instance of the SSPIException class with the given message and status.
        /// </summary>
        /// <param name="message">A message explaining what part of the system failed.</param>
        /// <param name="errorCode">The error code observed during the failure.</param>
        internal SspiException( string message, SecurityStatus errorCode )
        {
            this.message = message;
            this.errorCode = errorCode;
        }

        /// <summary>
        /// Creates a new instance of <see cref="SspiException"/>.
        /// </summary>
        public SspiException(string message, int errorCode) : this(message, (SecurityStatus) errorCode)
        {
            
        }
        
#if NET452
        /// <summary>
        /// Initializes a new instance of the SSPIException class from serialization data.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SspiException( SerializationInfo info, StreamingContext context )
            : base( info, context )
        {
            this.message = info.GetString( "messsage" );
            this.errorCode = (SecurityStatus)info.GetUInt32( "errorCode" );
        }

        /// <summary>
        /// Serializes the exception.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData( SerializationInfo info, StreamingContext context )
        {
            base.GetObjectData( info, context );

            info.AddValue( "message", this.message );
            info.AddValue( "errorCode", this.errorCode );
        }
#endif

        /// <summary>
        /// The error code that was observed during the SSPI call.
        /// </summary>
        public int ErrorCode
        {
            get
            {
                return (int) this.errorCode;
            }
        }

        /// <summary>
        /// A human-readable message indicating the nature of the exception.
        /// </summary>
        public override string Message
        {
            get
            {
                return string.Format( 
                    "{0}. Error Code = '0x{1:X}' - \"{2}\".", 
                    this.message, 
                    this.errorCode, 
                    EnumMgr.ToText(this.errorCode) 
                );
            }
        }
    }
}
