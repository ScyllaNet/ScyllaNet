// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.DataStax.Insights.MessageFactories;
using Scylla.Net.DataStax.Insights.Schema.StartupMessage;
using Scylla.Net.DataStax.Insights.Schema.StatusMessage;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.DataStax.Insights
{
    internal class InsightsClientFactory : IInsightsClientFactory
    {
        private readonly IInsightsMessageFactory<InsightsStartupData> _startupMessageFactory;
        private readonly IInsightsMessageFactory<InsightsStatusData> _statusMessageFactory;

        public InsightsClientFactory(
            IInsightsMessageFactory<InsightsStartupData> startupMessageFactory,
            IInsightsMessageFactory<InsightsStatusData> statusMessageFactory)
        {
            _startupMessageFactory = startupMessageFactory;
            _statusMessageFactory = statusMessageFactory;
        }

        public IInsightsClient Create(IInternalCluster cluster, IInternalSession session)
        {
            return new InsightsClient(cluster, session, _startupMessageFactory, _statusMessageFactory);
        }
    }
}
