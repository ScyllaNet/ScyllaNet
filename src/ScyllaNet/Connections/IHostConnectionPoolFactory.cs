// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Observers.Abstractions;
using Scylla.Net.Serialization;

namespace Scylla.Net.Connections
{
    internal interface IHostConnectionPoolFactory
    {
        IHostConnectionPool Create(Host host, Configuration config, ISerializerManager serializer, IObserverFactory observerFactory);
    }
}
