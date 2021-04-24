// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Insights.Schema.Converters;
using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Insights.Schema.StartupMessage
{
    [JsonObject]
    internal class ExecutionProfileInfo
    {
        [JsonProperty("readTimeout")]
        public int? ReadTimeout { get; set; }
        
        [JsonProperty("retry")]
        public PolicyInfo Retry { get; set; }

        [JsonProperty("loadBalancing")]
        public PolicyInfo LoadBalancing { get; set; }

        [JsonProperty("speculativeExecution")]
        public PolicyInfo SpeculativeExecution { get; set; }

        [JsonProperty("consistency")]
        [JsonConverter(typeof(ConsistencyInsightsConverter))]
        public ConsistencyLevel? Consistency { get; set; }

        [JsonProperty("serialConsistency")]
        [JsonConverter(typeof(ConsistencyInsightsConverter))]
        public ConsistencyLevel? SerialConsistency { get; set; }

        [JsonProperty("graphOptions")]
        public Dictionary<string, object> GraphOptions { get; set; }
    }
}
