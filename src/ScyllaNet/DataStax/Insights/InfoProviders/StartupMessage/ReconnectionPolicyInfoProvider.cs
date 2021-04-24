// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using Scylla.Net.DataStax.Insights.Schema.StartupMessage;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.DataStax.Insights.InfoProviders.StartupMessage
{
    internal class ReconnectionPolicyInfoProvider : IPolicyInfoMapper<IReconnectionPolicy>, IInsightsInfoProvider<PolicyInfo>
    {
        static ReconnectionPolicyInfoProvider()
        {
            ReconnectionPolicyInfoProvider.PolicyOptionsProviders = new Dictionary<Type, Func<IReconnectionPolicy, Dictionary<string, object>>>
            {
                { 
                    typeof(ConstantReconnectionPolicy), 
                    policy =>
                    {
                        var typedPolicy = (ConstantReconnectionPolicy) policy;
                        return new Dictionary<string, object> {{ "constantDelayMs", typedPolicy.ConstantDelayMs }};
                    }
                },
                { 
                    typeof(ExponentialReconnectionPolicy), 
                    policy =>
                    {
                        var typedPolicy = (ExponentialReconnectionPolicy) policy;
                        return new Dictionary<string, object> {{ "baseDelayMs", typedPolicy.BaseDelayMs }, { "maxDelayMs", typedPolicy.MaxDelayMs }};
                    }
                },
                { 
                    typeof(FixedReconnectionPolicy), 
                    policy =>
                    {
                        var typedPolicy = (FixedReconnectionPolicy) policy;
                        return new Dictionary<string, object> {{ "delays", typedPolicy.Delays }};
                    }
                }
            };
        }

        public static IReadOnlyDictionary<Type, Func<IReconnectionPolicy, Dictionary<string, object>>> PolicyOptionsProviders { get; }

        public PolicyInfo GetInformation(IInternalCluster cluster, IInternalSession session)
        {
            var policy = cluster.Configuration.Policies.ReconnectionPolicy;
            var type = policy.GetType();
            ReconnectionPolicyInfoProvider.PolicyOptionsProviders.TryGetValue(type, out var optionsProvider);
            return new PolicyInfo
            {
                Namespace = type.Namespace,
                Type = type.Name,
                Options = optionsProvider?.Invoke(policy)
            };
        }

        public PolicyInfo GetPolicyInformation(IReconnectionPolicy policy)
        {
            var type = policy.GetType();
            ReconnectionPolicyInfoProvider.PolicyOptionsProviders.TryGetValue(type, out var optionsProvider);
            return new PolicyInfo
            {
                Namespace = type.Namespace,
                Type = type.Name,
                Options = optionsProvider?.Invoke(policy)
            };
        }
    }
}
