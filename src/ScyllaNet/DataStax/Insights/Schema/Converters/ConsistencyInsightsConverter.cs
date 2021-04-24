// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.DataStax.Insights.Schema.Converters
{
    internal class ConsistencyInsightsConverter : InsightsEnumConverter<ConsistencyLevel, string>
    {
        private static readonly IReadOnlyDictionary<ConsistencyLevel, string> ConsistencyLevelStringMap =
            new Dictionary<ConsistencyLevel, string>
            {
                { ConsistencyLevel.All, "ALL" },
                { ConsistencyLevel.Any, "ANY" },
                { ConsistencyLevel.EachQuorum, "EACH_QUORUM" },
                { ConsistencyLevel.LocalOne, "LOCAL_ONE" },
                { ConsistencyLevel.LocalQuorum, "LOCAL_QUORUM" },
                { ConsistencyLevel.LocalSerial, "LOCAL_SERIAL" },
                { ConsistencyLevel.One, "ONE" },
                { ConsistencyLevel.Quorum, "QUORUM" },
                { ConsistencyLevel.Serial, "SERIAL" },
                { ConsistencyLevel.Three, "THREE" },
                { ConsistencyLevel.Two, "TWO" }
            };

        protected override IReadOnlyDictionary<ConsistencyLevel, string> EnumToJsonValueMap => 
            ConsistencyInsightsConverter.ConsistencyLevelStringMap;
    }
}
