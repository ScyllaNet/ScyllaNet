// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.MetadataHelpers
{
    internal class ReplicationFactor : IEquatable<ReplicationFactor>, IComparable<ReplicationFactor>
    {
        private ReplicationFactor(int allReplicas, int transientReplicas)
        {
            AllReplicas = allReplicas;
            TransientReplicas = transientReplicas;
            FullReplicas = allReplicas - transientReplicas;
        }

        public int AllReplicas { get; }

        public int TransientReplicas { get; }

        public int FullReplicas { get; }

        public bool HasTransientReplicas() => AllReplicas != FullReplicas;

        public override string ToString()
        {
            return AllReplicas + (HasTransientReplicas() ? "/" + TransientReplicas : "");
        }

        public static ReplicationFactor Parse(string rf)
        {
            if (rf == null)
            {
                throw new ArgumentNullException(nameof(rf));
            }

            var slashIndex = rf.IndexOf('/');
            if (slashIndex < 0)
            {
                return new ReplicationFactor(ReplicationFactor.ParseNumberOfReplicas(rf), 0);
            }

            var allPart = rf.Substring(0, slashIndex);
            var transientPart = rf.Substring(slashIndex + 1);
            var parsedAllPart = ReplicationFactor.ParseNumberOfReplicas(allPart);
            var parsedTransientPart = ReplicationFactor.ParseNumberOfReplicas(transientPart);
            
            return new ReplicationFactor(parsedAllPart, parsedTransientPart);
        }

        private static int ParseNumberOfReplicas(string numberOfReplicas)
        {
            if (!int.TryParse(numberOfReplicas, out var parsed))
            {
                throw new FormatException("Value of keyspace strategy option is in invalid format!");
            }

            return parsed;
        }

        public bool Equals(ReplicationFactor other)
        {
            if (other == null)
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return AllReplicas == other.AllReplicas 
                   && TransientReplicas == other.TransientReplicas;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ReplicationFactor);
        }

        public override int GetHashCode()
        {
            return Utils.CombineHashCode(new object[] { AllReplicas, TransientReplicas });
        }

        public int CompareTo(ReplicationFactor other)
        {
            if (object.ReferenceEquals(this, other))
            {
                return 0;
            }

            if (other == null)
            {
                return 1;
            }

            var allReplicasComparison = AllReplicas.CompareTo(other.AllReplicas);
            return allReplicasComparison != 0 
                ? allReplicasComparison 
                : TransientReplicas.CompareTo(other.TransientReplicas);
        }
    }
}
