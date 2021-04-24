// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections;
using System.Collections.Generic;

namespace Scylla.Net.Data.Linq
{
    /// <summary>
    /// Contains methods to use as Linq operators.
    /// </summary>
    public static class CqlOperator
    {
        /// <summary>
        /// Represents the CQL add assign (+=) operator for collections
        /// </summary>
        public static T Append<T>(T value) where T: IEnumerable
        {
            return default(T);   
        }

        /// <summary>
        /// Represents the CQL prepend operator for collections (col1 = ? + col1)
        /// </summary>
        public static T Prepend<T>(T value) where T: IEnumerable
        {
            return default(T);
        }

        /// <summary>
        /// Represents the CQL operator to remove an item from lists and sets (col1 = col1 - ?).
        /// </summary>
        public static T SubstractAssign<T>(T value) where T: IEnumerable
        {
            return default(T);
        }

        /// <summary>
        /// Represents the CQL operator to remove an item from a map (col1 = col1 - ?).
        /// </summary>
        public static Dictionary<TKey, TValue> SubstractAssign<TKey, TValue>(this Dictionary<TKey, TValue> map,
                                                                             params TKey[] value)
        {
            return null;
        }

        /// <summary>
        /// Represents the CQL operator to remove an item from a map (col1 = col1 - ?).
        /// </summary>
        public static IDictionary<TKey, TValue> SubstractAssign<TKey, TValue>(this IDictionary<TKey, TValue> map,
                                                                              params TKey[] value)
        {
            return null;
        }

        /// <summary>
        /// Represents the CQL operator to remove an item from a map (col1 = col1 - ?).
        /// </summary>
        public static SortedDictionary<TKey, TValue> SubstractAssign<TKey, TValue>(
            this SortedDictionary<TKey, TValue> map, params TKey[] value)
        {
            return null;
        }
    }
}
