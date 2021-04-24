// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Data.Common;

namespace Scylla.Net.Data
{
    /// <summary>
    /// Represents a set of methods for creating instances of a CQL ADO.NET implementation
    /// of the data source classes.
    /// </summary>
    public class CqlProviderFactory : DbProviderFactory
    {
        public static readonly CqlProviderFactory Instance = new CqlProviderFactory();

        public virtual CqlProviderFactory GetInstance()
        {
            return Instance;
        }

        public override DbCommand CreateCommand()
        {
            return new CqlCommand();
        }

        public override DbConnection CreateConnection()
        {
            return new CqlConnection();
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return new ScyllaDbConnectionStringBuilder();
        }

        public override DbParameter CreateParameter()
        {
            throw new NotSupportedException();
        }
        
        public override bool CanCreateDataSourceEnumerator
        {
            get { return false; }
        }

        public override DbCommandBuilder CreateCommandBuilder()
        {
            return new CqlCommandBuilder();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return new CqlDataAdapter();
        }

        public override DbDataSourceEnumerator CreateDataSourceEnumerator()
        {
            throw new NotSupportedException();
        }

#if NETFRAMEWORK
        public override System.Security.CodeAccessPermission CreatePermission(System.Security.Permissions.PermissionState state)
        {
            throw new NotSupportedException();
        }
#endif
    }
}
