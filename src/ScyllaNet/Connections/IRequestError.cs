// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Connections
{
    /// <summary>
    /// Represents an error that occured during a request.
    /// </summary>
    internal interface IRequestError
    {
        /// <summary>
        /// Exception related to this error.
        /// </summary>
        Exception Exception { get; }

        /// <summary>
        /// Whether this error was parsed from a server response.
        /// </summary>
        bool IsServerError { get; }

        /// <summary>
        /// Whether the request was sent or not. This error might have happened before writing
        /// the request to the socket.
        /// </summary>
        bool Unsent { get; }
    }
}
