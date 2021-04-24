// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    /// <summary>
    ///  Indicates a syntactically correct but invalid query.
    /// </summary>
    public class InvalidQueryException : QueryValidationException
    {
        public InvalidQueryException(string message)
            : base(message)
        {
        }

        public InvalidQueryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
