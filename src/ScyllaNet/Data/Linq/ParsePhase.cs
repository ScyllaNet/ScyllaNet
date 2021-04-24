// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Data.Linq
{
    /// <summary>
    /// Represents the different phases during the parsing a Linq expressions.
    /// </summary>
    internal enum ParsePhase
    {
        None,

        /// <summary>
        /// Select() method calls.
        /// </summary>
        Select,

        /// <summary>
        /// Where() method calls or LWT conditions.
        /// </summary>
        Condition,

        /// <summary>
        /// Lambda evaluation after Select()
        /// </summary>
        SelectBinding,

        /// <summary>
        /// Take() method calls.
        /// </summary>
        Take,
        
        /// <summary>
        /// OrderBy() method calls.
        /// </summary>
        OrderBy,

        /// <summary>
        /// OrderByDescending() method calls.
        /// </summary>
        OrderByDescending,
        
        /// <summary>
        /// GroupBy() method calls.
        /// </summary>
        GroupBy
    }
}
