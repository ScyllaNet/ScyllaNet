// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.MetadataHelpers
{
    internal struct DatacenterReplicationFactor : IEquatable<DatacenterReplicationFactor>, IComparable<DatacenterReplicationFactor>
    {
        private readonly int _hashCode;

        public DatacenterReplicationFactor(string datacenter, ReplicationFactor replicationFactor)
        {
            Datacenter = datacenter ?? throw new ArgumentNullException(nameof(datacenter));
            ReplicationFactor = replicationFactor;
            _hashCode = DatacenterReplicationFactor.ComputeHashCode(Datacenter, ReplicationFactor);
        }

        public string Datacenter { get; }

        public ReplicationFactor ReplicationFactor { get; }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return obj.GetType() == GetType() 
                   && Equals((DatacenterReplicationFactor)obj);
        }

        public bool Equals(DatacenterReplicationFactor other)
        {
            return Datacenter == other.Datacenter &&
                   ReplicationFactor.Equals(other.ReplicationFactor);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        private static int ComputeHashCode(string datacenter, ReplicationFactor replicationFactor)
        {
            return Utils.CombineHashCode(new object[] { datacenter, replicationFactor });
        }

        public int CompareTo(DatacenterReplicationFactor other)
        {
            var dcComparison = string.Compare(Datacenter, other.Datacenter, StringComparison.Ordinal);
            return dcComparison != 0 
                ? dcComparison
                : ReplicationFactor.CompareTo(other.ReplicationFactor);
        }
    }
}
