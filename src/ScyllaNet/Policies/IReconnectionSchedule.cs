// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

﻿namespace Scylla.Net
{
    /// <summary>
    ///  Schedules reconnection attempts to a node.
    /// </summary>
    public interface IReconnectionSchedule
    {
        /// <summary>
        ///  When to attempt the next reconnection. This method will be called once when
        ///  the host is detected down to schedule the first reconnection attempt, and
        ///  then once after each failed reconnection attempt to schedule the next one.
        ///  Hence each call to this method are free to return a different value.
        /// </summary>
        /// 
        /// <returns>a time in milliseconds to wait before attempting the next
        ///  reconnection.</returns>
        long NextDelayMs();
    }
}
