// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    ///  A server timeout during a query. Such an exception is returned when the
    ///  query has been tried by a server coordinator but cannot be achieved with the requested
    ///  consistency level within the rpc timeout set at server level.
    /// </summary>
    public abstract class QueryTimeoutException : QueryExecutionException
    {
        /// <summary>
        ///  Gets the consistency level of the operation that time outed. 
        /// </summary>
        public ConsistencyLevel ConsistencyLevel { get; private set; }

        /// <summary>
        /// Gets the number of replica that had acknowledged/responded to the operation before it time outed. 
        /// </summary>
        public int ReceivedAcknowledgements { get; private set; }

        /// <summary>
        ///  Gets the minimum number of replica acknowledgements/responses that were required to fulfill the operation. 
        /// </summary>
        public int RequiredAcknowledgements { get; private set; }

        public QueryTimeoutException(string message, ConsistencyLevel consistencyLevel, int received, int required)
            : base(message)
        {
            ConsistencyLevel = consistencyLevel;
            ReceivedAcknowledgements = received;
            RequiredAcknowledgements = required;
        }
    }
}
