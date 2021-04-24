// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Scylla.Net.MetadataHelpers
{
    internal class ReplicationStrategyFactory : IReplicationStrategyFactory
    {
        private static readonly Logger Logger = new Logger(typeof(ReplicationStrategyFactory));

        public IReplicationStrategy Create(
            string strategyClass, IReadOnlyDictionary<string, ReplicationFactor> replicationOptions)
        {
            if (strategyClass == null)
            {
                throw new ArgumentNullException(nameof(strategyClass));
            }

            if (replicationOptions == null)
            {
                throw new ArgumentNullException(nameof(replicationOptions));
            }

            if (strategyClass.Equals(ReplicationStrategies.SimpleStrategy, StringComparison.OrdinalIgnoreCase))
            {
                return replicationOptions.TryGetValue("replication_factor", out var replicationFactorValue)
                    ? new SimpleStrategy(replicationFactorValue)
                    : null;
            }

            if (strategyClass.Equals(ReplicationStrategies.NetworkTopologyStrategy, StringComparison.OrdinalIgnoreCase)) 
            {
                return new NetworkTopologyStrategy(replicationOptions);
            }

            if (strategyClass.Equals(ReplicationStrategies.LocalStrategy, StringComparison.OrdinalIgnoreCase)) 
            {
                return LocalStrategy.Instance;
            }

            if (strategyClass.Equals(ReplicationStrategies.EverywhereStrategy, StringComparison.OrdinalIgnoreCase)) 
            {
                return EverywhereStrategy.Instance;
            }

            ReplicationStrategyFactory.Logger.Info($"Replication Strategy class name not recognized: {strategyClass}");

            return null;
        }
    }
}
