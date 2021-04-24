// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    ///  Exception thrown when the coordinator knows there is not enough replica alive
    ///  to perform a query with the requested consistency level.
    /// </summary>
    public class UnavailableException : QueryExecutionException
    {
        /// <summary>
        ///  Gets the consistency level of the operation triggering this unavailable exception. 
        /// </summary>
        public ConsistencyLevel Consistency { get; private set; }

        /// <summary>
        /// Gets the number of replica acknowledgements/responses required to perform the operation (with its required consistency level). 
        /// </summary>
        public int RequiredReplicas { get; private set; }

        /// <summary>
        ///  Gets the number of replica that were known to be alive by the Cassandra coordinator node when it tried to execute the operation. 
        /// </summary>
        public int AliveReplicas { get; private set; }

        public UnavailableException(ConsistencyLevel consistency, int required, int alive) :
            base(
            string.Format("Not enough replicas available for query at consistency {0} ({1} required but only {2} alive)", consistency, required, alive)
            )
        {
            Consistency = consistency;
            RequiredReplicas = required;
            AliveReplicas = alive;
        }
    }
}
