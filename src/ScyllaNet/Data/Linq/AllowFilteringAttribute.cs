// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Data.Linq
{
    /// <summary>
    /// The ALLOW FILTERING option allows to explicitly allow queries that require filtering. 
    /// Please note that a query using ALLOW FILTERING may thus have unpredictable performance (for the definition above), i.e. even a query that selects a handful of records may exhibit performance that depends on the total amount of data stored in the cluster.
    /// </summary>
    [Obsolete("Linq attributes are deprecated, use mapping attributes defined in Cassandra.Mapping.Attributes instead.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public class AllowFilteringAttribute : Attribute
    {
    }
}
