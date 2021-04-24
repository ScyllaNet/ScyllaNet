// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net;

namespace Scylla.Net
{
    public class ExecutionException : DriverException
    {
        public Dictionary<IPAddress, Exception> InnerInnerExceptions { get; private set; }

        public ExecutionException(string message, Exception innerException = null, Dictionary<IPAddress, Exception> innerInnerExceptions = null)
            : base(message, innerException)
        {
            InnerInnerExceptions = innerInnerExceptions;
        }
    }
}
