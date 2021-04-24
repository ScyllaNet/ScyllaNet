// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Scylla.Net.DataStax.Graph
{
#pragma warning disable 1591

    public class TEnum : EnumWrapper
    {
        private TEnum(string enumValue)
            : base("T", enumValue)
        {
        }

        public static TEnum Id => new TEnum("id");

        public static TEnum Key => new TEnum("key");

        public static TEnum Label => new TEnum("label");

        public static TEnum Value => new TEnum("value");

        private static readonly IDictionary<string, TEnum> Properties = new Dictionary<string, TEnum>
        {
            { "id", TEnum.Id },
            { "key", TEnum.Key },
            { "label", TEnum.Label },
            { "value", TEnum.Value },
        };

        /// <summary>
        /// Gets the T enumeration by value.
        /// </summary>
        public static TEnum GetByValue(string value)
        {
            if (!TEnum.Properties.TryGetValue(value, out var property))
            {
                throw new ArgumentException($"No matching T for value '{value}'");
            }
            return property;
        }
    }


#pragma warning restore 1591
}
