// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Data.Linq
{
    /// <summary>
    /// Specifies table information for a given class
    /// </summary>
    [Obsolete("Linq attributes are deprecated, use mapping attributes defined in Cassandra.Mapping.Attributes instead.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class TableAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the table name in Cassandra
        /// </summary>
        public string Name {get; set; }

        /// <summary>
        /// Determines if the table and column names are defined as case sensitive (default to true).
        /// </summary>
        public bool CaseSensitive { get; set; }

        /// <summary>
        /// Specifies table information for a given class
        /// </summary>
        public TableAttribute()
        {
            //Linq tables are case sensitive by default
            CaseSensitive = true;
        }

        /// <summary>
        /// Specifies table information for a given class
        /// </summary>
        /// <param name="name">Name of the table</param>
        /// <param name="caseSensitive">Determines if the table and column names are defined as case sensitive</param>
        public TableAttribute(string name, bool caseSensitive = true)
        {
            Name = name;
            CaseSensitive = caseSensitive;
        }
    }
}
