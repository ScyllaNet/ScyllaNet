// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Cloud
{
    [JsonObject]
    internal class CloudConfiguration
    {
        [JsonProperty("host")]
        public string Host { get; private set; }

        [JsonProperty("port")]
        public int Port { get; private set; }
        
        [JsonProperty("pfxCertPassword")]
        public string CertificatePassword { get; private set; }
    }
}
