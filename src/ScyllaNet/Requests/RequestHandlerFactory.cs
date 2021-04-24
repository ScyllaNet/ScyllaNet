// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.ExecutionProfiles;
using Scylla.Net.Serialization;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.Requests
{
    internal class RequestHandlerFactory : IRequestHandlerFactory
    {
        public IRequestHandler Create(IInternalSession session, ISerializer serializer, IRequest request, IStatement statement, IRequestOptions options)
        {
            return new RequestHandler(session, serializer, request, statement, options);
        }

        public IRequestHandler Create(
            IInternalSession session, ISerializer serializer, IStatement statement, IRequestOptions options)
        {
            return new RequestHandler(session, serializer, statement, options);
        }

        public IRequestHandler Create(IInternalSession session, ISerializer serializer)
        {
            return new RequestHandler(session, serializer);
        }

        public IGraphRequestHandler CreateGraphRequestHandler(IInternalSession session, IGraphTypeSerializerFactory graphTypeSerializerFactory)
        {
            return new GraphRequestHandler(session, graphTypeSerializerFactory);
        }
    }
}
