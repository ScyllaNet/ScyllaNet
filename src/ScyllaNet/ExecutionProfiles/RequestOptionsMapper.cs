// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Scylla.Net.DataStax.Graph;

namespace Scylla.Net.ExecutionProfiles
{
    /// <inheritdoc />
    internal class RequestOptionsMapper : IRequestOptionsMapper
    {
        /// <inheritdoc />
        public IReadOnlyDictionary<string, IRequestOptions> BuildRequestOptionsDictionary(
            IReadOnlyDictionary<string, IExecutionProfile> executionProfiles,
            Policies policies,
            SocketOptions socketOptions,
            ClientOptions clientOptions,
            QueryOptions queryOptions,
            GraphOptions graphOptions)
        {
            executionProfiles.TryGetValue(Configuration.DefaultExecutionProfileName, out var defaultProfile);
            var requestOptions =
                executionProfiles
                    .Where(kvp => kvp.Key != Configuration.DefaultExecutionProfileName)
                    .ToDictionary<KeyValuePair<string, IExecutionProfile>, string, IRequestOptions>(
                        kvp => kvp.Key,
                        kvp => new RequestOptions(kvp.Value, defaultProfile, policies, socketOptions, queryOptions, clientOptions, graphOptions));

            requestOptions.Add(
                Configuration.DefaultExecutionProfileName, 
                new RequestOptions(null, defaultProfile, policies, socketOptions, queryOptions, clientOptions, graphOptions));
            return requestOptions;
        }
    }
}
