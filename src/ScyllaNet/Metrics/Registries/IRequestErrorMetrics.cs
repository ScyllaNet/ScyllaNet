// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics.Registries
{
    /// <summary>
    /// Exposes request error metrics.
    /// </summary>
    internal interface IRequestErrorMetrics
    {
        IDriverCounter Aborted { get; }

        IDriverCounter ReadTimeout { get; }

        IDriverCounter WriteTimeout { get; }

        IDriverCounter Unavailable { get; }

        IDriverCounter ClientTimeout { get; }

        IDriverCounter Other { get; }

        IDriverCounter Unsent { get; }

        IDriverCounter ConnectionInitErrors { get; }

        IDriverCounter AuthenticationErrors { get; }
    }
}
