// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Serialization;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.Requests
{
    internal class PrepareHandlerFactory : IPrepareHandlerFactory
    {
        public IPrepareHandler CreatePrepareHandler(
            ISerializerManager serializerManager, IInternalCluster cluster)
        {
            return new PrepareHandler(serializerManager, cluster, CreateReprepareHandler());
        }

        public IReprepareHandler CreateReprepareHandler()
        {
            return new ReprepareHandler();
        }
    }
}
