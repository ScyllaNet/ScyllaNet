// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Linq;
using Scylla.Net.Metrics.Abstractions;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.Metrics.Registries
{
    /// <inheritdoc />
    internal class SessionMetrics : ISessionMetrics
    {
        private readonly IDriverMetricsProvider _driverMetricsProvider;
        private readonly string _context;

        public SessionMetrics(IDriverMetricsProvider driverMetricsProvider, DriverMetricsOptions metricsOptions, bool metricsEnabled, string context)
        {
            _driverMetricsProvider = driverMetricsProvider;
            _context = context;
            MetricsRegistry = new InternalMetricsRegistry<SessionMetric>(
                driverMetricsProvider, SessionMetric.AllSessionMetrics.Except(metricsOptions.EnabledSessionMetrics), metricsEnabled);
        }

        public IDriverTimer CqlRequests { get; private set; }

        public IDriverCounter CqlClientTimeouts { get; private set; }

        public IDriverMeter BytesSent { get; private set; }

        public IDriverMeter BytesReceived { get; private set; }

        public IDriverGauge ConnectedNodes { get; private set; }

        /// <inheritdoc />
        public IInternalMetricsRegistry<SessionMetric> MetricsRegistry { get; }

        public void InitializeMetrics(IInternalSession session)
        {
            try
            {
                CqlRequests = MetricsRegistry.Timer(_context, SessionMetric.Timers.CqlRequests);
                CqlClientTimeouts = MetricsRegistry.Counter(_context, SessionMetric.Counters.CqlClientTimeouts);
                BytesSent = MetricsRegistry.Meter(_context, SessionMetric.Meters.BytesSent);
                BytesReceived = MetricsRegistry.Meter(_context, SessionMetric.Meters.BytesReceived);
                ConnectedNodes = MetricsRegistry.Gauge(
                    _context, SessionMetric.Gauges.ConnectedNodes, () => session.ConnectedNodes);

                MetricsRegistry.OnMetricsAdded();
            }
            catch (Exception)
            {
                Dispose();
                throw;
            }
        }

        public void Dispose()
        {
            _driverMetricsProvider.ShutdownMetricsBucket(_context);
        }
    }
}
