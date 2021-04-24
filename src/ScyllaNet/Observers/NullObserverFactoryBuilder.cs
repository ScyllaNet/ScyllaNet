// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Metrics.Internal;
using Scylla.Net.Observers.Abstractions;

namespace Scylla.Net.Observers
{
    internal class NullObserverFactoryBuilder : IObserverFactoryBuilder
    {
        public IObserverFactory Build(IMetricsManager manager)
        {
            return new NullObserverFactory();
        }
    }
}
