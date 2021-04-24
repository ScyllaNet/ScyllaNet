// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Net;
using System.Threading.Tasks;

namespace Scylla.Net.Connections
{
    /// <summary>
    /// Abstraction for DNS resolution. Useful for tests.
    /// </summary>
    internal interface IDnsResolver
    {
        Task<IPHostEntry> GetHostEntryAsync(string name);
    }
}
