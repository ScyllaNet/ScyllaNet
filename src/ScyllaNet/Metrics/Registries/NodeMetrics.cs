// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Linq;
using Scylla.Net.Connections;
using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics.Registries
{
    /// <inheritdoc />
    internal class NodeMetrics : INodeMetrics
    {
        private readonly IDriverMetricsProvider _driverMetricsProvider;
        private readonly string _bucketName;

        private IHostConnectionPool _hostConnectionPool = null;

        public NodeMetrics(
            IDriverMetricsProvider driverMetricsProvider, DriverMetricsOptions metricOptions, bool metricsEnabled, string bucketName)
        {
            _driverMetricsProvider = driverMetricsProvider;
            _bucketName = bucketName;
            MetricsRegistry = new InternalMetricsRegistry<NodeMetric>(
                driverMetricsProvider, NodeMetric.AllNodeMetrics.Except(metricOptions.EnabledNodeMetrics), metricsEnabled);

            InitializeMetrics();
        }

        public IDriverCounter SpeculativeExecutions { get; private set; }

        public IDriverMeter BytesSent { get; private set; }

        public IDriverMeter BytesReceived { get; private set; }

        public IDriverTimer CqlMessages { get; private set; }

        public IDriverGauge OpenConnections { get; private set; }

        public IDriverGauge InFlight { get; private set; }

        public IRequestErrorMetrics Errors { get; private set; }

        public IRetryPolicyMetrics Retries { get; private set; }

        public IRetryPolicyMetrics Ignores { get; private set; }

        /// <inheritdoc />
        public IInternalMetricsRegistry<NodeMetric> MetricsRegistry { get; }

        private void InitializeMetrics()
        {
            try
            {
                SpeculativeExecutions = MetricsRegistry.Counter(
                    _bucketName, NodeMetric.Counters.SpeculativeExecutions);
                BytesSent = MetricsRegistry.Meter(_bucketName, NodeMetric.Meters.BytesSent);
                BytesReceived = MetricsRegistry.Meter(_bucketName, NodeMetric.Meters.BytesReceived);
                CqlMessages = MetricsRegistry.Timer(_bucketName, NodeMetric.Timers.CqlMessages);

                Errors = new RequestErrorMetrics(MetricsRegistry, _bucketName);
                Retries = new RetryPolicyOnRetryMetrics(MetricsRegistry, _bucketName);
                Ignores = new RetryPolicyOnIgnoreMetrics(MetricsRegistry, _bucketName);

                OpenConnections = MetricsRegistry.Gauge(
                    _bucketName, NodeMetric.Gauges.OpenConnections, () => _hostConnectionPool?.OpenConnections);
                
                InFlight = MetricsRegistry.Gauge(
                    _bucketName, NodeMetric.Gauges.InFlight, () => _hostConnectionPool?.InFlight);

                MetricsRegistry.OnMetricsAdded();
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }

        /// <inheritdoc />
        public void InitializePoolGauges(IHostConnectionPool pool)
        {
            _hostConnectionPool = pool;
        }

        public void Dispose()
        {
            _driverMetricsProvider.ShutdownMetricsBucket(_bucketName);
        }
    }
}
