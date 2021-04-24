// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Scylla.Net.MetadataHelpers
{
    internal class LocalStrategy : IReplicationStrategy, IEquatable<LocalStrategy>
    {
        private static readonly int HashCode = typeof(LocalStrategy).GetHashCode();

        private LocalStrategy()
        {
        }

        public static IReplicationStrategy Instance { get; } = new LocalStrategy();

        public Dictionary<IToken, ISet<Host>> ComputeTokenToReplicaMap(
            IReadOnlyList<IToken> ring, 
            IReadOnlyDictionary<IToken, Host> primaryReplicas,
            int numberOfHostsWithTokens,
            IReadOnlyDictionary<string, DatacenterInfo> datacenters)
        {
            return primaryReplicas.ToDictionary(kvp => kvp.Key, kvp => new HashSet<Host> { kvp.Value } as ISet<Host>);
        }

        public bool Equals(IReplicationStrategy other)
        {
            return TypedEquals(other as LocalStrategy);
        }

        public override bool Equals(object obj)
        {
            return TypedEquals(obj as LocalStrategy);
        }

        public bool Equals(LocalStrategy other)
        {
            return TypedEquals(other);
        }

        private bool TypedEquals(LocalStrategy other)
        {
            return other != null;
        }

        public override int GetHashCode()
        {
            return LocalStrategy.HashCode;
        }
    }
}
