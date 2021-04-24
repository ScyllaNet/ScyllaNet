// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Cloud
{
    [JsonObject]
    internal class ContactInfo
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("local_dc")]
        public string LocalDc { get; set; }

        [JsonRequired]
        [JsonProperty("contact_points")]
        public List<string> ContactPoints { get; set; }
        
        [JsonRequired]
        [JsonProperty("sni_proxy_address")]
        public string SniProxyAddress { get; set; }
    }
}
