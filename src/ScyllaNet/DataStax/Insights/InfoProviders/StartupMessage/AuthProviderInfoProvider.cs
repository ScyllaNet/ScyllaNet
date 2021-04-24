// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.DataStax.Insights.Schema.StartupMessage;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.DataStax.Insights.InfoProviders.StartupMessage
{
    internal class AuthProviderInfoProvider : IInsightsInfoProvider<AuthProviderInfo>
    {
        public AuthProviderInfo GetInformation(IInternalCluster cluster, IInternalSession session)
        {
            var type = cluster.Configuration.AuthProvider.GetType();
            return new AuthProviderInfo
            {
                Namespace = type.Namespace,
                Type = type.Name
            };
        }
    }
}
