// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

using Scylla.Net.Connections;
using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics.Registries
{
    /// <summary>
    /// Exposes specific node metrics for the driver internals.
    /// </summary>
    internal interface INodeMetrics : IDisposable
    {
        IDriverCounter SpeculativeExecutions { get; }

        IDriverMeter BytesSent { get; }

        IDriverMeter BytesReceived { get; }

        IDriverTimer CqlMessages { get; }

        IDriverGauge OpenConnections { get; }
        
        IDriverGauge InFlight { get; }

        IRequestErrorMetrics Errors { get; }

        IRetryPolicyMetrics Retries { get; }

        IRetryPolicyMetrics Ignores { get; }

        /// <summary>
        /// Internal MetricsRegistry used to create metrics internally.
        /// </summary>
        IInternalMetricsRegistry<NodeMetric> MetricsRegistry { get; }

        /// <summary>
        /// Initialize gauge metrics with a specific connection pool.
        /// </summary>
        /// <param name="pool"></param>
        void InitializePoolGauges(IHostConnectionPool pool);
    }
}
