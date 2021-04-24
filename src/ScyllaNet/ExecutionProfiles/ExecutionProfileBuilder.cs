// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.DataStax.Graph;

namespace Scylla.Net.ExecutionProfiles
{
    internal class ExecutionProfileBuilder : IExecutionProfileBuilder
    {
        private int? _readTimeoutMillis;
        private ConsistencyLevel? _consistencyLevel;
        private ConsistencyLevel? _serialConsistencyLevel;
        private ILoadBalancingPolicy _loadBalancingPolicy;
        private ISpeculativeExecutionPolicy _speculativeExecutionPolicy;
        private IExtendedRetryPolicy _retryPolicy;
        
        private GraphOptions _graphOptions;
        
        public IExecutionProfileBuilder WithLoadBalancingPolicy(ILoadBalancingPolicy loadBalancingPolicy)
        {
            _loadBalancingPolicy = loadBalancingPolicy ?? throw new ArgumentNullException(nameof(loadBalancingPolicy));
            return this;
        }

        public IExecutionProfileBuilder WithRetryPolicy(IExtendedRetryPolicy retryPolicy)
        {
            _retryPolicy = retryPolicy ?? throw new ArgumentNullException(nameof(retryPolicy));
            return this;
        }

        public IExecutionProfileBuilder WithSpeculativeExecutionPolicy(ISpeculativeExecutionPolicy speculativeExecutionPolicy)
        {
            _speculativeExecutionPolicy = speculativeExecutionPolicy ?? throw new ArgumentNullException(nameof(speculativeExecutionPolicy));
            return this;
        }

        public IExecutionProfileBuilder WithConsistencyLevel(ConsistencyLevel consistencyLevel)
        {
            _consistencyLevel = consistencyLevel;
            return this;
        }
        
        public IExecutionProfileBuilder WithSerialConsistencyLevel(ConsistencyLevel serialConsistencyLevel)
        {
            _serialConsistencyLevel = serialConsistencyLevel;
            return this;
        }
        
        public IExecutionProfileBuilder WithReadTimeoutMillis(int readTimeoutMillis)
        {
            _readTimeoutMillis = readTimeoutMillis;
            return this;
        }

        /// <inheritdoc />
        public IExecutionProfileBuilder WithGraphOptions(GraphOptions graphOptions)
        {
            _graphOptions = graphOptions;
            return this;
        }

        public IExecutionProfile Build()
        {
            return new ExecutionProfile(
                _consistencyLevel,
                _serialConsistencyLevel,
                _readTimeoutMillis,
                _loadBalancingPolicy,
                _speculativeExecutionPolicy,
                _retryPolicy,
                _graphOptions);
        }
    }
}
