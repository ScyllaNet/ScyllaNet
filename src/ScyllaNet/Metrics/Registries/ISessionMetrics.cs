// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

using Scylla.Net.Metrics.Abstractions;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.Metrics.Registries
{
    /// <summary>
    /// Exposes specific session metrics for the driver internals.
    /// </summary>
    internal interface ISessionMetrics : IDisposable
    {
        IDriverTimer CqlRequests { get; }

        IDriverCounter CqlClientTimeouts { get; }

        IDriverMeter BytesSent { get; }

        IDriverMeter BytesReceived { get; }

        IDriverGauge ConnectedNodes { get; }
        
        /// <summary>
        /// Internal MetricsRegistry used to create metrics internally.
        /// </summary>
        IInternalMetricsRegistry<SessionMetric> MetricsRegistry { get; }

        void InitializeMetrics(IInternalSession session);
    }
}
