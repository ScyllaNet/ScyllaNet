// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    /// <summary>
    ///  An unexpected error happened internally. This should never be raise and
    ///  indicates an unexpected behavior (either in the driver or in Cassandra).
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Naming", 
        "CA1710:Identifiers should have correct suffix", 
        Justification = "Public API")]
    public class DriverInternalError : Exception
    {
        public DriverInternalError(string message)
            : base(message)
        {
        }

        public DriverInternalError(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
