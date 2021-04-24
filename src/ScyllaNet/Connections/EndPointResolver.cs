// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Scylla.Net.Connections
{
    internal class EndPointResolver : IEndPointResolver
    {
        private readonly IServerNameResolver _serverNameResolver;

        public EndPointResolver(IServerNameResolver serverNameResolver)
        {
            _serverNameResolver = serverNameResolver ?? throw new ArgumentNullException(nameof(serverNameResolver));
        }

        public bool CanBeResolved => false;

        /// <inheritdoc />
        public Task<IConnectionEndPoint> GetConnectionEndPointAsync(Host host, bool refreshCache)
        {
            return Task.FromResult((IConnectionEndPoint)new ConnectionEndPoint(host.Address, _serverNameResolver, host.ContactPoint));
        }
    }
}
