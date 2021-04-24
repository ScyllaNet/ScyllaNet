// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Insights.Schema;

namespace Scylla.Net.DataStax.Insights.MessageFactories
{
    internal class InsightsMetadataFactory : IInsightsMetadataFactory
    {
        private readonly IInsightsMetadataTimestampGenerator _unixTimestampGenerator;

        public InsightsMetadataFactory(IInsightsMetadataTimestampGenerator unixTimestampGenerator)
        {
            _unixTimestampGenerator = unixTimestampGenerator;
        }

        public InsightsMetadata CreateInsightsMetadata(string messageName, string mappingId, InsightType insightType)
        {
            var millisecondsSinceEpoch = _unixTimestampGenerator.GenerateTimestamp();

            return new InsightsMetadata
            {
                Name = messageName,
                InsightMappingId = mappingId,
                InsightType = insightType,
                Tags = new Dictionary<string, string>
                {
                    { "language" , "csharp" }
                },
                Timestamp = millisecondsSinceEpoch
            };
        }
    }
}
