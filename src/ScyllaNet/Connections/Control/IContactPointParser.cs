// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.Connections.Control
{
    internal interface IContactPointParser
    {
        IEnumerable<IContactPoint> ParseContactPoints(IEnumerable<object> providedContactPoints);
    }
}
