// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Threading.Tasks;

namespace Scylla.Net.ProtocolEvents
{
    /// <summary>
    /// Component that coalesces protocol events into a single queue.
    /// The public methods allow the client to schedule events for processing. Each scheduled event
    /// will move the sliding window of the internal timer forward until a certain max delay has been hit.
    /// The entire queue will be processed once the timeout has passed without any more events coming in or after
    /// the max delay has passed.
    /// </summary>
    internal interface IProtocolEventDebouncer
    {
        /// <summary>
        /// Returned task will be complete when the event has been scheduled for processing.
        /// </summary>
        Task ScheduleEventAsync(ProtocolEvent ev, bool processNow);

        /// <summary>
        /// Returned task will be complete when the event has been processed.
        /// </summary>
        Task HandleEventAsync(ProtocolEvent ev, bool processNow);

        Task ShutdownAsync();
    }
}
