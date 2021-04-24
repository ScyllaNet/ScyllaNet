// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Insights.Schema.StartupMessage
{
    [JsonObject]
    internal class PolicyInfo
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("options")]
        public Dictionary<string, object> Options { get; set; }
    }
}
