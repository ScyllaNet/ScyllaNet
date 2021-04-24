// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Scylla.Net.Mapping.Utils
{
    /// <summary>
    /// A concrete implementation of System.Collections.ObjectModel.KeyedCollection which acts as a List/Dictionary hybrid where
    /// the Dictionary key is embedded in the item and list order is preserved.
    /// </summary>
    internal class LookupKeyedCollection<TKey, TItem> : KeyedCollection<TKey, TItem>
    {
        private readonly Func<TItem, TKey> _getKeyFunc;

        public LookupKeyedCollection(Func<TItem, TKey> getKeyFunc)
        {
            _getKeyFunc = getKeyFunc;
        }

        public LookupKeyedCollection(Func<TItem, TKey> getKeyFunc, IEqualityComparer<TKey> keyComparer) 
            : base(keyComparer)
        {
            _getKeyFunc = getKeyFunc;
        }

        protected override TKey GetKeyForItem(TItem item)
        {
            return _getKeyFunc(item);
        }

        public bool TryGetItem(TKey key, out TItem item)
        {
            if (Dictionary == null)
            {
                item = default(TItem);
                return false;
            }

            return Dictionary.TryGetValue(key, out item);
        }
    }
}
