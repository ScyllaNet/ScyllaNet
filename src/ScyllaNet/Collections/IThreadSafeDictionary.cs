// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Scylla.Net.Collections
{
    internal interface IThreadSafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        TValue GetOrAdd(TKey key, TValue value);

        bool TryRemove(TKey key, out TValue value);

        TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory);

        TValue AddOrUpdate(
            TKey key,
            Func<TKey, TValue> addValueFactory,
            Func<TKey, TValue, TValue> updateValueFactory);

        /// <summary>
        /// Calls <paramref name="updateValueFactory"/> and updates an existing key
        /// only if <paramref name="compareFunc"/> returns true.
        /// </summary>
        /// <param name="key">Key used to fetch/update values in the dictionary.</param>
        /// <param name="compareFunc">Parameters are the key and the existing value. Returned bool determines
        /// whether <paramref name="updateValueFactory"/> is called and the map is updated.</param>
        /// <param name="updateValueFactory">Factory that will be invoked to modify the existing value.
        /// The existing value will be replaced with the output from this factory.</param>
        TValue CompareAndUpdate(
            TKey key,
            Func<TKey, TValue, bool> compareFunc,
            Func<TKey, TValue, TValue> updateValueFactory);
    }
}
