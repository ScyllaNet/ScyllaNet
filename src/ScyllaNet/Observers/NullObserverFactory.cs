// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Observers.Abstractions;

namespace Scylla.Net.Observers
{
    internal class NullObserverFactory : IObserverFactory
    {
        public IRequestObserver CreateRequestObserver()
        {
            return NullRequestObserver.Instance;
        }

        public IConnectionObserver CreateConnectionObserver(Host host)
        {
            return NullConnectionObserver.Instance;
        }
    }
}
