// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics.Registries
{
    /// <summary>
    /// Internal metrics registry that creates metrics (using the configured <see cref="IDriverMetricsProvider"/>).
    /// Also filters the metrics and returns a null implementation if the metric is disabled.
    /// </summary>
    /// <typeparam name="TMetric"></typeparam>
    internal interface IInternalMetricsRegistry<TMetric> : IMetricsRegistry<TMetric> where TMetric : IMetric
    {
        IDriverTimer Timer(string bucket, TMetric metric);
        
        IDriverMeter Meter(string bucket, TMetric metric);

        IDriverCounter Counter(string bucket, TMetric metric);

        IDriverGauge Gauge(string bucket, TMetric metric, Func<double?> valueProvider);

        IDriverMetric GetMetric(TMetric metric);

        /// <summary>
        /// Used to notify the registry that no more metrics will be added. (Concurrency optimization).
        /// </summary>
        void OnMetricsAdded();
    }
}
