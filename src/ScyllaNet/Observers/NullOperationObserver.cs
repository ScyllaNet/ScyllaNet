// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Connections;
using Scylla.Net.Observers.Abstractions;
using Scylla.Net.Responses;

namespace Scylla.Net.Observers
{
    internal class NullOperationObserver : IOperationObserver
    {
        public static readonly IOperationObserver Instance = new NullOperationObserver();

        private NullOperationObserver()
        {
        }

        public void OnOperationSend(long requestSize, long timestamp)
        {
        }

        public void OnOperationReceive(IRequestError error, Response response, long timestamp)
        {
        }
    }
}
