// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.Observers.Abstractions;

namespace Scylla.Net.Observers
{
    internal class NullConnectionObserver : IConnectionObserver
    {
        public static readonly IConnectionObserver Instance = new NullConnectionObserver();

        private NullConnectionObserver()
        {
        }

        public void OnBytesSent(long size)
        {
        }

        public void OnBytesReceived(long size)
        {
        }

        public void OnErrorOnOpen(Exception exception)
        {
        }

        public IOperationObserver CreateOperationObserver()
        {
            return NullOperationObserver.Instance;
        }
    }
}
