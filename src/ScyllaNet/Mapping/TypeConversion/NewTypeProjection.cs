// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Reflection;

namespace Scylla.Net.Mapping.TypeConversion
{
    /// <summary>
    /// Represents the components to build a expression to create a new instance.
    /// </summary>
    internal class NewTypeProjection
    {
        public ConstructorInfo ConstructorInfo { get; private set; }

        public ICollection<MemberInfo> Members { get; private set; }

        public NewTypeProjection(ConstructorInfo constructorInfo)
        {
            ConstructorInfo = constructorInfo;
            Members = new LinkedList<MemberInfo>();
        }
    }
}
