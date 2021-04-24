// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.DataStax.Insights.Schema.StartupMessage;

namespace Scylla.Net.DataStax.Insights.InfoProviders.StartupMessage
{
    internal interface IPolicyInfoMapper<in T>
    {
        PolicyInfo GetPolicyInformation(T policy);
    }
}
