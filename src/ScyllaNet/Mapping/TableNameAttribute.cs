// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.Mapping.Attributes;

namespace Scylla.Net.Mapping
{
    /// <summary>
    /// Deprecated (use <see cref="TableAttribute"/>)
    /// Used to specify the table a POCO maps to. Deprecated.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        private readonly string _tableName;

        /// <summary>
        /// The table name.
        /// </summary>
        public string Value
        {
            get { return _tableName; }
        }

        /// <summary>
        /// Specifies the table a POCO maps to.
        /// </summary>
        /// <param name="tableName">The name of the table to map this POCO to.</param>
        public TableNameAttribute(string tableName)
        {
            _tableName = tableName;
        }
    }
}
