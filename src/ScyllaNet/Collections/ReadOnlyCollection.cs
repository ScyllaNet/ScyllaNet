// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Scylla.Net.Collections
{
    /// <summary>
    /// Represents a wrapper around a collection to make it readonly.
    /// </summary>
    internal class ReadOnlyCollection<T> : IReadOnlyCollection<T>
    {
        private readonly ICollection<T> _items;

        internal ReadOnlyCollection(ICollection<T> items)
        {
            _items = items ?? throw new ArgumentNullException(nameof(items));
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _items.Count;
    }
}
