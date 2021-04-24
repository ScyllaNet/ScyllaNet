// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Scylla.Net.MetadataHelpers
{
    internal interface ISchemaParserFactory
    {
        ISchemaParser Create(Version cassandraVersion, Metadata parent,
                                 Func<string, string, Task<UdtColumnInfo>> udtResolver,
                                 ISchemaParser currentInstance = null);
    }
}
