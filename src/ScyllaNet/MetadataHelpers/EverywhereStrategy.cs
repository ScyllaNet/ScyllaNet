// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Scylla.Net.MetadataHelpers
{
    internal class EverywhereStrategy : IReplicationStrategy, IEquatable<EverywhereStrategy>
    {
        private static readonly int HashCode = typeof(EverywhereStrategy).GetHashCode();

        private EverywhereStrategy()
        {
        }

        public static IReplicationStrategy Instance { get; } = new EverywhereStrategy();

        public Dictionary<IToken, ISet<Host>> ComputeTokenToReplicaMap(
            IReadOnlyList<IToken> ring, 
            IReadOnlyDictionary<IToken, Host> primaryReplicas,
            int numberOfHostsWithTokens,
            IReadOnlyDictionary<string, DatacenterInfo> datacenters)
        {
            var allHosts = primaryReplicas.Values.Distinct();
            return primaryReplicas.ToDictionary(kvp => kvp.Key, kvp => new HashSet<Host>(allHosts) as ISet<Host>);
        }

        public bool Equals(IReplicationStrategy other)
        {
            return TypedEquals(other as EverywhereStrategy);
        }

        public override bool Equals(object obj)
        {
            return TypedEquals(obj as EverywhereStrategy);
        }

        public bool Equals(EverywhereStrategy other)
        {
            return TypedEquals(other);
        }

        private bool TypedEquals(EverywhereStrategy other)
        {
            return other != null;
        }

        public override int GetHashCode()
        {
            return EverywhereStrategy.HashCode;
        }
    }
}
