// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Scylla.Net.Mapping.TypeConversion
{
    /// <summary>
    /// A class that maps strings to enum values.  Uses a cache internally to speed lookups.
    /// </summary>
    public static class EnumStringMapper<T>
    {
        private static readonly Dictionary<string, T> StringToEnumCache;

        static EnumStringMapper()
        {
            StringToEnumCache = Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(val => val.ToString());
        }

        /// <summary>
        /// Converts a string value to an enum of Type T.
        /// </summary>
        public static T MapStringToEnum(string value)
        {
            return StringToEnumCache[value];
        }
    }
}
