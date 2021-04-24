// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics
{
    /// <summary>
    /// Exposes driver metrics.
    /// </summary>
    public interface IDriverMetrics
    {
        /// <summary>
        /// Exposes session metrics for the session from which this instance was retrieved. See <see cref="ISession.GetMetrics"/>.
        /// </summary>
        IMetricsRegistry<SessionMetric> SessionMetrics { get; }
        
        /// <summary>
        /// Exposes node metrics for the hosts used in requests executed by the session
        /// from which this instance was retrieved. See <see cref="ISession.GetMetrics"/>.
        /// </summary>
        IReadOnlyDictionary<Host, IMetricsRegistry<NodeMetric>> NodeMetrics { get; }

        /// <summary>
        /// Gets a specific node metric of a specific host. <typeparamref name="TMetricType"/> can be any type in the
        /// inheritance tree of the metric object returned by the <see cref="IDriverMetricsProvider"/>.
        /// </summary>
        /// <exception cref="ArgumentException">This exception is thrown if the metric object can not be cast to <typeparamref name="TMetricType"/>.</exception>
        TMetricType GetNodeMetric<TMetricType>(Host host, NodeMetric nodeMetric) where TMetricType : class, IDriverMetric;
        
        /// <summary>
        /// Gets a specific session metric. <typeparamref name="TMetricType"/> can be any type in the
        /// inheritance tree of the metric object returned by the <see cref="IDriverMetricsProvider"/>.
        /// </summary>
        /// <exception cref="ArgumentException">This exception is thrown if the metric object can not be cast to <typeparamref name="TMetricType"/>.</exception>
        TMetricType GetSessionMetric<TMetricType>(SessionMetric sessionMetric) where TMetricType : class, IDriverMetric;
    }
}
