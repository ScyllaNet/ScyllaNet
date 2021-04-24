// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Insights.Schema.StatusMessage;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.DataStax.Insights.InfoProviders.StatusMessage
{
    internal class NodeStatusInfoProvider : IInsightsInfoProvider<Dictionary<string, NodeStatusInfo>>
    {
        public Dictionary<string, NodeStatusInfo> GetInformation(IInternalCluster cluster, IInternalSession session)
        {
            var nodeStatusDictionary = new Dictionary<string, NodeStatusInfo>();
            var state = session.GetState();
            var connectedHosts = state.GetConnectedHosts();

            foreach (var h in connectedHosts)
            {
                var inFlightQueries = state.GetInFlightQueries(h);
                var openConnections = state.GetOpenConnections(h);
                nodeStatusDictionary.Add(
                    h.Address.ToString(),
                    new NodeStatusInfo
                    {
                        Connections = openConnections,
                        InFlightQueries = inFlightQueries
                    });
            }

            return nodeStatusDictionary;
        }
    }
}
