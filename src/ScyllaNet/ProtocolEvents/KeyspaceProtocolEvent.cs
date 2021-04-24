// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Scylla.Net.ProtocolEvents
{
    /// <summary>
    /// Protocol event associated with a single keyspace.
    /// </summary>
    internal class KeyspaceProtocolEvent : ProtocolEvent
    {
        public KeyspaceProtocolEvent(bool isRefreshKeyspaceEvent, string keyspace, Func<Task> handler)
            : base(handler)
        {
            IsRefreshKeyspaceEvent = isRefreshKeyspaceEvent;
            Keyspace = keyspace;
        }

        public bool IsRefreshKeyspaceEvent { get; }

        public string Keyspace { get; }
    }
}
