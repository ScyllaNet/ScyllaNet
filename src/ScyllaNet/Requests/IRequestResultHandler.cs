// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Scylla.Net.Requests
{
    /// <summary>
    /// Component used by <see cref="IRequestHandler"/> and <see cref="IRequestExecution"/> to store the result
    /// of a request and expose an awaitable task that will be complete when the request is complete.
    /// </summary>
    internal interface IRequestResultHandler
    {
        void TrySetResult(RowSet result);

        void TrySetException(Exception exception);

        Task<RowSet> Task { get; }
    }
}
