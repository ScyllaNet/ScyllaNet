// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Threading.Tasks;
using Scylla.Net.Observers.Abstractions;

namespace Scylla.Net.Requests
{
    internal class TcsMetricsRequestResultHandler : IRequestResultHandler
    {
        private readonly IRequestObserver _requestObserver;
        private readonly TaskCompletionSource<RowSet> _taskCompletionSource;

        public TcsMetricsRequestResultHandler(IRequestObserver requestObserver)
        {
            _requestObserver = requestObserver;
            _taskCompletionSource = new TaskCompletionSource<RowSet>();
            _requestObserver.OnRequestStart();
        }

        public void TrySetResult(RowSet result)
        {
            if (_taskCompletionSource.TrySetResult(result))
            {
                _requestObserver.OnRequestFinish(null);
            }
        }

        public void TrySetException(Exception exception)
        {
            if (_taskCompletionSource.TrySetException(exception))
            {
                _requestObserver.OnRequestFinish(exception);
            }
        }

        public Task<RowSet> Task => _taskCompletionSource.Task;
    }
}
