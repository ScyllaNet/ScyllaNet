// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    // We really only use the generic for type safety and it's not an interface because we don't want to expose
    // Note: we may want to expose this later if people use custom partitioner and want to be able to extend that. This is way premature however.
    internal abstract class TokenFactory
    {
        public static TokenFactory GetFactory(string partitionerName)
        {
            if (partitionerName.EndsWith("Murmur3Partitioner"))
            {
                return M3PToken.Factory;
            }

            if (partitionerName.EndsWith("RandomPartitioner"))
            {
                return RPToken.Factory;
            }

            if (partitionerName.EndsWith("OrderedPartitioner"))
            {
                return OPPToken.Factory;
            }

            return null;
        }

        public abstract IToken Parse(String tokenStr);
        public abstract IToken Hash(byte[] partitionKey);
    }

    // Murmur3Partitioner tokens

    // OPPartitioner tokens


    // RandomPartitioner tokens
}
