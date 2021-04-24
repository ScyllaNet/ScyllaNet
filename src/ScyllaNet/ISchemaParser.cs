// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Scylla.Net.Tasks;

namespace Scylla.Net
{
    internal interface ISchemaParser
    {
        /// <summary>
        /// Gets the keyspace metadata
        /// </summary>
        /// <returns>The keyspace metadata or null if not found</returns>
        Task<KeyspaceMetadata> GetKeyspaceAsync(string name);

        /// <summary>
        /// Gets all the keyspaces metadata
        /// </summary>
        Task<IEnumerable<KeyspaceMetadata>> GetKeyspacesAsync(bool retry);

        Task<TableMetadata> GetTableAsync(string keyspaceName, string tableName);

        Task<MaterializedViewMetadata> GetViewAsync(string keyspaceName, string viewName);

        Task<ICollection<string>> GetTableNamesAsync(string keyspaceName);

        Task<ICollection<string>> GetKeyspacesNamesAsync();

        Task<FunctionMetadata> GetFunctionAsync(string keyspaceName, string functionName, string signatureString);

        Task<AggregateMetadata> GetAggregateAsync(string keyspaceName, string aggregateName, string signatureString);

        Task<UdtColumnInfo> GetUdtDefinitionAsync(string keyspaceName, string typeName);

        string ComputeFunctionSignatureString(string[] signature);

        Task<QueryTrace> GetQueryTraceAsync(QueryTrace trace, HashedWheelTimer timer);
    }
}
