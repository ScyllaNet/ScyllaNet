// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Net;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.DataStax.Insights.InfoProviders.StartupMessage
{
    internal class HostnameInfoProvider : IInsightsInfoProvider<string>
    {
        public string GetInformation(IInternalCluster cluster, IInternalSession session)
        {
            return Dns.GetHostName();
        }
    }
}
