// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.DataStax.Insights.InfoProviders.StartupMessage
{
    internal class DataCentersInfoProvider : IInsightsInfoProvider<HashSet<string>>
    {
        public HashSet<string> GetInformation(IInternalCluster cluster, IInternalSession session)
        {
            var dataCenters = new HashSet<string>();
            var remoteConnectionsLength =
                cluster
                    .Configuration
                    .GetOrCreatePoolingOptions(cluster.Metadata.ControlConnection.ProtocolVersion)
                    .GetCoreConnectionsPerHost(HostDistance.Remote);

            foreach (var h in cluster.AllHosts()) 
            {
                if (h.Datacenter == null)
                {
                    continue;
                }

                var distance = cluster.Configuration.Policies.LoadBalancingPolicy.Distance(h);
                if (distance == HostDistance.Local || (distance == HostDistance.Remote && remoteConnectionsLength > 0))
                {
                    dataCenters.Add(h.Datacenter);
                }
            }

            return dataCenters;
        }
    }
}
