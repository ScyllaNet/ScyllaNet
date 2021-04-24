// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    [Flags]
    internal enum CassandraEventType
    {
        TopologyChange = 0x01,
        StatusChange = 0x02,
        SchemaChange = 0x03
    }
}
