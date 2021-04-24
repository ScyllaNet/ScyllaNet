// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.Collections;

namespace Scylla.Net.MetadataHelpers
{
    internal struct NetworkTopologyTokenMapContext
    {
        public NetworkTopologyTokenMapContext(
            IReadOnlyList<IToken> ring, 
            IReadOnlyDictionary<IToken, Host> primaryReplicas, 
            IReadOnlyDictionary<string, DatacenterInfo> datacenters)
        {
            Ring = ring;
            PrimaryReplicas = primaryReplicas;
            Datacenters = datacenters;
            ReplicasByDc = new Dictionary<string, int>();
            TokenReplicas = new OrderedHashSet<Host>();
            RacksAdded = new Dictionary<string, HashSet<string>>();
            SkippedHosts = new List<Host>();
        }

        public IList<Host> SkippedHosts { get; }

        public IDictionary<string, HashSet<string>> RacksAdded { get; }

        public OrderedHashSet<Host> TokenReplicas { get; }

        public IDictionary<string, int> ReplicasByDc { get; }

        public IReadOnlyList<IToken> Ring { get; }

        public IReadOnlyDictionary<IToken, Host> PrimaryReplicas { get; }

        public IReadOnlyDictionary<string, DatacenterInfo> Datacenters { get; }
    }
}
