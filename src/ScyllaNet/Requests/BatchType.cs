// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

﻿namespace Scylla.Net
{
    /// <summary>
    /// The type of batch to use
    /// </summary>
    public enum BatchType
    {
        /// <summary>
        /// A logged batch: Cassandra will first write the batch to its distributed batch log to ensure the atomicity of the batch.
        /// </summary>
        Logged = 0,
        /// <summary>
        /// An unlogged batch: The batch will not be written to the batch log and atomicity of the batch is NOT guaranteed.
        /// </summary>
        Unlogged = 1,
        /// <summary>
        /// A counter batch
        /// </summary>
        Counter = 2
    }
}
