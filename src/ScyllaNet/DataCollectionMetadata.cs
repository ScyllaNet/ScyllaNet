﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Scylla.Net
{
    /// <summary>
    /// Describes a table or materialized view in Cassandra
    /// </summary>
    public abstract class DataCollectionMetadata
    {
        /// <summary>
        /// Specifies sort order of the clustering keys
        /// </summary>
        public enum SortOrder : sbyte
        {
            Ascending = 1,
            Descending = -1
        }

        /// <summary>
        /// Gets the table name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the table columns
        /// </summary>
        public TableColumn[] TableColumns { get; protected set; }

        /// <summary>
        /// Gets a dictionary of columns by name
        /// </summary>
        public IDictionary<string, TableColumn> ColumnsByName { get; protected set; }

        /// <summary>
        /// Gets an array of columns that are part of the partition key in correct order
        /// </summary>
        public TableColumn[] PartitionKeys { get; protected set; }

        /// <summary>
        /// Gets an array of pairs of columns and sort order that are part of the clustering key
        /// </summary>
        public Tuple<TableColumn, SortOrder>[] ClusteringKeys { get; protected set; }

        /// <summary>
        /// Gets the table options
        /// </summary>
        public TableOptions Options { get; protected set; }

        protected DataCollectionMetadata()
        {
   
        }

        internal void SetValues(IDictionary<string, TableColumn> columns, TableColumn[] partitionKeys, Tuple<TableColumn, SortOrder>[] clusteringKeys, TableOptions options)
        {
            ColumnsByName = columns;
            TableColumns = columns.Values.ToArray();
            PartitionKeys = partitionKeys;
            ClusteringKeys = clusteringKeys;
            Options = options;
        }
    }
}
