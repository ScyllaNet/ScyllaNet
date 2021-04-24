// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Requests
{
    /// <summary>
    /// Represents an CQL Request (BATCH, EXECUTE or QUERY)
    /// </summary>
    internal interface ICqlRequest : IRequest
    {
        /// <summary>
        /// Gets or sets the Consistency for the Request.
        /// It defaults to the one provided by the Statement but it can be changed by the retry policy.
        /// </summary>
        ConsistencyLevel Consistency { get; set; }
    }
}
