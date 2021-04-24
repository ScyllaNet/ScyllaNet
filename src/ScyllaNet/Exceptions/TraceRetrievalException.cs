// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    /// <summary>
    ///  Exception thrown if a query trace cannot be retrieved.
    /// </summary>
    public class TraceRetrievalException : DriverException
    {
        public TraceRetrievalException(string message)
            : base(message)
        {
        }

        public TraceRetrievalException(string message, Exception cause)
            : base(message, cause)
        {
        }
    }
}
