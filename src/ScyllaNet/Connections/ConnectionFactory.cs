// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Observers;
using Scylla.Net.Observers.Abstractions;
using Scylla.Net.Requests;
using Scylla.Net.Serialization;

namespace Scylla.Net.Connections
{
    internal class ConnectionFactory : IConnectionFactory
    {
        public IConnection Create(
            ISerializer serializer, IConnectionEndPoint endPoint, Configuration configuration, IConnectionObserver connectionObserver)
        {
            return new Connection(
                serializer, endPoint, configuration, new StartupRequestFactory(configuration.StartupOptionsFactory), connectionObserver);
        }

        public IConnection CreateUnobserved(ISerializer serializer, IConnectionEndPoint endPoint, Configuration configuration)
        {
            return new Connection(
                serializer, 
                endPoint, 
                configuration, 
                new StartupRequestFactory(configuration.StartupOptionsFactory), 
                NullConnectionObserver.Instance);
        }
    }
}
