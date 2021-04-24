// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Insights.Schema.StatusMessage
{
    [JsonObject]
    internal class NodeStatusInfo
    {
        [JsonProperty("connections")]
        public int Connections { get; set; }

        [JsonProperty("inFlightQueries")]
        public int InFlightQueries { get; set; }
    }
}
