// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Insights.Schema
{
    [JsonObject]
    internal class Insight<T>
    {
        [JsonProperty("metadata")]
        public InsightsMetadata Metadata;
        
        [JsonProperty("data")]
        public T Data;
    }
}
