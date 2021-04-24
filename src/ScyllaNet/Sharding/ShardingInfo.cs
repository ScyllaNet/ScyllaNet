// Copyright (c) 2021, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Sharding
{
    public class ShardingInfo
    {
        private readonly int _shardsCount;
        private readonly string _partitioner;
        private readonly string _shardingAlgorithm;
        private readonly int _shardingIgnoreMSB;

        public ShardingInfo(
            int shardsCount,
            string partitioner,
            string shardingAlgorithm,
            int shardingIgnoreMSB)
        {
            _shardsCount = shardsCount;
            _partitioner = partitioner;
            _shardingAlgorithm = shardingAlgorithm;
            _shardingIgnoreMSB = shardingIgnoreMSB;
        }

        public int GetShardsCount()
        {
            return _shardsCount;
        }
    } 
}
