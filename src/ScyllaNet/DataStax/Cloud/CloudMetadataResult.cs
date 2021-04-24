// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Cloud
{
    [JsonObject]
    internal class CloudMetadataResult
    {
        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonRequired]
        [JsonProperty("contact_info")]
        public ContactInfo ContactInfo { get; set; }
    }
}
