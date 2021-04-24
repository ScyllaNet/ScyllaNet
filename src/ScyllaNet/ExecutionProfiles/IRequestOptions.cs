// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.DataStax.Graph;

namespace Scylla.Net.ExecutionProfiles
{
    internal interface IRequestOptions
    {
        ConsistencyLevel ConsistencyLevel { get; }

        ConsistencyLevel SerialConsistencyLevel { get; }

        int ReadTimeoutMillis { get; }

        ILoadBalancingPolicy LoadBalancingPolicy { get; }

        ISpeculativeExecutionPolicy SpeculativeExecutionPolicy { get; }

        IExtendedRetryPolicy RetryPolicy { get; }

        GraphOptions GraphOptions { get; }

        //// next settings don't exist in execution profiles

        int PageSize { get; }

        ITimestampGenerator TimestampGenerator { get; }

        bool DefaultIdempotence { get; }

        int QueryAbortTimeout { get; }
        
        /// <summary>
        /// Gets the serial consistency level of the statement or the default value from the query options.
        /// </summary>
        /// <exception cref="ArgumentException" />
        ConsistencyLevel GetSerialConsistencyLevelOrDefault(IStatement statement);

        int GetQueryAbortTimeout(int amountOfQueries);
    }
}
