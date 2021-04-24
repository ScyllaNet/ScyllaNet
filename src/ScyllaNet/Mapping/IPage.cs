// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.Mapping
{
    /// <summary>
    /// Represents the result of a paged query, returned by manually paged query executions.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Naming", 
        "CA1710:Identifiers should have correct suffix", 
        Justification = "Public API")]
    public interface IPage<T> : ICollection<T>
    {
        /// <summary>
        /// Returns a token representing the state used to retrieve this results.
        /// </summary>
        byte[] CurrentPagingState { get; }
        /// <summary>
        /// Returns a token representing the state to retrieve the next page of results.
        /// </summary>
        byte[] PagingState { get; }
    }
}
