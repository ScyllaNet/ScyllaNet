// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Threading;

namespace Scylla.Net
{
    internal class BoolSwitch
    {
        private int _val = 0;

        public bool TryTake()
        {
            return Interlocked.Increment(ref _val) == 1;
        }

        public bool IsTaken()
        {
            return _val > 0;
        }
    }
}
