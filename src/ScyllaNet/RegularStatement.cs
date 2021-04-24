// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.Serialization;

namespace Scylla.Net
{
    /// <summary>
    ///  A non-prepared CQL statement.
    ///  This class represents a query string along with query options. This class can be extended but
    ///  <see cref="SimpleStatement"/> is provided to build a <see cref="IStatement"/>
    ///  directly from its query string.
    /// </summary>
    public abstract class RegularStatement : Statement
    {
        /// <summary>
        /// Names of the parameters
        /// </summary>
        internal IList<string> QueryValueNames { get; set; }
        /// <summary>
        ///  Gets the query string for this statement.
        /// </summary>
        public abstract string QueryString { get; }

        /// <summary>
        /// Gets or sets the serialized used
        /// </summary>
        internal ISerializer Serializer { get; set; }

        protected RegularStatement()
        {

        }

        public override string ToString()
        {
            return QueryString;
        }
    }
}
