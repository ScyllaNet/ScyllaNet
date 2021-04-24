// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Metrics
{
    /// <summary>
    /// Interface implemented by <see cref="NodeMetric"/> and <see cref="SessionMetric"/>.
    /// </summary>
    public interface IMetric
    {
        /// <summary>
        /// Metric name.
        /// </summary>
        string Name { get; }
    }
}
