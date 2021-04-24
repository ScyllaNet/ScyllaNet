// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scylla.Net.Connections.Control
{
    internal interface IContactPoint : IEquatable<IContactPoint>
    {
        bool CanBeResolved { get; }

        string StringRepresentation { get; }

        Task<IEnumerable<IConnectionEndPoint>> GetConnectionEndPointsAsync(bool refreshCache);
    }
}
