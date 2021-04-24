// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    /// <summary>
    ///  An exception indicating that a query cannot be executed because it is
    ///  incorrect syntactically, invalid, unauthorized or any other reason.
    /// </summary>
    public abstract class QueryValidationException : DriverException
    {
        public QueryValidationException(string message)
            : base(message)
        {
        }

        public QueryValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
