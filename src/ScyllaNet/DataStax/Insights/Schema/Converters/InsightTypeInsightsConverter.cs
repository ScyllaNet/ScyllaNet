// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.DataStax.Insights.Schema.Converters
{
    internal class InsightTypeInsightsConverter : InsightsEnumConverter<InsightType, string>
    {
        private static readonly IReadOnlyDictionary<InsightType, string> InsightTypeStringMap =
            new Dictionary<InsightType, string>
            {
                { InsightType.Event, "EVENT" }
            };

        protected override IReadOnlyDictionary<InsightType, string> EnumToJsonValueMap => 
            InsightTypeInsightsConverter.InsightTypeStringMap;
    }
}
