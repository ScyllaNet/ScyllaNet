// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Mapping.Attributes
{
    /// <summary>
    /// Indicates that the property or field is Frozen.
    /// Only valid for collections, tuples, and user-defined types.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class FrozenAttribute : Attribute
    {

    }
}
