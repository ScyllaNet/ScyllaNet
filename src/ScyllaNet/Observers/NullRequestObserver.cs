// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.Observers.Abstractions;
using Scylla.Net.Requests;

namespace Scylla.Net.Observers
{
    internal class NullRequestObserver : IRequestObserver
    {
        public static readonly IRequestObserver Instance = new NullRequestObserver();

        private NullRequestObserver()
        {
        }

        public void OnSpeculativeExecution(Host host, long delay)
        {
        }

        public void OnRequestError(Host host, RequestErrorType errorType, RetryDecision.RetryDecisionType decision)
        {
        }

        public void OnRequestStart()
        {
        }

        public void OnRequestFinish(Exception exception)
        {
        }
    }
}
