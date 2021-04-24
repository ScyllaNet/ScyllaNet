// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    ///  A specific invalid query exception that indicates that the query is invalid
    ///  because of some configuration problem. <p> This is generally throw by query
    ///  that manipulate the schema (CREATE and ALTER) when the required configuration
    ///  options are invalid.</p>
    /// </summary>
    public class InvalidConfigurationInQueryException : InvalidQueryException
    {
        public InvalidConfigurationInQueryException(string message) : base(message)
        {
        }
    }
}
