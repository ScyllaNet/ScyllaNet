// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using Scylla.Net.DataStax.Insights.Schema.StartupMessage;

namespace Scylla.Net.DataStax.Insights.InfoProviders.StartupMessage
{
    internal class SpeculativeExecutionPolicyInfoProvider : IPolicyInfoMapper<ISpeculativeExecutionPolicy>
    {
        static SpeculativeExecutionPolicyInfoProvider()
        {
            SpeculativeExecutionPolicyInfoProvider.SpeculativeExecutionPolicyOptionsProviders
                = new Dictionary<Type, Func<ISpeculativeExecutionPolicy, Dictionary<string, object>>>
                {
                    {
                        typeof(ConstantSpeculativeExecutionPolicy),
                        policy =>
                        {
                            var typedPolicy = (ConstantSpeculativeExecutionPolicy) policy;
                            return new Dictionary<string, object>
                            {
                                { "delay", typedPolicy.Delay }, { "maxSpeculativeExecutions", typedPolicy.MaxSpeculativeExecutions }
                            };
                        }
                    }
                };
        }

        private static IReadOnlyDictionary<Type, Func<ISpeculativeExecutionPolicy, Dictionary<string, object>>> SpeculativeExecutionPolicyOptionsProviders { get; }

        public PolicyInfo GetPolicyInformation(ISpeculativeExecutionPolicy policy)
        {
            return SpeculativeExecutionPolicyInfoProvider.GetSpeculativeExecutionPolicyInfo(policy);
        }

        private static PolicyInfo GetSpeculativeExecutionPolicyInfo(ISpeculativeExecutionPolicy policy)
        {
            var speculativeExecutionPolicyType = policy.GetType();
            SpeculativeExecutionPolicyInfoProvider.SpeculativeExecutionPolicyOptionsProviders.TryGetValue(speculativeExecutionPolicyType, out var speculativeExecutionPolicyOptionsProvider);

            return new PolicyInfo
            {
                Namespace = speculativeExecutionPolicyType.Namespace,
                Type = speculativeExecutionPolicyType.Name,
                Options = speculativeExecutionPolicyOptionsProvider?.Invoke(policy)
            };
        }
    }
}
