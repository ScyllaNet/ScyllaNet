// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using Scylla.Net.DataStax.Insights.Schema.StartupMessage;

namespace Scylla.Net.DataStax.Insights.InfoProviders.StartupMessage
{
    internal class RetryPolicyInfoProvider : IPolicyInfoMapper<IExtendedRetryPolicy>
    {
        static RetryPolicyInfoProvider()
        {
            RetryPolicyInfoProvider.RetryPolicyOptionsProviders = new Dictionary<Type, Func<IRetryPolicy, Dictionary<string, object>>>
            {
                {
                    typeof(IdempotenceAwareRetryPolicy),
                    policy =>
                    {
                        var typedPolicy = (IdempotenceAwareRetryPolicy) policy;
                        return new Dictionary<string, object>
                        {
                            { "childPolicy", RetryPolicyInfoProvider.GetRetryPolicyInfo(typedPolicy.ChildPolicy) }
                        };
                    }
                },
                {
                    typeof(LoggingRetryPolicy),
                    policy =>
                    {
                        var typedPolicy = (LoggingRetryPolicy) policy;
                        return new Dictionary<string, object>
                        {
                            { "childPolicy", RetryPolicyInfoProvider.GetRetryPolicyInfo(typedPolicy.ChildPolicy) }
                        };
                    }
                }
            };
        }

        public PolicyInfo GetPolicyInformation(IExtendedRetryPolicy policy)
        {
            return RetryPolicyInfoProvider.GetRetryPolicyInfo(policy);
        }

        private static IReadOnlyDictionary<Type, Func<IRetryPolicy, Dictionary<string, object>>> RetryPolicyOptionsProviders { get; }

        private static PolicyInfo GetRetryPolicyInfo(IRetryPolicy policy)
        {
            var retryPolicyType = policy.GetType();

            if (retryPolicyType == typeof(RetryPolicyExtensions.WrappedExtendedRetryPolicy))
            {
                var typedPolicy = (RetryPolicyExtensions.WrappedExtendedRetryPolicy) policy;
                return RetryPolicyInfoProvider.GetRetryPolicyInfo(typedPolicy.Policy);
            }

            RetryPolicyInfoProvider.RetryPolicyOptionsProviders.TryGetValue(retryPolicyType, out var retryPolicyOptionsProvider);
            return new PolicyInfo
            {
                Namespace = retryPolicyType.Namespace,
                Type = retryPolicyType.Name,
                Options = retryPolicyOptionsProvider?.Invoke(policy)
            };
        }
    }
}
