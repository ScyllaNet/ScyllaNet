﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Scylla.Net.Mapping;
using Scylla.Net.Mapping.Statements;
using Scylla.Net.Tasks;

namespace Scylla.Net.Data.Linq
{
    /// <summary>
    /// Represents an IQueryable that returns the first column of the first rows
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class CqlScalar<TEntity> : CqlQueryBase<TEntity>
    {
        internal CqlScalar(Expression expression, ITable table, StatementFactory stmtFactory, PocoData pocoData)
            : base(expression, table, null, stmtFactory, pocoData)
        {

        }

        public new TEntity Execute()
        {
            return Execute(Configuration.DefaultExecutionProfileName);
        }
        
        public new TEntity Execute(string executionProfile)
        {
            return WaitToCompleteWithMetrics(ExecuteAsync(executionProfile), QueryAbortTimeout);
        }

        public new CqlScalar<TEntity> SetConsistencyLevel(ConsistencyLevel? consistencyLevel)
        {
            base.SetConsistencyLevel(consistencyLevel);
            return this;
        }

        protected override string GetCql(out object[] values)
        {
            var visitor = new CqlExpressionVisitor(PocoData, Table.Name, Table.KeyspaceName);
            return visitor.GetCount(Expression, out values);
        }

        public override string ToString()
        {
            object[] _;
            return GetCql(out _);
        }

        public new Task<TEntity> ExecuteAsync()
        {
            return ExecuteAsync(Configuration.DefaultExecutionProfileName);
        }
        
        public new async Task<TEntity> ExecuteAsync(string executionProfile)
        {
            if (executionProfile == null)
            {
                throw new ArgumentNullException(nameof(executionProfile));
            }

            string cql = GetCql(out object[] values);
            var rs = await InternalExecuteWithProfileAsync(executionProfile, cql, values).ConfigureAwait(false);
            var result = default(TEntity);
            var row = rs.FirstOrDefault();
            if (row != null)
            {
                result = (TEntity)row[0];
            }

            return result;
        }

        public new IAsyncResult BeginExecute(AsyncCallback callback, object state)
        {
            return ExecuteAsync().ToApm(callback, state);
        }

        public new TEntity EndExecute(IAsyncResult ar)
        {
            var task = (Task<TEntity>)ar;
            return task.Result;
        }
    }
}
