// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Scylla.Net.Mapping.Utils
{
    /// <summary>
    /// Extension methods to IEnumerable&lt;T&gt;.
    /// </summary>
    internal static class EnumerableExtensions
    {
        public static LookupKeyedCollection<TKey, TValue> ToLookupKeyedCollection<TKey, TValue>(this IEnumerable<TValue> values,
                                                                                        Func<TValue, TKey> keySelector)
        {
            var keyedCollection = new LookupKeyedCollection<TKey, TValue>(keySelector);
            foreach(TValue value in values)
                keyedCollection.Add(value);

            return keyedCollection;
        }

        public static LookupKeyedCollection<TKey, TValue> ToLookupKeyedCollection<TKey, TValue>(this IEnumerable<TValue> values,
                                                                                        Func<TValue, TKey> keySelector,
                                                                                        IEqualityComparer<TKey> keyComparer)
        {
            var keyedCollection = new LookupKeyedCollection<TKey, TValue>(keySelector, keyComparer);
            foreach (TValue value in values)
                keyedCollection.Add(value);

            return keyedCollection;
        }

        /// <summary>
        /// Converts an IEnumerable&lt;string&gt; to a comma-delimited string.
        /// </summary>
        public static string ToCommaDelimitedString(this IEnumerable<string> values)
        {
            return string.Join(", ", values);
        }
    }
}
