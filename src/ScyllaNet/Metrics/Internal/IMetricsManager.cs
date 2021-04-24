// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

using Scylla.Net.Metrics.Registries;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.Metrics.Internal
{
    /// <summary>
    /// Implements <see cref="IDriverMetrics"/> and exposes methods for the driver internals to update metrics.
    /// </summary>
    internal interface IMetricsManager : IDriverMetrics, IDisposable
    {
        ISessionMetrics GetSessionMetrics();

        /// <summary>
        /// Get the existing node metrics for the provided host or creates them and returns them if they don't exist yet.
        /// </summary>
        INodeMetrics GetOrCreateNodeMetrics(Host host);

        /// <summary>
        /// Initialize metrics with the provided session.
        /// </summary>
        void InitializeMetrics(IInternalSession session);

        /// <summary>
        /// 
        /// </summary>
        void RemoveNodeMetrics(Host host);

        /// <summary>
        /// Whether SessionMetrics of type Timer are enabled.
        /// </summary>
        bool AreSessionTimerMetricsEnabled { get; }
        
        /// <summary>
        /// Whether NodeMetrics of type Timer are enabled.
        /// </summary>
        bool AreNodeTimerMetricsEnabled { get; }

        /// <summary>
        /// Whether metrics are enabled.
        /// </summary>
        bool AreMetricsEnabled { get; }
    }
}
