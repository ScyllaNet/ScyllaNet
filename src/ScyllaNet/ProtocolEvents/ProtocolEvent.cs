// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Scylla.Net.ProtocolEvents
{
    /// <summary>
    /// Protocol event associated with no keyspace in particular.
    /// </summary>
    internal class ProtocolEvent
    {
        public ProtocolEvent(Func<Task> handler)
        {
            Handler = handler;
        }

        public Func<Task> Handler { get; }
    }
}
