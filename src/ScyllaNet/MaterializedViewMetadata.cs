// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    /// Describes a materialized view in Cassandra. 
    /// </summary>
    public class MaterializedViewMetadata : DataCollectionMetadata
    {
        /// <summary>
        /// Gets the view where clause
        /// </summary>
        public string WhereClause { get; protected set; }

        protected MaterializedViewMetadata()
        {
            
        }

        internal MaterializedViewMetadata(string name, string whereClause)
        {
            Name = name;
            WhereClause = whereClause;
        }
    }
}
