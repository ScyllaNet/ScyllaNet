// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Mapping.Statements;

namespace Scylla.Net.Data.Linq
{
    /// <summary>
    /// Internal interface that exposes useful methods and properties for the driver internal components.
    /// </summary>
    internal interface IInternalStatement : IStatement
    {
        ITable GetTable();

        StatementFactory StatementFactory { get; }
    }
}
