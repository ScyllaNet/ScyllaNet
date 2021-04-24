// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.DataStax.Auth
{
    /// <summary>
    /// Base class for Authenticator implementations that want to make use of
    /// the authentication scheme negotiation in the DseAuthenticator
    /// </summary>
    internal abstract class BaseDseAuthenticator : IAuthenticator
    {
        private readonly string _name;
        private const string DseAuthenticatorName = "com.datastax.bdp.cassandra.auth.DseAuthenticator";

        protected BaseDseAuthenticator(string name)
        {
            _name = name;
        }

        protected abstract byte[] GetMechanism();

        protected abstract byte[] GetInitialServerChallenge();

        public virtual byte[] InitialResponse()
        {
            if (!IsDseAuthenticator())
            {
                //fallback
                return EvaluateChallenge(GetInitialServerChallenge());
            }
            //send the mechanism as a first auth message
            return GetMechanism();
        }

        public abstract byte[] EvaluateChallenge(byte[] challenge);

        protected bool IsDseAuthenticator()
        {
            return _name == BaseDseAuthenticator.DseAuthenticatorName;
        }
    }
}
