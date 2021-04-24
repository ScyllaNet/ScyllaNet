// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Metrics.Abstractions
{
    /// <summary>
    /// Represents an individual metric. Every metric type implements this interface:
    /// <see cref="IDriverGauge"/>, <see cref="IDriverMetric"/>, <see cref="IDriverTimer"/>, <see cref="IDriverCounter"/>.
    /// </summary>
    public interface IDriverMetric
    {
    }
}
