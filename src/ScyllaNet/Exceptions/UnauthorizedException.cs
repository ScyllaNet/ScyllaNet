// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    ///  Indicates that a query cannot be performed due to the authorisation restrictions of the logged user.
    /// </summary>
    public class UnauthorizedException : QueryValidationException
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}
