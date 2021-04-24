// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Threading.Tasks;

using Scylla.Net.SessionManagement;

namespace Scylla.Net.Requests
{
    internal interface IPrepareHandler
    {
        Task<PreparedStatement> Prepare(
            PrepareRequest request, IInternalSession session, IEnumerator<Host> queryPlan);
    }
}
