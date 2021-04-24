// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    /// Represents client-side, microsecond-precision query timestamps.
    /// <para>
    /// Given that Apache Cassandra uses those timestamps to resolve conflicts, implementations should generate
    /// monotonically increasing timestamps for successive invocations of <see cref="Next()"/>.
    /// </para>
    /// </summary>
    public interface ITimestampGenerator
    {
        /// <summary>
        /// Returns the next timestamp in microseconds since UNIX epoch.
        /// <para>
        /// Implementers should enforce increasing monotonic timestamps, that is,
        /// a timestamp returned should always be strictly greater that any previously returned
        /// timestamp.
        /// </para>
        /// <para>
        /// Implementers should strive to achieve microsecond precision in the best possible way,
        /// which is usually largely dependent on the underlying operating system's capabilities.
        /// </para>
        /// </summary>
        /// <returns>
        /// The next timestamp (in microseconds). When returning <see cref="long.MinValue"/>, the driver
        /// will not set the timestamp, letting Apache Cassandra generate a server-side timestamp.
        /// </returns>
        long Next();
    }
}
