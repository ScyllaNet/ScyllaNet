// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Connections;
using Scylla.Net.Responses;

namespace Scylla.Net.Observers.Abstractions
{
    /// <summary>
    /// Exposes callbacks to handle a couple of events related to <see cref="OperationState"/>.
    /// </summary>
    internal interface IOperationObserver
    {
        void OnOperationSend(long requestSize, long timestamp);

        void OnOperationReceive(IRequestError exception, Response response, long timestamp);
    }
}
