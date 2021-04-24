// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Mapping.Attributes
{
    /// <summary>
    /// Indicates that the property or field is part of the Clustering Key
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
    public class ClusteringKeyAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the clustering order
        /// </summary>
        public SortOrder ClusteringSortOrder { get; set; }

        /// <summary>
        /// Index of the clustering key, relative to the other clustering keys
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Name of the column
        /// </summary>
        public string Name { get; set; }

        public ClusteringKeyAttribute(int index = 0)
        {
            Index = index;
        }

        public ClusteringKeyAttribute(int index, SortOrder order)
        {
            Index = index;
            ClusteringSortOrder = order;
        }
    }
}
