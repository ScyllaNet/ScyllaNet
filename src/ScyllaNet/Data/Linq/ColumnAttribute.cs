﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Data.Linq
{
    [Obsolete("Linq attributes are deprecated, use mapping attributes defined in Cassandra.Mapping.Attributes instead.")]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class ColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public ColumnAttribute()
        {
        }

        public ColumnAttribute(string name)
        {
            Name = name;
        }
    }
}