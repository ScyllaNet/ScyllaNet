// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Insights.Schema.Converters;
using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Insights.Schema
{
    [JsonObject]
    internal class InsightsMetadata
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("tags")]
        public Dictionary<string, string> Tags { get; set; }

        [JsonProperty("insightType")]
        [JsonConverter(typeof(InsightTypeInsightsConverter))]
        public InsightType InsightType { get; set; }

        [JsonProperty("insightMappingId")]
        public string InsightMappingId { get; set; }
    }
}
