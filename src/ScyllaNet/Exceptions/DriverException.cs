// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Runtime.Serialization;

namespace Scylla.Net
{
    /// <summary>
    /// Top level class for exceptions thrown by the driver.
    /// </summary>
    [Serializable]
    public class DriverException : Exception
    {
        public DriverException(string message)
            : base(message)
        {
        }

        public DriverException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        
        protected DriverException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
            
        }
    }
}
