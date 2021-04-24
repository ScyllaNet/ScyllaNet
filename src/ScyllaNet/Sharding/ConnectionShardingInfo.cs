// Copyright (c) 2021, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.Sharding
{
    public class ConnectionShardingInfo
    {
        private const string SCYLLA_SHARD_PARAM_KEY = "SCYLLA_SHARD";
        private const string SCYLLA_NR_SHARDS_PARAM_KEY = "SCYLLA_NR_SHARDS";
        private const string SCYLLA_PARTITIONER = "SCYLLA_PARTITIONER";
        private const string SCYLLA_SHARDING_ALGORITHM = "SCYLLA_SHARDING_ALGORITHM";
        private const string SCYLLA_SHARDING_IGNORE_MSB = "SCYLLA_SHARDING_IGNORE_MSB"; 
        private readonly int? _shardId;
        private readonly ShardingInfo _shardingInfo;

        private ConnectionShardingInfo(int shardId, ShardingInfo shardingInfo)
        {
            _shardId = shardId;
            _shardingInfo = shardingInfo;
        }

        public static ConnectionShardingInfo ParseShardingInfo(IDictionary<string, string[]> parametros)
        {
            var shardId = parametros.TryGetValue(SCYLLA_SHARD_PARAM_KEY, out var shardIds) ? (int?)int.Parse(shardIds[0]) : null;
            var shardsCount = parametros.TryGetValue(SCYLLA_NR_SHARDS_PARAM_KEY, out var nrShards) ? (int?)int.Parse(nrShards[0]) : null;
            var shardingIgnoreMSB = parametros.TryGetValue(SCYLLA_SHARDING_IGNORE_MSB, out var msb) ? (int?)int.Parse(msb[0]) : null;
            var partitioner = parametros.TryGetValue(SCYLLA_PARTITIONER, out var partitioners) ? partitioners[0] : null;
            var shardingAlgorithm = parametros.TryGetValue(SCYLLA_SHARDING_ALGORITHM, out var algorithm) ? algorithm[0] : null;


            if (shardId == null
                || shardsCount == null
                || partitioner == null
                || shardingAlgorithm == null
                || shardingIgnoreMSB == null
                || !partitioner.Equals("org.apache.cassandra.dht.Murmur3Partitioner")
                || !shardingAlgorithm.Equals("biased-token-round-robin"))
            {
                return null;
            }

            return new ConnectionShardingInfo(
                shardId.Value,
                new ShardingInfo(
                    shardsCount.Value,
                    partitioner,
                    shardingAlgorithm,
                    shardingIgnoreMSB.Value));
        }

        public int? GetShardId() => _shardId;
        public ShardingInfo GetShardingInfo() => _shardingInfo;
    }
}
