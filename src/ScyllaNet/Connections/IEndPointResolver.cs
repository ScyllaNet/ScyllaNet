// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Threading.Tasks;

namespace Scylla.Net.Connections
{
    /// <summary>
    /// Builds instances of <see cref="IConnectionEndPoint"/> from <see cref="Host"/> instances.
    /// The endpoints are used to create connections.
    /// </summary>
    internal interface IEndPointResolver
    {
        bool CanBeResolved { get; }

        /// <summary>
        /// Gets an instance of <see cref="IConnectionEndPoint"/> to the provided host from the internal cache (if caching is supported by the implementation).
        /// </summary>
        /// <param name="host">Host related to the new endpoint.</param>
        /// <param name="refreshCache">Whether to refresh the internal cache. If it is false and the cache is populated then
        /// no round trip will occur.</param>
        /// <returns>Endpoint.</returns>
        Task<IConnectionEndPoint> GetConnectionEndPointAsync(Host host, bool refreshCache);
    }
}
