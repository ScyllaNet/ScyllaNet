// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.DataStax.Insights.Schema.StartupMessage;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.DataStax.Insights.InfoProviders.StartupMessage
{
    internal class PoolSizeByHostDistanceInfoProvider : IInsightsInfoProvider<PoolSizeByHostDistance>
    {
        public PoolSizeByHostDistance GetInformation(IInternalCluster cluster, IInternalSession session)
        {
            return new PoolSizeByHostDistance
            {
                Local = cluster
                        .Configuration
                        .GetOrCreatePoolingOptions(cluster.Metadata.ControlConnection.ProtocolVersion)
                        .GetCoreConnectionsPerHost(HostDistance.Local),
                Remote = cluster
                         .Configuration
                         .GetOrCreatePoolingOptions(cluster.Metadata.ControlConnection.ProtocolVersion)
                         .GetCoreConnectionsPerHost(HostDistance.Remote)
            };
        }
    }
}
