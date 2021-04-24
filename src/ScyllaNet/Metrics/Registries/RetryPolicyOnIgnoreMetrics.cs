// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics.Registries
{
    internal class RetryPolicyOnIgnoreMetrics : IRetryPolicyMetrics
    {
        public RetryPolicyOnIgnoreMetrics(IInternalMetricsRegistry<NodeMetric> nodeMetricsRegistry, string context)
        {
            ReadTimeout = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.IgnoresOnReadTimeout);
            WriteTimeout = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.IgnoresOnWriteTimeout);
            Unavailable = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.IgnoresOnUnavailable);
            Other = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.IgnoresOnOtherError);
            Total = nodeMetricsRegistry.Counter(context, NodeMetric.Counters.Ignores);
        }

        public IDriverCounter ReadTimeout { get; }

        public IDriverCounter WriteTimeout { get; }

        public IDriverCounter Unavailable { get; }

        public IDriverCounter Other { get; }

        public IDriverCounter Total { get; }
    }
}
