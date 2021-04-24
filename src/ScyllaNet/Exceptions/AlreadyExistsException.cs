﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    ///  Exception thrown when a query attemps to create a keyspace or table that
    ///  already exists.
    /// </summary>
    public class AlreadyExistsException : QueryValidationException
    {
        /// <summary>
        ///  Gets the name of keyspace that either already exists or is home to the table that
        ///  already exists.
        /// </summary>
        public string Keyspace { get; private set; }

        /// <summary>
        ///  If the failed creation was a table creation, gets the name of the table that already exists. 
        /// </summary>
        public string Table { get; private set; }

        /// <summary>
        ///  Gets whether the query yielding this exception was a table creation
        ///  attempt.
        /// </summary>
        public bool WasTableCreation
        {
            get { return !string.IsNullOrEmpty((Table)); }
        }

        public AlreadyExistsException(string keyspace, string table) :
            base(MakeMsg(keyspace, table))
        {
            Keyspace = string.IsNullOrEmpty(keyspace.Trim()) ? null : keyspace;
            Table = string.IsNullOrEmpty(table.Trim()) ? null : table;
        }

        private static string MakeMsg(string keyspace, string table)
        {
            if (string.IsNullOrEmpty(table))
            {
                return string.Format("Keyspace {0} already exists", keyspace);
            }

            return string.Format("Table {0}.{1} already exists", keyspace, table);
        }
    }
}
