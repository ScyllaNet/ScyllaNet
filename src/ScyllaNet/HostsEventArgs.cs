// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Net;

namespace Scylla.Net
{
    public class HostsEventArgs : EventArgs
    {
        public enum Kind
        {
            Up,
            Down
        }

        public IPEndPoint Address;
        public Kind What;
    }
}
