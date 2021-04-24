// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Scylla.Net.Connections;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.Requests
{
    internal interface IReprepareHandler
    {
        /// <summary>
        /// Sends the prepare request to all nodes have have an existing open connection. Will not attempt to send the request to hosts that were tried before (successfully or not).
        /// </summary>
        /// <param name="session"></param>
        /// <param name="request"></param>
        /// <param name="prepareResult">The result of the prepare request on the first node.</param>
        /// <returns></returns>
        Task ReprepareOnAllNodesWithExistingConnections(
            IInternalSession session, PrepareRequest request, PrepareResult prepareResult);

        Task ReprepareOnSingleNodeAsync(
            KeyValuePair<Host, IHostConnectionPool> poolKvp, PreparedStatement ps, IRequest request, SemaphoreSlim sem, bool throwException);
    }
}
