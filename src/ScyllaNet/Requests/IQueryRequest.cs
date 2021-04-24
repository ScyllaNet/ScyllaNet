// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Requests
{
    /// <summary>
    /// Represents a QUERY or EXECUTE request that can be included in a batch
    /// </summary>
    internal interface IQueryRequest : IRequest
    {
        /// <summary>
        /// The paging state for the request
        /// </summary>
        byte[] PagingState { get; set; }

        /// <summary>
        /// Whether the skip_metadata flag is set for this request.
        /// </summary>
        bool SkipMetadata { get; }

        /// <summary>
        /// Method used by the batch to build each individual request
        /// </summary>
        void WriteToBatch(FrameWriter writer);
    }
}
