// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    /// Represents a plan that governs speculative executions for a given query.
    /// </summary>
    public interface ISpeculativeExecutionPlan
    {
        /// <summary>
        /// Returns the time before the next speculative query.
        /// </summary>
        /// <param name="lastQueried">the host that was just queried.</param>
        /// <returns>the time (in milliseconds) before a speculative query is sent to the next host. If zero or negative, no speculative query will be sent.</returns>
        long NextExecution(Host lastQueried);
    }
}
