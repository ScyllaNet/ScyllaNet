// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Scylla.Net.DataStax.Insights.Schema.Converters;
using Scylla.Net.DataStax.Insights.Schema.StartupMessage;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.DataStax.Insights.InfoProviders.StartupMessage
{
    internal class ExecutionProfileInfoProvider : IInsightsInfoProvider<Dictionary<string, ExecutionProfileInfo>>
    {
        private readonly IPolicyInfoMapper<ILoadBalancingPolicy> _loadBalancingPolicyInfoProvider;
        private readonly IPolicyInfoMapper<ISpeculativeExecutionPolicy> _speculativeExecutionPolicyInfoProvider;
        private readonly IPolicyInfoMapper<IExtendedRetryPolicy> _retryPolicyInfoProvider;

        public ExecutionProfileInfoProvider(
            IPolicyInfoMapper<ILoadBalancingPolicy> loadBalancingPolicyInfoProvider,
            IPolicyInfoMapper<ISpeculativeExecutionPolicy> speculativeExecutionPolicyInfoProvider,
            IPolicyInfoMapper<IExtendedRetryPolicy> retryPolicyInfoProvider)
        {
            _loadBalancingPolicyInfoProvider = loadBalancingPolicyInfoProvider;
            _speculativeExecutionPolicyInfoProvider = speculativeExecutionPolicyInfoProvider;
            _retryPolicyInfoProvider = retryPolicyInfoProvider;
        }
        
        public Dictionary<string, ExecutionProfileInfo> GetInformation(IInternalCluster cluster, IInternalSession session)
        {
            // add default first so that it is on top
            var dict = new Dictionary<string, ExecutionProfileInfo>
            {
                {
                    "default", GetExecutionProfileInfo(
                        cluster.Configuration.ExecutionProfiles[Configuration.DefaultExecutionProfileName])
                }
            };

            foreach (var profile in cluster.Configuration
                                           .ExecutionProfiles
                                           .Where(kvp => kvp.Key != Configuration.DefaultExecutionProfileName))
            {
                dict.Add(profile.Key, GetExecutionProfileInfo(profile.Value));
            }

            return dict;
        }

        private ExecutionProfileInfo GetExecutionProfileInfo(IExecutionProfile profile)
        {
            return new ExecutionProfileInfo
            {
                GraphOptions = GetGraphOptions(profile),
                SerialConsistency = profile.SerialConsistencyLevel,
                Retry = profile.RetryPolicy == null ? null : _retryPolicyInfoProvider.GetPolicyInformation(profile.RetryPolicy),
                SpeculativeExecution = profile.SpeculativeExecutionPolicy == null
                    ? null
                    : _speculativeExecutionPolicyInfoProvider.GetPolicyInformation(profile.SpeculativeExecutionPolicy),
                LoadBalancing = profile.LoadBalancingPolicy == null
                    ? null
                    : _loadBalancingPolicyInfoProvider.GetPolicyInformation(profile.LoadBalancingPolicy),
                ReadTimeout = profile.ReadTimeoutMillis,
                Consistency = profile.ConsistencyLevel
            };
        }

        private Dictionary<string, object> GetGraphOptions(IExecutionProfile profile)
        {
            if (profile.GraphOptions == null)
            {
                return null;
            }

            var consistencyConverter = new ConsistencyInsightsConverter();
            string graphReadConsistency = null;
            if (profile.GraphOptions.ReadConsistencyLevel.HasValue)
            {
                consistencyConverter.TryConvert(profile.GraphOptions.ReadConsistencyLevel.Value, out graphReadConsistency);
            }

            string graphWriteConsistency = null;
            if (profile.GraphOptions.WriteConsistencyLevel.HasValue)
            {
                consistencyConverter.TryConvert(profile.GraphOptions.WriteConsistencyLevel.Value, out graphWriteConsistency);
            }

            return new Dictionary<string, object>
            {
                { "language", profile.GraphOptions.Language },
                { "source", profile.GraphOptions.Source },
                { "name", profile.GraphOptions.Name },
                { "readConsistency", graphReadConsistency },
                { "writeConsistency", graphWriteConsistency },
                { "readTimeout", profile.GraphOptions.ReadTimeoutMillis }
            };
        }
    }
}
