// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.DataStax.Insights.MessageFactories
{
    internal class InsightsMetadataTimestampGenerator : IInsightsMetadataTimestampGenerator
    {
        private static readonly DateTimeOffset UnixEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);

        public long GenerateTimestamp()
        {
            var t = DateTimeOffset.UtcNow - InsightsMetadataTimestampGenerator.UnixEpoch;
            return (long) t.TotalMilliseconds;
        }
    }
}
