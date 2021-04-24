// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Data;
using System.Data.Common;

namespace Scylla.Net.Data
{
    internal class CqlCommandBuilder : DbCommandBuilder
    {
        protected override void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause)
        {
            throw new NotSupportedException();
        }

        protected override string GetParameterName(string parameterName)
        {
            throw new NotSupportedException();
        }

        protected override string GetParameterName(int parameterOrdinal)
        {
            throw new NotSupportedException();
        }

        protected override string GetParameterPlaceholder(int parameterOrdinal)
        {
            throw new NotSupportedException();
        }

        protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
        {
            throw new NotSupportedException();
        }
    }
}
