// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.DataStax.Insights.InfoProviders.StartupMessage
{
    internal class ConfigAntiPatternsInfoProvider : IInsightsInfoProvider<Dictionary<string, string>>
    {
        static ConfigAntiPatternsInfoProvider()
        {
            ConfigAntiPatternsInfoProvider.AntiPatternsProviders = new Dictionary<Type, Func<object, Dictionary<string, string>, Dictionary<string, string>>>
            {
                { 
                    typeof(DCAwareRoundRobinPolicy), 
                    (obj, antiPatterns) =>
                    {
                        var typedPolicy = (DCAwareRoundRobinPolicy) obj;
#pragma warning disable 618
                        if (typedPolicy.UsedHostsPerRemoteDc > 0)
#pragma warning restore 618
                        {
                            antiPatterns["useRemoteHosts"] = "Using remote hosts for fail-over";
                        }

                        return antiPatterns;
                    }
                },
                { 
#pragma warning disable 618
                    typeof(DowngradingConsistencyRetryPolicy),
#pragma warning restore 618
                    (obj, antiPatterns) =>
                    {
                        antiPatterns["downgradingConsistency"] = "Downgrading consistency retry policy in use";
                        return antiPatterns;
                    }
                },
                { 
                    typeof(DefaultLoadBalancingPolicy), 
                    (obj, antiPatterns) =>
                    {
                        var typedPolicy = (DefaultLoadBalancingPolicy) obj;
                        return ConfigAntiPatternsInfoProvider.AddAntiPatterns(typedPolicy.ChildPolicy, antiPatterns);
                    }
                },
                { 
                    typeof(RetryLoadBalancingPolicy), 
                    (obj, antiPatterns) =>
                    {
                        var typedPolicy = (RetryLoadBalancingPolicy) obj;
                        antiPatterns =  ConfigAntiPatternsInfoProvider.AddAntiPatterns(typedPolicy.ReconnectionPolicy, antiPatterns);
                        return ConfigAntiPatternsInfoProvider.AddAntiPatterns(typedPolicy.LoadBalancingPolicy, antiPatterns);
                    }
                },
                { 
                    typeof(TokenAwarePolicy), 
                    (obj, antiPatterns) =>
                    {
                        var typedPolicy = (TokenAwarePolicy) obj;
                        return ConfigAntiPatternsInfoProvider.AddAntiPatterns(typedPolicy.ChildPolicy, antiPatterns);
                    }
                },
                {
                    typeof(IdempotenceAwareRetryPolicy),
                    (obj, antiPatterns) =>
                    {
                        var typedPolicy = (IdempotenceAwareRetryPolicy) obj;
                        return ConfigAntiPatternsInfoProvider.AddAntiPatterns(typedPolicy.ChildPolicy, antiPatterns);
                    }
                },
                {
                    typeof(LoggingRetryPolicy),
                    (obj, antiPatterns) =>
                    {
                        var typedPolicy = (LoggingRetryPolicy) obj;
                        return ConfigAntiPatternsInfoProvider.AddAntiPatterns(typedPolicy.ChildPolicy, antiPatterns);
                    }
                },
                {
                    typeof(RetryPolicyExtensions.WrappedExtendedRetryPolicy),
                    (obj, antiPatterns) =>
                    {
                        var typedPolicy = (RetryPolicyExtensions.WrappedExtendedRetryPolicy) obj;
                        antiPatterns =  ConfigAntiPatternsInfoProvider.AddAntiPatterns(typedPolicy.Policy, antiPatterns);
                        return ConfigAntiPatternsInfoProvider.AddAntiPatterns(typedPolicy.DefaultPolicy, antiPatterns);
                    }
                }
            };
        }

        public static IReadOnlyDictionary<Type, Func<object, Dictionary<string, string>, Dictionary<string, string>>> AntiPatternsProviders { get; }

        public Dictionary<string, string> GetInformation(IInternalCluster cluster, IInternalSession session)
        {
            var antiPatterns = new Dictionary<string, string>();

            var resolvedContactPoints = cluster.Metadata.ResolvedContactPoints;

            var contactPointsEndPoints = resolvedContactPoints
                                         .Values
                                         .SelectMany(endPoints => endPoints)
                                         .Select(c => c.GetHostIpEndPointWithFallback())
                                         .ToList();

            var contactPointsHosts = cluster
                                     .AllHosts()
                                     .Where(host => (host.ContactPoint != null && resolvedContactPoints.ContainsKey(host.ContactPoint)) 
                                                    || contactPointsEndPoints.Contains(host.Address))
                                     .ToList();

            if (contactPointsHosts.Select(c => c.Datacenter).Where(dc => dc != null).Distinct().Count() > 1)
            {
                antiPatterns["contactPointsMultipleDCs"] = "Contact points contain hosts from multiple data centers";
            }

            var loadBalancingPolicy = cluster.Configuration.Policies.LoadBalancingPolicy;
            antiPatterns = ConfigAntiPatternsInfoProvider.AddAntiPatterns(loadBalancingPolicy, antiPatterns);

            var retryPolicy = cluster.Configuration.Policies.RetryPolicy;
            antiPatterns = ConfigAntiPatternsInfoProvider.AddAntiPatterns(retryPolicy, antiPatterns);

            return antiPatterns;
        }

        private static Dictionary<string, string> AddAntiPatterns(object obj, Dictionary<string, string> antiPatterns)
        {
            return ConfigAntiPatternsInfoProvider.AntiPatternsProviders.TryGetValue(obj.GetType(), out var provider) 
                ? provider.Invoke(obj, antiPatterns) 
                : antiPatterns;
        }
    }
}
