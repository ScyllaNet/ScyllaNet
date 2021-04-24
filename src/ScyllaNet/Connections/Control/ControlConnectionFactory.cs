// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.ProtocolEvents;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.Connections.Control
{
    internal class ControlConnectionFactory : IControlConnectionFactory
    {
        public IControlConnection Create(
            IInternalCluster cluster,
            IProtocolEventDebouncer protocolEventDebouncer, 
            ProtocolVersion initialProtocolVersion, 
            Configuration config, 
            Metadata metadata,
            IEnumerable<IContactPoint> contactPoints)
        {
            return new ControlConnection(
                cluster,
                protocolEventDebouncer, 
                initialProtocolVersion, 
                config, 
                metadata,
                contactPoints);
        }
    }
}
