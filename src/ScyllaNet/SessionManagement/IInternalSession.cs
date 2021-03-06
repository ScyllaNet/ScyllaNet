// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Scylla.Net.Connections;
using Scylla.Net.ExecutionProfiles;
using Scylla.Net.Metrics.Internal;
using Scylla.Net.Observers.Abstractions;

namespace Scylla.Net.SessionManagement
{
    /// <inheritdoc />
    /// <remarks>This is an internal interface designed to declare the internal methods that are called
    /// across multiple locations of the driver's source code.</remarks>
    internal interface IInternalSession : ISession
    {
        /// <summary>
        /// Unique Session ID generated by the driver.
        /// </summary>
        Guid InternalSessionId { get; }

        /// <summary>
        /// Initialize the session
        /// </summary>
        Task Init();

        /// <summary>
        /// Gets or creates the connection pool for a given host
        /// </summary>
        IHostConnectionPool GetOrCreateConnectionPool(Host host, HostDistance distance);

        /// <summary>
        /// Gets a snapshot of the connection pools
        /// </summary>
        IEnumerable<KeyValuePair<IPEndPoint, IHostConnectionPool>> GetPools();

        /// <summary>
        /// Gets the existing connection pool for this host and session or null when it does not exists
        /// </summary>
        IHostConnectionPool GetExistingPool(IPEndPoint address);

        void CheckHealth(Host host, IConnection connection);

        bool HasConnections(Host host);

        void OnAllConnectionClosed(Host host, IHostConnectionPool pool);

        /// <summary>
        /// Gets or sets the keyspace
        /// </summary>
        new string Keyspace { get; set; }

        IInternalCluster InternalCluster { get; }
        
        /// <summary>
        /// Fetches the request options that were mapped from the provided execution profile's name.
        /// </summary>
        IRequestOptions GetRequestOptions(string executionProfileName);

        /// <summary>
        /// Returns the number of connected nodes.
        /// </summary>
        int ConnectedNodes { get; }

        IMetricsManager MetricsManager { get; }

        IObserverFactory ObserverFactory { get; }

        Task<RowSet> ExecuteAsync(IStatement statement, IRequestOptions requestOptions);
    }
}
