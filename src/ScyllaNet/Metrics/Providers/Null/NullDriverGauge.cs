﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics.Providers.Null
{
    internal class NullDriverGauge : IDriverGauge
    {
        public static IDriverGauge Instance = new NullDriverGauge();

        private NullDriverGauge()
        {
        }

        public void Dispose()
        {
        }
    }
}
