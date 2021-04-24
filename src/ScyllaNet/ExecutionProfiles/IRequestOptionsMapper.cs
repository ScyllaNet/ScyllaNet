// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Graph;

namespace Scylla.Net.ExecutionProfiles
{
    /// <summary>
    /// Component that builds <see cref="IRequestOptions"/> instances from the provided <see cref="IExecutionProfile"/> instances.
    /// </summary>
    internal interface IRequestOptionsMapper
    {
        /// <summary>
        /// Converts execution profile instances to RequestOptions which handle the fallback logic
        /// therefore guaranteeing that the settings are non null.
        /// </summary>
        IReadOnlyDictionary<string, IRequestOptions> BuildRequestOptionsDictionary(
            IReadOnlyDictionary<string, IExecutionProfile> executionProfiles,
            Policies policies,
            SocketOptions socketOptions,
            ClientOptions clientOptions,
            QueryOptions queryOptions,
            GraphOptions graphOptions);
    }
}
