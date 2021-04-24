// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net;

namespace Scylla.Net.Requests
{
    internal class PrepareResult
    {
        public PreparedStatement PreparedStatement { get; set; }

        public IDictionary<IPEndPoint, Exception> TriedHosts { get; set; }

        public IPEndPoint HostAddress { get; set; }
    }
}
