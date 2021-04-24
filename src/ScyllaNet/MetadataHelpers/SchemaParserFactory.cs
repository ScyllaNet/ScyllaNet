// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Scylla.Net.MetadataHelpers
{
    internal class SchemaParserFactory : ISchemaParserFactory
    {
        private static readonly Version Version30 = new Version(3, 0);

        private static readonly Version Version40 = new Version(4, 0);

        /// <summary>
        /// Creates a new instance if the currentInstance is not valid for the given Cassandra version
        /// </summary>
        public ISchemaParser Create(Version cassandraVersion, Metadata parent,
                                               Func<string, string, Task<UdtColumnInfo>> udtResolver,
                                               ISchemaParser currentInstance = null)
        {
            if (cassandraVersion >= Version40 && !(currentInstance is SchemaParserV3))
            {
                return new SchemaParserV3(parent, udtResolver);
            }
            if (cassandraVersion >= Version30 && !(currentInstance is SchemaParserV2))
            {
                return new SchemaParserV2(parent, udtResolver);
            }
            if (cassandraVersion < Version30 && !(currentInstance is SchemaParserV1))
            {
                return new SchemaParserV1(parent);
            }
            if (currentInstance == null)
            {
                throw new ArgumentNullException(nameof(currentInstance));
            }
            return currentInstance;
        }
    }
}
