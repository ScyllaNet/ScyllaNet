// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Net;
using System.Threading.Tasks;

namespace Scylla.Net.Connections
{
    internal interface ISniEndPointResolver : IEndPointResolver, IEquatable<ISniEndPointResolver>
    {
        SniOptions SniOptions { get; }

        Task RefreshProxyResolutionAsync();

        Task<IPEndPoint> GetNextEndPointAsync(bool refreshCache);
    }
}
