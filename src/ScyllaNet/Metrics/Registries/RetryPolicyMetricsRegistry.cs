// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics.Registries
{
    internal class RetryPolicyOnRetryMetrics : IRetryPolicyMetrics
    {
        public RetryPolicyOnRetryMetrics(IInternalMetricsRegistry<NodeMetric> nodeMetricsRegistry, string context)
        {
            ReadTimeout = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.RetriesOnReadTimeout);
            WriteTimeout = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.RetriesOnWriteTimeout);
            Unavailable = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.RetriesOnUnavailable);
            Other = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.RetriesOnOtherError);
            Total = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.Retries);
        }

        public IDriverCounter ReadTimeout { get; }

        public IDriverCounter WriteTimeout { get; }

        public IDriverCounter Unavailable { get; }

        public IDriverCounter Other { get; }

        public IDriverCounter Total { get; }
    }
}
