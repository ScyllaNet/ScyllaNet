// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    internal class SchemaChangeEventArgs : CassandraEventArgs
    {
        public enum Reason
        {
            Created,
            Updated,
            Dropped
        };

        /// <summary>
        /// The keyspace affected
        /// </summary>
        public string Keyspace { get; set; }

        /// <summary>
        /// The table affected
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// The type of change in the schema object
        /// </summary>
        public Reason What { get; set; }

        /// <summary>
        /// The custom type affected
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Name of the Cql function affected
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// Name of the aggregate affected
        /// </summary>
        public string AggregateName { get; set; }

        /// <summary>
        /// Signature of the function or aggregate
        /// </summary>
        public string[] Signature { get; set; }
    }
}
