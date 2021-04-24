// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.Mapping.Utils
{
    internal class CqlIdentifierHelper : ICqlIdentifierHelper
    {
        public string EscapeIdentifierIfNecessary(IPocoData pocoData, string identifier)
        {
            if (!pocoData.CaseSensitive
                && !string.IsNullOrWhiteSpace(identifier)
                && !CqlIdentifierHelper.ReservedKeywords.Contains(identifier.ToLowerInvariant()))
            {
                return identifier;
            }

            return "\"" + identifier + "\"";
        }
        
        public string EscapeTableNameIfNecessary(IPocoData pocoData, string keyspace, string table)
        {
            string name = null;
            if (keyspace != null)
            {
                name = EscapeIdentifierIfNecessary(pocoData, keyspace) + ".";
            }

            name += EscapeIdentifierIfNecessary(pocoData, table);
            return name;
        }
        
        private static readonly HashSet<string> ReservedKeywords = new HashSet<string>
        {
            // See https://github.com/apache/cassandra/blob/trunk/doc/cql3/CQL.textile#appendixA
            "add",
            "allow",
            "alter",
            "and",
            "apply",
            "asc",
            "authorize",
            "batch",
            "begin",
            "by",
            "columnfamily",
            "create",
            "default",
            "delete",
            "desc",
            "describe",
            "drop",
            "entries",
            "execute",
            "from",
            "full",
            "grant",
            "if",
            "in",
            "index",
            "infinity",
            "insert",
            "into",
            "is",
            "keyspace",
            "limit",
            "materialized",
            "mbean",
            "mbeans",
            "modify",
            "nan",
            "norecursive",
            "not",
            "null",
            "of",
            "on",
            "or",
            "order",
            "primary",
            "rename",
            "replace",
            "revoke",
            "schema",
            "select",
            "set",
            "table",
            "to",
            "token",
            "truncate",
            "unlogged",
            "unset",
            "update",
            "use",
            "using",
            "view",
            "where",
            "with"
        };
    }
}
