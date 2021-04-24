// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Linq.Expressions;

namespace Scylla.Net.Data.Linq
{
    public class CqlLinqNotSupportedException : NotSupportedException
    {
        public Expression Expression { get; private set; }

        internal CqlLinqNotSupportedException(Expression expression, ParsePhase parsePhase)
            : base(string.Format("The expression {0} = [{1}] is not supported in {2} parse phase.",
                                 expression.NodeType, expression, parsePhase))
        {
            Expression = expression;
        }
    }
}
