// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Insights.Schema.StatusMessage
{
    [JsonObject]
    internal class InsightsStatusData
    {
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }
        
        [JsonProperty("controlConnection")]
        public string ControlConnection { get; set; }

        [JsonProperty("connectedNodes")]
        public Dictionary<string, NodeStatusInfo> ConnectedNodes { get; set; }
    }
}
