// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Insights.Schema.Converters;
using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Insights.Schema.StartupMessage
{
    [JsonObject]
    internal class InsightsStartupData
    {
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("applicationName")]
        public string ApplicationName { get; set; }

        [JsonProperty("applicationVersion")]
        public string ApplicationVersion { get; set; }

        [JsonProperty("contactPoints")]
        public Dictionary<string, List<string>> ContactPoints { get; set; }

        [JsonProperty("initialControlConnection")]
        public string InitialControlConnection { get; set; }

        [JsonProperty("protocolVersion")]
        public byte ProtocolVersion { get; set; }

        [JsonProperty("localAddress")]
        public string LocalAddress { get; set; }

        [JsonProperty("executionProfiles")]
        public Dictionary<string, ExecutionProfileInfo> ExecutionProfiles { get; set; }

        [JsonProperty("poolSizeByHostDistance")]
        public PoolSizeByHostDistance PoolSizeByHostDistance { get; set; }

        [JsonProperty("heartbeatInterval")]
        public long HeartbeatInterval { get; set; }

        [JsonProperty("compression")]
        [JsonConverter(typeof(CompressionTypeInsightsConverter))]
        public CompressionType Compression { get; set; }

        [JsonProperty("reconnectionPolicy")]
        public PolicyInfo ReconnectionPolicy { get; set; }

        [JsonProperty("ssl")]
        public SslInfo Ssl { get; set; }

        [JsonProperty("authProvider")]
        public AuthProviderInfo AuthProvider { get; set; }

        [JsonProperty("otherOptions")]
        public Dictionary<string, object> OtherOptions { get; set; }

        [JsonProperty("configAntiPatterns")]
        public Dictionary<string, string> ConfigAntiPatterns { get; set; }

        [JsonProperty("periodicStatusInterval")]
        public long PeriodicStatusInterval { get; set; }

        [JsonProperty("platformInfo")]
        public InsightsPlatformInfo PlatformInfo { get; set; }

        [JsonProperty("hostName")]
        public string HostName { get; set; }

        [JsonProperty("driverName")]
        public string DriverName { get; set; }

        [JsonProperty("applicationNameWasGenerated")]
        public bool ApplicationNameWasGenerated { get; set; }

        [JsonProperty("driverVersion")]
        public string DriverVersion { get; set; }

        [JsonProperty("dataCenters")]
        public HashSet<string> DataCenters { get; set; }
    }
}
