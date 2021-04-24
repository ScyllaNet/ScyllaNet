// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using Scylla.Net.DataStax.Insights.Schema.StartupMessage;

namespace Scylla.Net.DataStax.Insights.InfoProviders.StartupMessage
{
    internal class LoadBalancingPolicyInfoProvider : IPolicyInfoMapper<ILoadBalancingPolicy>
    {
        static LoadBalancingPolicyInfoProvider()
        {
            LoadBalancingPolicyInfoProvider.LoadBalancingPolicyOptionsProviders
                = new Dictionary<Type, Func<ILoadBalancingPolicy, IPolicyInfoMapper<IReconnectionPolicy>, Dictionary<string, object>>>
            {
                {
                    typeof(DCAwareRoundRobinPolicy),
                    (policy, reconnectionPolicyInfoMapper) =>
                    {
                        var typedPolicy = (DCAwareRoundRobinPolicy) policy;
                        return new Dictionary<string, object>
                        {
#pragma warning disable 618
                            { "localDc", typedPolicy.LocalDc }, { "usedHostsPerRemoteDc", typedPolicy.UsedHostsPerRemoteDc }
#pragma warning restore 618
                        };
                    }
                },
                {
                    typeof(DefaultLoadBalancingPolicy),
                    (policy, reconnectionPolicyInfoMapper) =>
                    {
                        var typedPolicy = (DefaultLoadBalancingPolicy) policy;
                        return new Dictionary<string, object>
                        {
                            { "childPolicy", LoadBalancingPolicyInfoProvider.GetLoadBalancingPolicyInfo(typedPolicy.ChildPolicy, reconnectionPolicyInfoMapper) }
                        };
                    }
                },
                {
                    typeof(RetryLoadBalancingPolicy),
                    (policy, reconnectionPolicyInfoMapper) =>
                    {
                        var typedPolicy = (RetryLoadBalancingPolicy) policy;
                        return new Dictionary<string, object>
                        {
                            { "reconnectionPolicy", reconnectionPolicyInfoMapper.GetPolicyInformation(typedPolicy.ReconnectionPolicy) },
                            { "loadBalancingPolicy", LoadBalancingPolicyInfoProvider.GetLoadBalancingPolicyInfo(typedPolicy.LoadBalancingPolicy, reconnectionPolicyInfoMapper) }
                        };
                    }
                },
                {
                    typeof(TokenAwarePolicy),
                    (policy, reconnectionPolicyInfoMapper) =>
                    {
                        var typedPolicy = (TokenAwarePolicy) policy;
                        return new Dictionary<string, object>
                        {
                            { "childPolicy", LoadBalancingPolicyInfoProvider.GetLoadBalancingPolicyInfo(typedPolicy.ChildPolicy, reconnectionPolicyInfoMapper) }
                        };
                    }
                }
            };
        }

        private static IReadOnlyDictionary<Type, Func<ILoadBalancingPolicy, IPolicyInfoMapper<IReconnectionPolicy>, Dictionary<string, object>>> LoadBalancingPolicyOptionsProviders { get; }

        private readonly IPolicyInfoMapper<IReconnectionPolicy> _reconnectionPolicyInfoMapper;

        public LoadBalancingPolicyInfoProvider(IPolicyInfoMapper<IReconnectionPolicy> reconnectionPolicyInfoMapper)
        {
            _reconnectionPolicyInfoMapper = reconnectionPolicyInfoMapper;
        }

        public PolicyInfo GetPolicyInformation(ILoadBalancingPolicy policy)
        {
            return LoadBalancingPolicyInfoProvider.GetLoadBalancingPolicyInfo(policy, _reconnectionPolicyInfoMapper);
        }

        private static PolicyInfo GetLoadBalancingPolicyInfo(ILoadBalancingPolicy policy, IPolicyInfoMapper<IReconnectionPolicy> reconnectionPolicyInfoMapper)
        {
            var loadBalancingPolicyType = policy.GetType();
            LoadBalancingPolicyInfoProvider.LoadBalancingPolicyOptionsProviders.TryGetValue(loadBalancingPolicyType, out var loadBalancingPolicyOptionsProvider);

            return new PolicyInfo
            {
                Namespace = loadBalancingPolicyType.Namespace,
                Type = loadBalancingPolicyType.Name,
                Options = loadBalancingPolicyOptionsProvider?.Invoke(policy, reconnectionPolicyInfoMapper)
            };
        }
    }
}
