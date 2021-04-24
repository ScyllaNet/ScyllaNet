// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    ///  A server timeout during a read query.
    /// </summary>
    public class ReadTimeoutException : QueryTimeoutException
    {
        public bool WasDataRetrieved { get; private set; }

        public ReadTimeoutException(ConsistencyLevel consistency, int received, int required, bool dataPresent) :
            base("Server timeout during read query at consistency" +
                 $" {consistency} ({FormatDetails(received, required, dataPresent)})",
                 consistency,
                 received,
                 required)
        {
            WasDataRetrieved = dataPresent;
        }

        private static string FormatDetails(int received, int required, bool dataPresent)
        {
            if (received < required)
            {
                return $"{received} replica(s) responded over {required} required";
            }

            if (!dataPresent)
            {
                return "the replica queried for data didn't respond";
            }
            return "timeout while waiting for repair of inconsistent replica";
        }
    }
}
