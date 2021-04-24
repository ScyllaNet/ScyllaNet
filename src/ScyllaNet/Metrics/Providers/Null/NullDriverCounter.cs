// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Metrics.Abstractions;

namespace Scylla.Net.Metrics.Providers.Null
{
    internal class NullDriverCounter : IDriverCounter
    {
        public static IDriverCounter Instance = new NullDriverCounter();

        private NullDriverCounter()
        {
        }
        
        public void Increment(long value)
        {
        }
        
        public void Increment()
        {
        }
    }
}
