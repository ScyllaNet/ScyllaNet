// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Net;

namespace Scylla.Net
{
    /// <summary>
    ///  Authentication informations provider to connect to Cassandra nodes. <p> The
    ///  authentication information themselves are just a key-value pairs. Which exact
    ///  key-value pairs are required depends on the authenticator set for the
    ///  Cassandra nodes.</p>
    /// </summary>
    /// 
    internal interface IAuthInfoProvider
        // only for protocol V1 Credentials support
    {
        /// <summary>
        ///  The authentication informations to use to connect to <c>host</c>.
        ///  Please note that if authentication is required, this method will be called to
        ///  initialize each new connection created by the driver. It is thus a good idea
        ///  to make sure this method returns relatively quickly.
        /// </summary>
        /// <param name="host"> the Cassandra host for which authentication information
        ///  are requested. </param>
        /// 
        /// <returns>The authentication informations to use.</returns>
        IDictionary<string, string> GetAuthInfos(IPEndPoint host);
    }
}
