// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Insights.InfoProviders.StartupMessage;
using Scylla.Net.DataStax.Insights.Schema.StartupMessage;

namespace Scylla.Net.DataStax.Insights.InfoProviders
{
    internal class InsightsInfoProvidersCollection
    {
        public InsightsInfoProvidersCollection(
            IInsightsInfoProvider<InsightsPlatformInfo> platformInfoProvider,
            IInsightsInfoProvider<Dictionary<string, ExecutionProfileInfo>> executionProfileInfoProvider,
            IInsightsInfoProvider<PoolSizeByHostDistance> poolSizeByHostDistanceInfoProvider,
            IInsightsInfoProvider<AuthProviderInfo> authProviderInfoProvider,
            IInsightsInfoProvider<HashSet<string>> dataCentersInfoProvider,
            IInsightsInfoProvider<Dictionary<string, object>> otherOptionsInfoProvider,
            IInsightsInfoProvider<Dictionary<string, string>> configAntiPatternsInfoProvider,
            IInsightsInfoProvider<PolicyInfo> reconnectionPolicyInfoProvider,
            IInsightsInfoProvider<DriverInfo> driverInfoProvider,
            IInsightsInfoProvider<string> hostnameProvider)
        {
            PlatformInfoProvider = platformInfoProvider;
            ExecutionProfileInfoProvider = executionProfileInfoProvider;
            PoolSizeByHostDistanceInfoProvider = poolSizeByHostDistanceInfoProvider;
            AuthProviderInfoProvider = authProviderInfoProvider;
            DataCentersInfoProvider = dataCentersInfoProvider;
            OtherOptionsInfoProvider = otherOptionsInfoProvider;
            ConfigAntiPatternsInfoProvider = configAntiPatternsInfoProvider;
            ReconnectionPolicyInfoProvider = reconnectionPolicyInfoProvider;
            DriverInfoProvider = driverInfoProvider;
            HostnameProvider = hostnameProvider;
        }

        public IInsightsInfoProvider<InsightsPlatformInfo> PlatformInfoProvider { get; }

        public IInsightsInfoProvider<Dictionary<string, ExecutionProfileInfo>> ExecutionProfileInfoProvider { get; }

        public IInsightsInfoProvider<PoolSizeByHostDistance> PoolSizeByHostDistanceInfoProvider { get; }

        public IInsightsInfoProvider<AuthProviderInfo> AuthProviderInfoProvider { get; }

        public IInsightsInfoProvider<HashSet<string>> DataCentersInfoProvider { get; }

        public IInsightsInfoProvider<Dictionary<string, object>> OtherOptionsInfoProvider { get; }

        public IInsightsInfoProvider<Dictionary<string, string>> ConfigAntiPatternsInfoProvider { get; }

        public IInsightsInfoProvider<PolicyInfo> ReconnectionPolicyInfoProvider { get; }

        public IInsightsInfoProvider<DriverInfo> DriverInfoProvider { get; }

        public IInsightsInfoProvider<string> HostnameProvider { get; }
    }
}
