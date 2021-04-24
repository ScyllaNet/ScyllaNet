// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Metrics.Abstractions
{
    /// <summary>
    /// Provides metric implementations.
    /// </summary>
    public interface IDriverMetricsProvider
    {
        /// <summary>
        /// Creates a timer metric. <paramref name="bucket"/> will contain the prefix configured with <see cref="DriverMetricsOptions.SetBucketPrefix"/>,
        /// session name and node's address. Node's address will only be in the bucket name when <paramref name="metric"/> is a <see cref="NodeMetric"/>.
        /// Implementations can call <see cref="object.Equals(object)"/> to test if <paramref name="metric"/> is a particular <see cref="NodeMetric"/> or <see cref="SessionMetric"/>.
        /// </summary>
        IDriverTimer Timer(string bucket, IMetric metric);
        
        /// <summary>
        /// Creates a meter metric. <paramref name="bucket"/> will contain the prefix configured with <see cref="DriverMetricsOptions.SetBucketPrefix"/>,
        /// session name and node's address. Node's address will only be in the bucket name when <paramref name="metric"/> is a <see cref="NodeMetric"/>.
        /// Implementations can call <see cref="object.Equals(object)"/> to test if <paramref name="metric"/> is a particular <see cref="NodeMetric"/> or <see cref="SessionMetric"/>.
        /// </summary>
        IDriverMeter Meter(string bucket, IMetric metric);
        
        /// <summary>
        /// Creates a counter metric. <paramref name="bucket"/> will contain the prefix configured with <see cref="DriverMetricsOptions.SetBucketPrefix"/>,
        /// session name and node's address. Node's address will only be in the bucket name when <paramref name="metric"/> is a <see cref="NodeMetric"/>.
        /// Implementations can call <see cref="object.Equals(object)"/> to test if <paramref name="metric"/> is a particular <see cref="NodeMetric"/> or <see cref="SessionMetric"/>.
        /// </summary>
        IDriverCounter Counter(string bucket, IMetric metric);
        
        /// <summary>
        /// Creates a gauge metric. <paramref name="bucket"/> will contain the prefix configured with <see cref="DriverMetricsOptions.SetBucketPrefix"/>,
        /// session name and node's address. Node's address will only be in the bucket name when <paramref name="metric"/> is a <see cref="NodeMetric"/>.
        /// Implementations can call <see cref="object.Equals(object)"/> to test if <paramref name="metric"/> is a particular <see cref="NodeMetric"/> or <see cref="SessionMetric"/>.
        /// </summary>
        IDriverGauge Gauge(string bucket, IMetric metric, Func<double?> valueProvider);
        
        /// <summary>
        /// Shutdowns/Disposes metrics created with the provided <paramref name="bucket"/>. This is especially useful to stop gauges which are supposed to run on a loop.
        /// </summary>
        void ShutdownMetricsBucket(string bucket);
    }
}
