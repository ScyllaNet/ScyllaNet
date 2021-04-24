// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Requests
{
    internal interface IRequestExecution
    {
        void Cancel();

        /// <summary>
        /// Starts a new execution using the current request. Note that an I/O task is scheduled here in a fire and forget manner.
        /// <para/>
        /// In some scenarios, some exceptions are thrown before the scheduling of any I/O task in order to fail fast.
        /// </summary>
        /// <param name="currentHostRetry">Whether this is a retry on the last queried host.
        /// Usually this is mapped from <see cref="RetryDecision.UseCurrentHost"/></param>
        /// <returns>Host chosen to which a connection will be obtained first.
        /// The actual host that will be queried might be different if a connection is not successfully obtained.
        /// In this scenario, the next host will be chosen according to the query plan.</returns>
        Host Start(bool currentHostRetry);
    }
}
