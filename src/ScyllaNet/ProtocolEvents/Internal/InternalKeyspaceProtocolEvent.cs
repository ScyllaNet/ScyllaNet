// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Threading.Tasks;

namespace Scylla.Net.ProtocolEvents.Internal
{
    /// <summary>
    /// Used internally by <see cref="ProtocolEventDebouncer"/>.
    /// </summary>
    internal class InternalKeyspaceProtocolEvent
    {
        public KeyspaceProtocolEvent KeyspaceEvent { get; set; }

        public TaskCompletionSource<bool> Callback { get; set; }
    }

}
