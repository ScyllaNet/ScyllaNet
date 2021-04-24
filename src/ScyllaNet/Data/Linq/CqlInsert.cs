// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Mapping;
using Scylla.Net.Mapping.Statements;
using Scylla.Net.Mapping.Utils;

namespace Scylla.Net.Data.Linq
{
    /// <summary>
    /// Represents an INSERT statement
    /// </summary>
    public class CqlInsert<TEntity> : CqlCommand
    {
        private static readonly ICqlIdentifierHelper CqlIdentifierHelper = new CqlIdentifierHelper();

        private readonly TEntity _entity;
        private bool _ifNotExists;
        private readonly MapperFactory _mapperFactory;
        private readonly bool _insertNulls;

        internal CqlInsert(TEntity entity, bool insertNulls, ITable table, StatementFactory stmtFactory, MapperFactory mapperFactory)
            : base(null, table, stmtFactory, mapperFactory.GetPocoData<TEntity>())
        {
            _entity = entity;
            _insertNulls = insertNulls;
            _mapperFactory = mapperFactory;
        }

        public CqlConditionalCommand<TEntity> IfNotExists()
        {
            _ifNotExists = true;
            return new CqlConditionalCommand<TEntity>(this, _mapperFactory);
        }

        protected internal override string GetCql(out object[] values)
        {
            var pocoData = _mapperFactory.PocoDataFactory.GetPocoData<TEntity>();
            var queryIdentifier = $"INSERT LINQ ID {Table.KeyspaceName}/{Table.Name}";
            var getBindValues = _mapperFactory.GetValueCollector<TEntity>(queryIdentifier);
            //get values first to identify null values
            var pocoValues = getBindValues(_entity);
            //generate INSERT query based on null values (if insertNulls set)
            var cqlGenerator = new CqlGenerator(_mapperFactory.PocoDataFactory);
            //Use the table name from Table<TEntity> instance instead of PocoData
            var tableName = CqlInsert<TEntity>.CqlIdentifierHelper.EscapeTableNameIfNecessary(pocoData, Table.KeyspaceName, Table.Name);
            return cqlGenerator.GenerateInsert<TEntity>(
                _insertNulls, pocoValues, out values, _ifNotExists, _ttl, _timestamp, tableName);
        }

        internal string GetCqlAndValues(out object[] values)
        {
            return GetCql(out values);
        }

        public override string ToString()
        {
            object[] _;
            return GetCql(out _);
        }
    }
}
