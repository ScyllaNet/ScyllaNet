// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Linq.Expressions;
using Scylla.Net.Mapping.TypeConversion;

namespace Scylla.Net.Mapping
{
    /// <summary>
    /// A Linq expression visitor that extracts the projection to allow to be reconstructed from a different origin.
    /// </summary>
    internal class ProjectionExpressionVisitor : ExpressionVisitor
    {
        private Expression _expression;
        public NewTypeProjection Projection { get; private set; }

        public override Expression Visit(Expression node)
        {
            _expression = node;
            return base.Visit(node);
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            if (Projection == null)
            {
                throw new NotSupportedException("Projection expression not supported: " + _expression);
            }
            Projection.Members.Add(node.Member);
            return node;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            Projection = new NewTypeProjection(node.Constructor);
            return node;
        }
    }
}
