// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Insights.InfoProviders;
using Scylla.Net.DataStax.Insights.Schema;
using Scylla.Net.DataStax.Insights.Schema.StatusMessage;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.DataStax.Insights.MessageFactories
{
    internal class InsightsStatusMessageFactory : IInsightsMessageFactory<InsightsStatusData>
    {
        private const string StatusMessageName = "driver.status";
        private const string StatusV1MappingId = "v1";

        private readonly IInsightsMetadataFactory _insightsMetadataFactory;
        private readonly IInsightsInfoProvider<Dictionary<string, NodeStatusInfo>> _connectedNodesInfoProvider;

        public InsightsStatusMessageFactory(
            IInsightsMetadataFactory insightsMetadataFactory,
            IInsightsInfoProvider<Dictionary<string, NodeStatusInfo>> connectedNodesInfoProvider)
        {
            _insightsMetadataFactory = insightsMetadataFactory;
            _connectedNodesInfoProvider = connectedNodesInfoProvider;
        }

        public Insight<InsightsStatusData> CreateMessage(IInternalCluster cluster, IInternalSession session)
        {
            var metadata = _insightsMetadataFactory.CreateInsightsMetadata(
                InsightsStatusMessageFactory.StatusMessageName, InsightsStatusMessageFactory.StatusV1MappingId, InsightType.Event);

            var data = new InsightsStatusData
            {
                ClientId = cluster.Configuration.ClusterId.ToString(),
                SessionId = session.InternalSessionId.ToString(),
                ControlConnection = cluster.Metadata.ControlConnection.EndPoint?.GetHostIpEndPointWithFallback().ToString(),
                ConnectedNodes = _connectedNodesInfoProvider.GetInformation(cluster, session)
            };

            return new Insight<InsightsStatusData>
            {
                Metadata = metadata,
                Data = data
            };
        }
    }
}
