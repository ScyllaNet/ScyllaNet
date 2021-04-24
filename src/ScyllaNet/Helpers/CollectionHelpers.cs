// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.Helpers
{
    internal static class CollectionHelpers
    {
        internal static void CreateOrAdd<TKey, TElement>(
            this IDictionary<TKey, ICollection<TElement>> dictionary, TKey key, TElement elementToAdd)
        {
            if (!dictionary.TryGetValue(key, out var collection))
            {
                collection = new List<TElement>();
                dictionary.Add(new KeyValuePair<TKey, ICollection<TElement>>(key, collection));
            }

            collection.Add(elementToAdd);
        }
        
        internal static void CreateIfDoesNotExist<TKey, TElement>(
            this IDictionary<TKey, ICollection<TElement>> dictionary, TKey key)
        {
            if (!dictionary.TryGetValue(key, out var collection))
            {
                collection = new List<TElement>();
                dictionary.Add(new KeyValuePair<TKey, ICollection<TElement>>(key, collection));
            }
        }
    }
}
