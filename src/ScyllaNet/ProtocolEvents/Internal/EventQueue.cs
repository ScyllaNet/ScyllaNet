// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scylla.Net.ProtocolEvents.Internal
{
    /// <summary>
    /// Used internally by <see cref="ProtocolEventDebouncer"/>.
    /// </summary>
    internal class EventQueue
    {
        public volatile ProtocolEvent MainEvent;

        public IList<TaskCompletionSource<bool>> Callbacks { get; } = new List<TaskCompletionSource<bool>>();

        public IDictionary<string, KeyspaceEvents> Keyspaces { get; } = new Dictionary<string, KeyspaceEvents>();
    }
}
