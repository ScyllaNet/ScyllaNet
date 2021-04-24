﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Scylla.Net.Tasks;

namespace Scylla.Net.Data.Linq
{
    public class CqlQuerySingleElement<TEntity> : CqlQueryBase<TEntity>
    {
        internal CqlQuerySingleElement(Expression expression, CqlQuery<TEntity> source)
            : base(expression, source.Table, source.MapperFactory, source.StatementFactory, source.PocoData)
        {
            
        }

        protected override string GetCql(out object[] values)
        {
            var visitor = new CqlExpressionVisitor(PocoData, Table.Name, Table.KeyspaceName);
            return visitor.GetSelect(Expression, out values);
        }

        public override string ToString()
        {
            object[] _;
            return GetCql(out _);
        }

        public new CqlQuerySingleElement<TEntity> SetConsistencyLevel(ConsistencyLevel? consistencyLevel)
        {
            base.SetConsistencyLevel(consistencyLevel);
            return this;
        }

        public new CqlQuerySingleElement<TEntity> SetSerialConsistencyLevel(ConsistencyLevel consistencyLevel)
        {
            base.SetSerialConsistencyLevel(consistencyLevel);
            return this;
        }

        public new async Task<TEntity> ExecuteAsync()
        {
            var rs = await base.ExecuteAsync().ConfigureAwait(false);
            return rs.FirstOrDefault();
        }

        public new async Task<TEntity> ExecuteAsync(string executionProfile)
        {
            var rs = await base.ExecuteAsync(executionProfile).ConfigureAwait(false);
            return rs.FirstOrDefault();
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

        /// <summary>
        /// Evaluates the Linq query, executes the cql statement and returns the first result.
        /// </summary>
        public new TEntity Execute()
        {
            return Execute(Configuration.DefaultExecutionProfileName);
        }
        
        /// <summary>
        /// Evaluates the Linq query, executes the cql statement with the provided execution profile and returns the first result.
        /// </summary>
        public new TEntity Execute(string executionProfile)
        {
            return WaitToCompleteWithMetrics(ExecuteAsync(executionProfile), QueryAbortTimeout);
        }
    }
}
