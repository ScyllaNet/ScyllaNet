// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Observers.Abstractions
{
    /// <summary>
    /// Observer with handlers for connection events, used for metrics for example.
    /// </summary>
    internal interface IConnectionObserver
    {
        void OnBytesSent(long size);

        void OnBytesReceived(long size);

        void OnErrorOnOpen(Exception exception);

        /// <summary>
        /// Creates an operation observer for an operation associated with the connection that is being observed by
        /// this instance.
        /// </summary>
        IOperationObserver CreateOperationObserver();
    }
}
