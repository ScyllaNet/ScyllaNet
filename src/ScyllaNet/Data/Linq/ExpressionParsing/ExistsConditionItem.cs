// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Scylla.Net.Mapping;

namespace Scylla.Net.Data.Linq.ExpressionParsing
{
    internal class ExistsConditionItem : IConditionItem
    {
        private readonly bool _positive;

        public PocoColumn Column =>
            throw new NotSupportedException("Getting column not supported on IF EXISTS condition");

        public ExistsConditionItem(bool positive)
        {
            _positive = positive;
        }

        public IConditionItem SetOperator(ExpressionType expressionType)
        {
            throw new NotSupportedException("Setting operator is not supported on IF EXISTS condition");
        }

        public IConditionItem SetParameter(object value)
        {
            throw new NotSupportedException("Setting parameter is not supported on IF EXISTS condition");
        }

        public IConditionItem SetColumn(PocoColumn column)
        {
            throw new NotSupportedException("Setting column is not supported on IF EXISTS condition");
        }

        public IConditionItem AllowMultipleColumns()
        {
            throw new NotSupportedException("Setting multiple columns is not supported on IF EXISTS condition");
        }

        public IConditionItem AllowMultipleParameters()
        {
            throw new NotSupportedException("Setting multiple parameters is not supported on IF EXISTS condition");
        }

        public IConditionItem SetFunctionName(string name)
        {
            throw new NotSupportedException("Setting function name is not supported on IF EXISTS condition");
        }

        public IConditionItem SetAsCompareTo()
        {
            throw new NotSupportedException("Setting function name is not supported on IF EXISTS condition");
        }

        public void ToCql(PocoData pocoData, StringBuilder query, IList<object> parameters)
        {
            query.Append(_positive ? "EXISTS" : "NOT EXISTS");
        }
    }
}
