// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Scylla.Net.Collections
{
    internal class ByteArrayComparer : IEqualityComparer<byte[]>
    {
        public bool Equals(byte[] a, byte[] b)
        {
            if (a == null || b == null)
            {
                return a == b;
            }
            return a.SequenceEqual(b);
        }

        public int GetHashCode(byte[] key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            var hash = 0;
            var rest = key.Length % 4;
            for (var i = 0; i < key.Length - rest; i += 4)
            {
                // Use int32 values
                hash = Utils.CombineHashCode(
                    new[] { hash, BeConverter.ToInt32(new [] { key[i], key[i+1], key[i+2], key[i+3] }) });
            }
            if (rest > 0)
            {
                var arr = new byte[4];
                for (var i = 0; i < rest; i++)
                {
                    arr[i] = key[key.Length - rest + i];
                }
                hash = Utils.CombineHashCode(new[] { hash, BeConverter.ToInt32(arr) });
            }
            return hash;
        }
    }
}
