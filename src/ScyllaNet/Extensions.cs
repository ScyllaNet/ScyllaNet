// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.SessionManagement;

namespace Scylla.Net
{
    /// <summary>
    /// Provides C# extension methods for interfaces and classes within the root namespace.
    /// <remarks>
    /// Used to introduce new methods on interfaces without making it a breaking change for the users.
    /// </remarks>
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Gets a snapshot containing information on the connections pools held by this Client at the current time.
        /// <para>
        /// The information provided in the returned object only represents the state at the moment this method was
        /// called and it's not maintained in sync with the driver metadata.
        /// </para>
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ISessionState GetState(this ISession instance)
        {
            var session = instance as IInternalSession;
            return session == null ? SessionState.Empty() : SessionState.From(session);
        }

        internal static ISessionState GetState(this IInternalSession instance)
        {
            return SessionState.From(instance);
        }
    }
}
