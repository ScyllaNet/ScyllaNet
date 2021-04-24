// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Helpers
{
    internal class DefaultRandom : IRandom
    {
        private readonly Random _random;

        public DefaultRandom()
        {
            _random = new Random();
        }

        public int Next()
        {
            return _random.Next();
        }
    }
}
