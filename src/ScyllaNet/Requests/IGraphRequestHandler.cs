// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Threading.Tasks;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.ExecutionProfiles;

namespace Scylla.Net.Requests
{
    /// <summary>
    /// Handles graph requests (represented by <see cref="IGraphStatement"/> objects).
    /// </summary>
    internal interface IGraphRequestHandler
    {
        Task<GraphResultSet> SendAsync(IGraphStatement graphStatement, IRequestOptions requestOptions);
    }
}
