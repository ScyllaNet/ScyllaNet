// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scylla.Net.Connections;
using Scylla.Net.Connections.Control;
using Scylla.Net.Requests;
using Scylla.Net.Serialization;

namespace Scylla.Net.SessionManagement
{
    /// <inheritdoc />
    internal interface IInternalCluster : ICluster
    {
        bool AnyOpenConnections(Host host);

        /// <summary>
        /// Gets the control connection used by the cluster
        /// </summary>
        IControlConnection GetControlConnection();

        /// <summary>
        /// Gets the the prepared statements cache
        /// </summary>
        ConcurrentDictionary<byte[], PreparedStatement> PreparedQueries { get; }

        /// <summary>
        /// Executes the prepare request on the first host selected by the load balancing policy.
        /// When <see cref="QueryOptions.IsPrepareOnAllHosts"/> is enabled, it prepares on the rest of the hosts in
        /// parallel.
        /// In case the statement was already in the prepared statements cache, logs an warning but prepares it anyway.
        /// </summary>
        Task<PreparedStatement> Prepare(IInternalSession session, ISerializerManager serializerManager, PrepareRequest request);
        
        IReadOnlyDictionary<IContactPoint, IEnumerable<IConnectionEndPoint>> GetResolvedEndpoints();

        /// <summary>
        /// Helper method to retrieve the aggregate distance from all configured LoadBalancingPolicies and set it at Host level.
        /// </summary>
        HostDistance RetrieveAndSetDistance(Host host);
    }
}
