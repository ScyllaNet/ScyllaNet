// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.Mapping;

namespace Scylla.Net
{
    /// <summary>
    /// Utility builder that is used to add Execution Profiles to a Cluster instance.
    /// See <see cref="Builder.WithExecutionProfiles"/>.
    /// </summary>
    public interface IExecutionProfileOptions
    {
        /// <summary>
        /// <para>
        /// Adds an execution profile to this ExecutionProfileOptions instance. The name
        /// that is provided here is the name that must be provided to the several driver
        /// APIs that support execution profiles like <see cref="ISession.ExecuteAsync(IStatement, string)"/>
        /// or <see cref="Cql.WithExecutionProfile(string)"/>.
        /// </para>
        /// </summary>
        /// <param name="name">Name of the execution profile.</param>
        /// <param name="profileBuildAction">Execution Profile builder.</param>
        /// <returns></returns>
        IExecutionProfileOptions WithProfile(string name, Action<IExecutionProfileBuilder> profileBuildAction);

        /// <summary>
        /// <para>
        /// The behavior of this method is the same as <see cref="WithProfile(string, Action&lt;IExecutionProfileBuilder&gt;)"/> but
        /// instead of adding a normal execution profile, this method can be used to add a derived execution profile
        /// which is a profile that will inherit any unset settings from the base execution profile specified by <paramref name="baseProfile"/>.
        /// </para>
        /// </summary>
        /// <param name="name">Name of the execution profile.</param>
        /// <param name="baseProfile">Base Execution Profile's name from which the derived profile will inherit unset settings.</param>
        /// <param name="profileBuildAction">Execution Profile builder.</param>
        /// <returns></returns>
        IExecutionProfileOptions WithDerivedProfile(string name, string baseProfile, Action<IExecutionProfileBuilder> profileBuildAction);
    }
}
