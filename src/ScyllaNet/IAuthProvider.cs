// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Net;

namespace Scylla.Net
{
    /// <summary>
    /// Provides <see cref="IAuthenticator"/> instances for use when connecting to Cassandra nodes. See 
    /// <see cref="PlainTextAuthProvider"/> for an implementation which uses SASL PLAIN mechanism to authenticate using
    /// username/password strings.
    /// </summary>
    public interface IAuthProvider
    {
        /// <summary>
        /// The <see cref="IAuthenticator"/> to use when connecting to host.
        /// </summary>
        /// <param name="host">The Cassandra host to connect to. </param>
        /// <returns>The authentication instance to use.</returns>
        IAuthenticator NewAuthenticator(IPEndPoint host);
    }

    /// <summary>
    /// Represents a <see cref="IAuthProvider"/> that is dependant on the name provided by Cassandra.
    /// </summary>
    /// <exclude />
    public interface IAuthProviderNamed : IAuthProvider
    {
        /// <summary>
        /// Sets the authenticator name from Cassandra.
        /// <para>
        /// This method is guaranteed to be called before <see cref="IAuthProvider.NewAuthenticator"/>.
        /// </para>
        /// </summary>
        void SetName(string name);
    }
}
