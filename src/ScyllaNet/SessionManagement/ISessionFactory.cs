// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Threading.Tasks;
using Scylla.Net.Serialization;

namespace Scylla.Net.SessionManagement
{
    internal interface ISessionFactory
    {
        Task<IInternalSession> CreateSessionAsync(
            IInternalCluster cluster, string keyspace, ISerializerManager serializer, string sessionName);
    }
}
