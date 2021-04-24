// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    /// Specifies the different graph protocol versions that are supported by the driver
    /// </summary>
    public enum GraphProtocol
    {
        /// <summary>
        /// GraphSON v1.
        /// </summary>
        GraphSON1 = 1,

        /// <summary>
        /// GraphSON v2.
        /// </summary>
        GraphSON2 = 2,

        /// <summary>
        /// GraphSON v3.
        /// </summary>
        GraphSON3 = 3
    }
}
