// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics.Providers.Null
{
    internal class NullDriverMetricsProvider : IDriverMetricsProvider
    {
        public IDriverTimer Timer(string bucket, IMetric metric)
        {
            return NullDriverTimer.Instance;
        }
        
        public IDriverMeter Meter(string bucket, IMetric metric)
        {
            return NullDriverMeter.Instance;
        }

        public IDriverCounter Counter(string bucket, IMetric metric)
        {
            return NullDriverCounter.Instance;
        }

        public IDriverGauge Gauge(string bucket, IMetric metric, Func<double?> valueProvider)
        {
            return NullDriverGauge.Instance;
        }

        public IDriverGauge Gauge(string bucket, IMetric metric)
        {
            return NullDriverGauge.Instance;
        }

        public void ShutdownMetricsBucket(string bucket)
        {
        }
    }
}
