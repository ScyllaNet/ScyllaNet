// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Mapping.Attributes
{
    /// <summary>
    /// Indicates that the property or field represents a column which value is frozen.
    /// Only valid for maps and lists.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class FrozenValueAttribute : Attribute
    {

    }
}
