// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

// ReSharper disable once CheckNamespace
namespace Scylla.Net
{
    /// <summary>
    /// The policy that decides if the driver will send speculative queries to the next hosts when the current host takes too long to respond.
    /// only idempotent statements will be speculatively retried, see <see cref="IStatement.IsIdempotent"/> for more information.
    /// </summary>
    public interface ISpeculativeExecutionPolicy : IDisposable
    {
        /// <summary>
        /// Initializes the policy at cluster startup.
        /// </summary>
        void Initialize(ICluster cluster);

        /// <summary>
        /// Returns the plan to use for a new query.
        /// </summary>
        /// <param name="keyspace">the currently logged keyspace</param>
        /// <param name="statement">the query for which to build a plan.</param>
        /// <returns></returns>
        ISpeculativeExecutionPlan NewPlan(string keyspace, IStatement statement);
    }
}
