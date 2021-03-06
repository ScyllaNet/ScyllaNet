// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Observers.Abstractions;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.Requests
{
    internal class RequestExecutionFactory : IRequestExecutionFactory
    {
        public IRequestExecution Create(
            IRequestHandler parent, IInternalSession session, IRequest request, IRequestObserver requestObserver)
        {
            return new RequestExecution(parent, session, request, requestObserver);
        }
    }
}
