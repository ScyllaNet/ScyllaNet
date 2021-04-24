// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.DataStax.Insights.Schema;

namespace Scylla.Net.DataStax.Insights.MessageFactories
{
    internal interface IInsightsMetadataFactory
    {
        InsightsMetadata CreateInsightsMetadata(string messageName, string mappingId, InsightType insightType);
    }
}
