// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.DataStax.Insights.Schema.Converters
{
    internal class CompressionTypeInsightsConverter : InsightsEnumConverter<CompressionType, string>
    {
        private static readonly IReadOnlyDictionary<CompressionType, string> CompressionTypeStringMap =
            new Dictionary<CompressionType, string>
            {
                { CompressionType.LZ4, "LZ4" },
                { CompressionType.NoCompression, "NONE" },
                { CompressionType.Snappy, "SNAPPY" }
            };
        
        protected override IReadOnlyDictionary<CompressionType, string> EnumToJsonValueMap =>
            CompressionTypeInsightsConverter.CompressionTypeStringMap;
    }
}
