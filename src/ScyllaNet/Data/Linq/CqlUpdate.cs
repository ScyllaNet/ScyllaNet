// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Linq.Expressions;
using Scylla.Net.Mapping;
using Scylla.Net.Mapping.Statements;

namespace Scylla.Net.Data.Linq
{
    public class CqlUpdate : CqlCommand
    {
        private readonly MapperFactory _mapperFactory;

        internal CqlUpdate(Expression expression, ITable table, StatementFactory stmtFactory, PocoData pocoData, MapperFactory mapperFactory)
            : base(expression, table, stmtFactory, pocoData)
        {
            _mapperFactory = mapperFactory;
        }

        protected internal override string GetCql(out object[] values)
        {
            var visitor = new CqlExpressionVisitor(PocoData, Table.Name, Table.KeyspaceName);
            return visitor.GetUpdate(Expression, out values, _ttl, _timestamp, _mapperFactory);
        }

        public override string ToString()
        {
            object[] _;
            return GetCql(out _);
        }
    }
}
