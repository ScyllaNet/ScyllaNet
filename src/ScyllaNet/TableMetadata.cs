// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Scylla.Net
{
    /// <summary>
    /// Describes a Cassandra table
    /// </summary>
    public class TableMetadata: DataCollectionMetadata
    {
        private static readonly IDictionary<string, IndexMetadata> EmptyIndexes =
            new ReadOnlyDictionary<string, IndexMetadata>(new Dictionary<string, IndexMetadata>());

        /// <summary>
        /// Gets the table indexes by name
        /// </summary>
        public IDictionary<string, IndexMetadata> Indexes { get; protected set; }

        /// <summary>
        /// Determines whether the table is a virtual table or not.
        /// </summary>
        public bool IsVirtual { get; protected set; }

        protected TableMetadata()
        {
            
        }

        internal TableMetadata(string name, IDictionary<string, IndexMetadata> indexes, bool isVirtual = false)
        {
            Name = name;
            Indexes = indexes ?? EmptyIndexes;
            IsVirtual = isVirtual;
        }
    }
}
