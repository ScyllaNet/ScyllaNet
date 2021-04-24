// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics.Providers.Null
{
    /// <inheritdoc cref="IDriverTimer" />
    internal class NullDriverTimer : IDriverTimer
    {
        public static IDriverTimer Instance = new NullDriverTimer();

        private NullDriverTimer()
        {
        }
        
        public void Record(long elapsedNanoseconds)
        {
        }
    }
}
