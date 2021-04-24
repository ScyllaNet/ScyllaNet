﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Globalization;

namespace Scylla.Net
{
    /// <summary>
    /// Provides utility methods to build replication strategies when creating a keyspace
    /// </summary>
    public static class ReplicationStrategies
    {
        public const string NetworkTopologyStrategy = "NetworkTopologyStrategy";
        public const string SimpleStrategy = "SimpleStrategy";

        // these two are internal because users shouldn't use these
        internal const string EverywhereStrategy = "EverywhereStrategy";
        internal const string LocalStrategy = "LocalStrategy";
        
        /// <summary>
        ///  Returns replication property for SimpleStrategy.
        /// </summary>        
        /// <param name="replicationFactor">Replication factor for the whole cluster.</param>
        /// <returns>a dictionary of replication property sub-options.</returns>         
        public static Dictionary<string, string> CreateSimpleStrategyReplicationProperty(int replicationFactor)
        {
            return new Dictionary<string, string> {{"class", SimpleStrategy}, {"replication_factor", replicationFactor.ToString(CultureInfo.InvariantCulture)}};
        }


        /// <summary>
        ///  Returns replication property for NetworkTopologyStrategy.
        /// </summary>        
        /// <param name="datacentersReplicationFactors">Dictionary in which key is the name of a data-center,
        /// value is a replication factor for that data-center.</param>
        /// <returns>a dictionary of replication property sub-options.</returns>         
        public static Dictionary<string, string> CreateNetworkTopologyStrategyReplicationProperty(
            Dictionary<string, int> datacentersReplicationFactors)
        {
            var result = new Dictionary<string, string> {{"class", NetworkTopologyStrategy}};
            if (datacentersReplicationFactors.Count <= 0)
            {
                return result;
            }
            foreach (var datacenter in datacentersReplicationFactors)
            {
                result.Add(datacenter.Key, datacenter.Value.ToString(CultureInfo.InvariantCulture));
            }
            return result;
        }


        /// <summary>
        ///  Returns replication property for other replication strategy. 
        ///  Use it only if there is no dedicated method that creates replication property for specified replication strategy.
        /// </summary>
        /// <param name="strategyClass">Name of replication strategy.</param>
        /// <param name="subOptions">Dictionary in which key is the name of sub-option,
        /// value is a value for that sub-option.</param>
        /// <returns>a dictionary of replication property sub-options.</returns>         
        public static Dictionary<string, string> CreateReplicationProperty(string strategyClass, Dictionary<string, string> subOptions)
        {
            var result = new Dictionary<string, string> {{"class", strategyClass}};
            if (subOptions.Count <= 0)
            {
                return result;
            }
            foreach (var elem in subOptions)
            {
                result.Add(elem.Key, elem.Value);
            }
            return result;
        }
    }
}
