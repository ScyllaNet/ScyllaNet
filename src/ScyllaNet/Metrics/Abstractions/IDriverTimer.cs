// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Metrics.Abstractions
{
    /// <summary>
    /// A <see cref="IDriverTimer"/> is a combination of a Histogram and a <see cref="IDriverMeter"/>
    /// allowing us to measure the duration of a type of event, the rate of its occurrence and provide duration statistics,
    /// for example tracking the time it takes to execute a particular CQL request.
    /// </summary>
    public interface IDriverTimer : IDriverMetric
    {
        /// <summary>
        /// Records an individual measurement in nanoseconds.
        /// </summary>
        void Record(long elapsedNanoseconds);
    }
}
