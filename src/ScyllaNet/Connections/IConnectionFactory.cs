// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Observers.Abstractions;
using Scylla.Net.Serialization;

namespace Scylla.Net.Connections
{
    internal interface IConnectionFactory
    {
        /// <summary>
        /// Create an observed connection. Usually used for <see cref="IHostConnectionPool"/> connections.
        /// </summary>
        IConnection Create(ISerializer serializer, IConnectionEndPoint endPoint, Configuration configuration, IConnectionObserver connectionObserver);

        /// <summary>
        /// Create an unobserved connection (without a <see cref="IConnectionObserver"/>). Usually used for control connections.
        /// </summary>
        IConnection CreateUnobserved(ISerializer serializer, IConnectionEndPoint endPoint, Configuration configuration);
    }
}
