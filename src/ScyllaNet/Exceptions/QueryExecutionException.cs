// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    /// <summary>
    ///  Exception related to the execution of a query. This correspond to the
    ///  exception that Cassandra throw when a (valid) query cannot be executed
    ///  (TimeoutException, UnavailableException, ...).
    /// </summary>
    public abstract class QueryExecutionException : QueryValidationException
    {
        public QueryExecutionException(string message)
            : base(message)
        {
        }

        public QueryExecutionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
