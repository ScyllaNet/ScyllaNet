// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics
{
    /// <summary>
    /// Metrics Registry.
    /// </summary>
    /// <typeparam name="TMetric">Should be <see cref="NodeMetric"/> out <see cref="SessionMetric"/>.</typeparam>
    public interface IMetricsRegistry<TMetric> where TMetric : IMetric
    {
        /// <summary>
        /// Dictionary with counter metrics.
        /// </summary>
        IReadOnlyDictionary<TMetric, IDriverCounter> Counters { get; }
        
        /// <summary>
        /// Dictionary with gauge metrics.
        /// </summary>
        IReadOnlyDictionary<TMetric, IDriverGauge> Gauges { get; }
        
        /// <summary>
        /// Dictionary with meter metrics.
        /// </summary>
        IReadOnlyDictionary<TMetric, IDriverMeter> Meters { get; }

        /// <summary>
        /// Dictionary with timer metrics.
        /// </summary>
        IReadOnlyDictionary<TMetric, IDriverTimer> Timers { get; }

        /// <summary>
        /// Dictionary with metrics of all types. The values can be cast to the appropriate type interface
        /// (or the implementation that is provider specific).
        /// </summary>
        IReadOnlyDictionary<TMetric, IDriverMetric> Metrics { get; }
    }
}
