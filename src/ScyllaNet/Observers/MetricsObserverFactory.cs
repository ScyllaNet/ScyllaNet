// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Metrics.Internal;
using Scylla.Net.Observers.Abstractions;

namespace Scylla.Net.Observers
{
    internal class MetricsObserverFactory : IObserverFactory
    {
        private readonly IMetricsManager _metricsManager;

        public MetricsObserverFactory(IMetricsManager metricsManager)
        {
            _metricsManager = metricsManager;
        }

        public IRequestObserver CreateRequestObserver()
        {
            return new MetricsRequestObserver(_metricsManager, _metricsManager.GetSessionMetrics().CqlRequests);
        }

        public IConnectionObserver CreateConnectionObserver(Host host)
        {
            return new MetricsConnectionObserver(
                _metricsManager.GetSessionMetrics(), 
                _metricsManager.GetOrCreateNodeMetrics(host), 
                _metricsManager.AreNodeTimerMetricsEnabled);
        }
    }
}
