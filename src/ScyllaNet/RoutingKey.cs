// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    /// <summary>
    /// Routing key using to determine the node for each partition
    /// </summary>
    public class RoutingKey
    {
        public static RoutingKey Empty = new RoutingKey();

        /// <summary>
        /// Byte array representing the partition key (or one of the partition)
        /// </summary>
        public byte[] RawRoutingKey { get; set; }

        public RoutingKey() { }

        public RoutingKey(byte[] rawKey)
        {
            RawRoutingKey = rawKey;
        }

        internal static RoutingKey Compose(params RoutingKey[] components)
        {
            if (components.Length == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (components.Length == 1)
            {
                return components[0];
            }

            var totalLength = 0;
            foreach (var bb in components)
            {
                totalLength += 2 + bb.RawRoutingKey.Length + 1;
            }

            var res = new byte[totalLength];
            var idx = 0;
            foreach (var bb in components)
            {
                PutShortLength(res, idx, bb.RawRoutingKey.Length);
                idx += 2;
                Buffer.BlockCopy(bb.RawRoutingKey, 0, res, idx, bb.RawRoutingKey.Length);
                idx += bb.RawRoutingKey.Length;
                res[idx] = 0;
                idx++;
            }
            return new RoutingKey {RawRoutingKey = res};
        }

        private static void PutShortLength(byte[] bb, int idx, int length)
        {
            bb[idx] = ((byte) ((length >> 8) & 0xFF));
            bb[idx + 1] = ((byte) (length & 0xFF));
        }
    }
}
