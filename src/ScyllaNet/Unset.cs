// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    /// The Unset class represents a unspecified value. 
    /// <para>
    /// In Cassandra 2.2 and above, when executing a UPDATE or INSERT query, a parameter can be unset.
    /// Designed to avoid tombstones, setting a parameter value to Unset will make Cassandra to ignore it.
    /// </para>
    /// <remarks>
    /// In some cases, we might be inserting rows using null for values that are not specified, and even though our intention is to leave the value empty, Cassandra will represent it as a tombstone causing an unnecessary overhead. 
    /// To avoid tombstones, in previous versions of Cassandra, you can use different query combinations only containing the fields that have a value.
    /// <para>
    /// The Unset type is a singleton class, which means only one Unset object exists. The Unset.Value member represents the sole Unset object.
    /// </para>
    /// </remarks>
    /// </summary>
    public sealed class Unset
    {
        /// <summary>
        /// Represents the sole instance of the Unset class.
        /// </summary>
        public static readonly Unset Value = new Unset();

        private Unset() { }
    }
}
