// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Insights.Schema.StartupMessage
{
    [JsonObject]
    internal class RuntimeInfo
    {
        [JsonProperty("runtimeFramework")]
        public string RuntimeFramework { get; set; }

        [JsonProperty("targetFramework")]
        public string TargetFramework { get; set; }

        [JsonProperty("dependencies")]
        public Dictionary<string, AssemblyInfo> Dependencies { get; set; }
    }
}
