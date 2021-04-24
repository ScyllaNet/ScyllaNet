// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics.Registries
{
    /// <summary>
    /// Exposes metrics related to outcomes decided by retry policies.
    /// </summary>
    internal interface IRetryPolicyMetrics
    {
        IDriverCounter ReadTimeout { get; }

        IDriverCounter WriteTimeout { get; }

        IDriverCounter Unavailable { get; }

        IDriverCounter Other { get; }

        IDriverCounter Total { get; }
    }
}
