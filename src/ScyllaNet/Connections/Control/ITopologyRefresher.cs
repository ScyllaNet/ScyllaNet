// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Threading.Tasks;
using Scylla.Net.Serialization;

namespace Scylla.Net.Connections.Control
{
    /// <summary>
    /// Class that issues system table queries and updates the hosts collection on <see cref="Metadata"/>.
    /// </summary>
    internal interface ITopologyRefresher
    {
        /// <summary>
        /// Refreshes the Hosts collection using the <paramref name="currentEndPoint"/> to issue system table queries (local and peers).
        /// </summary>
        /// <returns>Returns the Host parsed from the <paramref name="currentEndPoint"/>'s system.local table.</returns>
        Task<Host> RefreshNodeListAsync(IConnectionEndPoint currentEndPoint, IConnection connection, ISerializer serializer);
    }
}
