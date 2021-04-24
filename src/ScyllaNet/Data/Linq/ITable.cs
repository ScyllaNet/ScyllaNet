// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Linq;

namespace Scylla.Net.Data.Linq
{
    public interface ITable : IQueryProvider
    {
        void Create();
        Type GetEntityType();
        /// <summary>
        /// Gets the table name in Cassandra
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets the name of the keyspace used. If null, it uses the active session keyspace.
        /// </summary>
        string KeyspaceName { get; }
        ISession GetSession();
        TableType GetTableType();
    }
}
