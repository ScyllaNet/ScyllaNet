// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.Responses;

namespace Scylla.Net.Connections
{
    internal class RequestError : IRequestError
    {
        private RequestError(Exception ex, bool isServerError, bool unsent)
        {
            Exception = ex;
            IsServerError = isServerError;
            Unsent = unsent;
        }
        
        /// <summary>
        /// Creates a server side request error based on a server error.
        /// </summary>
        public static IRequestError CreateServerError(ErrorResponse response)
        {
            return new RequestError(response.Output.CreateException(), true, false);
        }
        
        /// <summary>
        /// Creates a client side request error based on an exception.
        /// </summary>
        public static IRequestError CreateServerError(Exception ex)
        {
            return new RequestError(ex, true, false);
        }
        
        /// <summary>
        /// Creates a client side request error based on a exception.
        /// </summary>
        public static IRequestError CreateClientError(Exception ex, bool unsent)
        {
            return new RequestError(ex, false, unsent);
        }
        
        public Exception Exception { get; }

        public bool IsServerError { get; }

        public bool Unsent { get; }
    }
}
