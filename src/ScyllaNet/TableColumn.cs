﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Scylla.Net
{
    /// <summary>
    /// Represents a table column information
    /// </summary>
    public class TableColumn : CqlColumn
    {
        /// <summary>
        /// Gets or sets the column key type.
        /// <para>
        /// This property is going to be deprecated in future releases, use 
        /// <see cref="DataCollectionMetadata.PartitionKeys"/>, <see cref="DataCollectionMetadata.ClusteringKeys"/>
        /// and <see cref="TableMetadata.Indexes"/> that provide a more accurate representation of a table or view keys
        /// and indexes.
        /// </para>
        /// </summary>
        public KeyType KeyType { get; set; }
        [Obsolete("The driver provides a new secondary index metadata API, IndexMetadata, that is returned as part of the TableMetadata.")]
        public string SecondaryIndexName { get; set; }
        [Obsolete("The driver provides a new secondary index metadata API, IndexMetadata, that is returned as part of the TableMetadata.")]
        public string SecondaryIndexType { get; set; }
        [Obsolete("The driver provides a new secondary index metadata API, IndexMetadata, that is returned as part of the TableMetadata.")]
        public IDictionary<string, string> SecondaryIndexOptions { get; set; }
    }
}
