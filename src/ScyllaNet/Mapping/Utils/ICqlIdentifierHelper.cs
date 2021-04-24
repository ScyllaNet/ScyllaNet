// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Mapping.Utils
{
    /// <summary>
    /// Helper component that can be used to escape identifiers when they are reserved keywords in the CQL protocol.
    /// </summary>
    internal interface ICqlIdentifierHelper
    {
        string EscapeIdentifierIfNecessary(IPocoData pocoData, string identifier);
        
        string EscapeTableNameIfNecessary(IPocoData pocoData, string keyspace, string table);
    }
}
