// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics.Registries
{
    internal class RequestErrorMetrics : IRequestErrorMetrics
    {
        public RequestErrorMetrics(IInternalMetricsRegistry<NodeMetric> nodeMetricsRegistry, string context)
        {
            Aborted = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.AbortedRequests);
            ReadTimeout = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.ReadTimeouts);
            WriteTimeout = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.WriteTimeouts);
            Unavailable = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.UnavailableErrors);
            Other = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.OtherErrors);
            Unsent = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.UnsentRequests);
            ClientTimeout = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.ClientTimeouts);
            ConnectionInitErrors = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.ConnectionInitErrors);
            AuthenticationErrors = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.AuthenticationErrors);
        }

        public IDriverCounter Aborted { get; }

        public IDriverCounter ReadTimeout { get; }

        public IDriverCounter WriteTimeout { get; }

        public IDriverCounter Unavailable { get; }

        public IDriverCounter ClientTimeout { get; }

        public IDriverCounter Other { get; }

        public IDriverCounter Unsent { get; }

        public IDriverCounter ConnectionInitErrors { get; }

        public IDriverCounter AuthenticationErrors { get; }
    }
}
