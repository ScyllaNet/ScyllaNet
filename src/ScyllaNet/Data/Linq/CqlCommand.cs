﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Scylla.Net.Mapping;
using Scylla.Net.Mapping.Statements;
using Scylla.Net.Metrics.Internal;
using Scylla.Net.SessionManagement;
using Scylla.Net.Tasks;

namespace Scylla.Net.Data.Linq
{
    /// <summary>
    /// Represents a Linq query (UPDATE/INSERT/DELETE) that gets evaluated as a CQL statement.
    /// </summary>
    public abstract class CqlCommand : SimpleStatement, IInternalStatement
    {
        private readonly Expression _expression;
        private readonly StatementFactory _statementFactory;
        protected DateTimeOffset? _timestamp;
        protected int? _ttl;
        private QueryTrace _queryTrace;
        private IMetricsManager _metricsManager;

        protected int QueryAbortTimeout { get; private set; }

        internal PocoData PocoData { get; }
        internal ITable Table { get; }

        internal IInternalStatement InternalRef => this;

        /// <inheritdoc />
        public override string QueryString
        {
            get
            {
                if (base.QueryString == null)
                {
                    InitializeStatement();
                }

                return base.QueryString;
            }
        }

        /// <inheritdoc />
        public override object[] QueryValues
        {
            get
            {
                if (base.QueryString == null)
                {
                    InitializeStatement();
                }

                return base.QueryValues;
            }
        }

        StatementFactory IInternalStatement.StatementFactory => _statementFactory;

        public Expression Expression => _expression;

        /// <summary>
        /// After being executed, it retrieves the trace of the CQL query.
        /// <para>Use <see cref="IStatement.EnableTracing"/> to enable tracing.</para>
        /// <para>
        /// Note that enabling query trace introduces server-side overhead by storing request information, so it's
        /// recommended that you only enable query tracing when trying to identify possible issues / debugging.
        /// </para>
        /// </summary>
        public QueryTrace QueryTrace
        {
            get => Volatile.Read(ref _queryTrace);
            protected set => Volatile.Write(ref _queryTrace, value);
        }

        internal CqlCommand(Expression expression, ITable table, StatementFactory stmtFactory, PocoData pocoData)
        {
            _expression = expression;
            Table = table;
            _statementFactory = stmtFactory;
            PocoData = pocoData;
            QueryAbortTimeout = table.GetSession().Cluster.Configuration.DefaultRequestOptions.QueryAbortTimeout;
            _metricsManager = (table.GetSession() as IInternalSession)?.MetricsManager;
        }

        protected internal abstract string GetCql(out object[] values);

        /// <summary>
        /// Executes the command using the <see cref="ISession"/>.
        /// </summary>
        public void Execute()
        {
            Execute(Configuration.DefaultExecutionProfileName);
        }

        /// <summary>
        /// Executes the command using the <see cref="ISession"/> with the provided execution profile.
        /// </summary>
        public RowSet Execute(string executionProfile)
        {
            if (executionProfile == null)
            {
                throw new ArgumentNullException(nameof(executionProfile));
            }
            return WaitToCompleteWithMetrics(ExecuteAsync(executionProfile), QueryAbortTimeout);
        }

        public void SetQueryTrace(QueryTrace trace)
        {
            QueryTrace = trace;
        }

        public new CqlCommand SetConsistencyLevel(ConsistencyLevel? consistencyLevel)
        {
            base.SetConsistencyLevel(consistencyLevel);
            return this;
        }

        public new CqlCommand SetSerialConsistencyLevel(ConsistencyLevel consistencyLevel)
        {
            base.SetSerialConsistencyLevel(consistencyLevel);
            return this;
        }

        /// <summary>
        /// Sets the time for data in a column to expire (TTL) for INSERT and UPDATE commands .
        /// </summary>
        /// <param name="seconds">Amount of seconds</param>
        public CqlCommand SetTTL(int seconds)
        {
            _ttl = seconds;
            return this;
        }

        /// <summary>
        /// Sets the timestamp associated with this statement execution.
        /// </summary>
        /// <returns>This instance.</returns>
        public new CqlCommand SetTimestamp(DateTimeOffset timestamp)
        {
            _timestamp = timestamp;
            return this;
        }

        protected void InitializeStatement()
        {
            var query = GetCql(out var values);
            SetQueryString(query);
            SetValues(values, Serializer);
        }

        public ITable GetTable()
        {
            return Table;
        }

        /// <summary>
        /// Evaluates the Linq command and executes asynchronously the cql statement.
        /// </summary>
        public Task<RowSet> ExecuteAsync()
        {
            return ExecuteAsync(Configuration.DefaultExecutionProfileName);
        }

        /// <summary>
        /// Evaluates the Linq command and executes asynchronously the cql statement with the provided execution profile.
        /// </summary>
        public async Task<RowSet> ExecuteAsync(string executionProfile)
        {
            if (executionProfile == null)
            {
                throw new ArgumentNullException(executionProfile);
            }

            var cqlQuery = GetCql(out var values);
            var session = GetTable().GetSession();
            var stmt = await _statementFactory.GetStatementAsync(
                session,
                Cql.New(cqlQuery, values).WithExecutionProfile(executionProfile)).ConfigureAwait(false);
            this.CopyQueryPropertiesTo(stmt);
            var rs = await session.ExecuteAsync(stmt, executionProfile).ConfigureAwait(false);
            QueryTrace = rs.Info.QueryTrace;
            return rs;
        }

        /// <summary>
        /// Starts executing the statement async
        /// </summary>
        public virtual IAsyncResult BeginExecute(AsyncCallback callback, object state)
        {
            return ExecuteAsync().ToApm(callback, state);
        }

        /// <summary>
        /// Starts the async executing of the statement
        /// </summary>
        public virtual void EndExecute(IAsyncResult ar)
        {
            var task = (Task<RowSet>)ar;
            task.Wait();
        }

        internal T WaitToCompleteWithMetrics<T>(Task<T> task, int timeout = Timeout.Infinite)
        {
            return TaskHelper.WaitToCompleteWithMetrics(_metricsManager, task, timeout);
        }
    }
}
