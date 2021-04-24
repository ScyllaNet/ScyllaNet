// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Reflection;

namespace Scylla.Net.Mapping.Utils
{
    using System;
    using System.Linq;

    public static class MemberInfoExtensions
    {
        public static bool HasAttribute<TAttribute>(MemberInfo member, bool inherit)
            where TAttribute : Attribute
        {
            return member.GetCustomAttributes(typeof(TAttribute), inherit).Any();
        }

        public static TAttribute GetFirstAttribute<TAttribute>(MemberInfo member, bool inherit)
            where TAttribute : Attribute
        {
            return member.GetCustomAttributes(typeof(TAttribute), inherit).OfType<TAttribute>().FirstOrDefault();
        }
    }
}
