// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Linq;
using Scylla.Net.SessionManagement;

namespace Scylla.Net.DataStax.Insights
{
    internal class InsightsSupportVerifier : IInsightsSupportVerifier
    {
        private static readonly Version MinDse6Version = new Version(6, 0, 5);
        private static readonly Version MinDse51Version = new Version(5, 1, 13);
        private static readonly Version Dse600Version = new Version(6, 0, 0);

        public bool SupportsInsights(IInternalCluster cluster)
        {
            var allHosts = cluster.AllHosts();
            return allHosts.Count != 0 && allHosts.All(h => DseVersionSupportsInsights(h.DseVersion));
        }
        
        public bool DseVersionSupportsInsights(Version dseVersion)
        {
            if (dseVersion == null)
            {
                return false;
            }

            if (dseVersion >= InsightsSupportVerifier.MinDse6Version)
            {
                return true;
            }

            if (dseVersion < InsightsSupportVerifier.Dse600Version)
            {
                if (dseVersion >= InsightsSupportVerifier.MinDse51Version)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
