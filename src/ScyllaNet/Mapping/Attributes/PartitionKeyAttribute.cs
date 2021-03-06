// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Mapping.Attributes
{
    /// <summary>
    /// Indicates that the property or field is part of the Partition Key
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
    public class PartitionKeyAttribute : Attribute
    {
        /// <summary>
        /// The index of the key, relative to the other partition keys.
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Specify the primary key column names (in order) for the table.
        /// </summary>
        /// <param name="index">The index of the key, relative to the other partition keys.</param>
        public PartitionKeyAttribute(int index = 0)
        {
            Index = index;
        }
    }
}
