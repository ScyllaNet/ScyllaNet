// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Runtime.Serialization;

// ReSharper disable once CheckNamespace
namespace Scylla.Net
{
    /// <summary>
    /// Specifies a User defined function execution failure.
    /// </summary>
    public class FunctionFailureException : DriverException
    {
        /// <summary>
        /// Keyspace where the function is defined
        /// </summary>
        public string Keyspace { get; set; }

        /// <summary>
        /// Name of the function
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name types of the arguments
        /// </summary>
        public string[] ArgumentTypes { get; set; }

        public FunctionFailureException(string message) : base(message)
        {
        }

        public FunctionFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
        
        protected FunctionFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
