// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.Requests;

namespace Scylla.Net.Observers.Abstractions
{
    /// <summary>
    /// Exposes callbacks for events related to <see cref="IRequestExecution"/> and <see cref="IRequestHandler"/>.
    /// </summary>
    internal interface IRequestObserver
    {
        void OnSpeculativeExecution(Host host, long delay);

        void OnRequestError(Host host, RequestErrorType errorType, RetryDecision.RetryDecisionType decision);

        void OnRequestStart();

        void OnRequestFinish(Exception exception);
    }
}
