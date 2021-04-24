// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Scylla.Net.DataStax.Graph
{
#pragma warning disable 1591

    public class Direction : EnumWrapper
    {
        private Direction(string enumValue)
            : base("Direction", enumValue)
        {
        }

        public static Direction Both => new Direction("BOTH");

        public static Direction In => new Direction("IN");

        public static Direction Out => new Direction("OUT");

        private static readonly IDictionary<string, Direction> Properties = new Dictionary<string, Direction>
        {
            { "BOTH", Direction.Both },
            { "IN", Direction.In },
            { "OUT", Direction.Out },
        };

        /// <summary>
        /// Gets the Direction enumeration by value.
        /// </summary>
        public static Direction GetByValue(string value)
        {
            if (!Direction.Properties.TryGetValue(value, out var property))
            {
                throw new ArgumentException($"No matching Direction for value '{value}'");
            }
            return property;
        }
    }


#pragma warning restore 1591
}
