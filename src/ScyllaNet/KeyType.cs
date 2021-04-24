// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    /// Describes the type of key.
    /// <para>
    /// This enum is going to be deprecated in future releases, use
    /// <see cref="DataCollectionMetadata.PartitionKeys"/>, <see cref="DataCollectionMetadata.ClusteringKeys"/>
    /// and <see cref="TableMetadata.Indexes"/> for a more accurate representation of a table or view keys and
    /// indexes.
    /// </para>
    /// </summary>
    public enum KeyType
    {
        None = 0,
        Partition = 1,
        Clustering = 2,
        SecondaryIndex = 3
    }
}
