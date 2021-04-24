// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Scylla.Net.Serialization.Graph;
using Scylla.Net.Serialization.Graph.GraphSON1;
using Scylla.Net.Serialization.Graph.GraphSON2;
using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// Represents a graph query.
    /// </summary>
    public class SimpleGraphStatement : GraphStatement
    {
        /// <summary>
        /// The underlying query string
        /// </summary>
        public string Query { get; private set; }

        /// <summary>
        /// Values object used for parameter substitution in the query string
        /// </summary>
        public object Values { get; private set; }

        /// <summary>
        /// Values dictionary used for parameter substitution in the query string
        /// </summary>
        public IDictionary<string, object> ValuesDictionary { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="SimpleGraphStatement"/> using a query with no parameters.
        /// </summary>
        /// <param name="query">The graph query string.</param>
        public SimpleGraphStatement(string query) : this(query, null)
        {
            
        }

        /// <summary>
        /// Creates a new instance of <see cref="SimpleGraphStatement"/> using a query with named parameters.
        /// </summary>
        /// <param name="query">The graph query string.</param>
        /// <param name="values">An anonymous object containing the parameters as properties.</param>
        /// <example>
        /// <code>new SimpleGraphStatement(&quot;g.V().has('name', myName)&quot;, new { myName = &quot;mark&quot;})</code>
        /// </example>
        public SimpleGraphStatement(string query, object values)
        {
            Query = query ?? throw new ArgumentNullException("query");
            if (values != null && !IsAnonymous(values))
            {
                throw new ArgumentException("Expected anonymous object containing the parameters as properties", "values");
            }
            Values = values;
        }

        /// <summary>
        /// Creates a new instance of <see cref="SimpleGraphStatement"/> using a query with named parameters.
        /// </summary>
        /// <param name="values">An Dictionary object containing the parameters name and values as key and values.</param>
        /// <param name="query">The graph query string.</param>
        /// <example>
        /// <code>
        /// new SimpleGraphStatement(
        ///     new Dictionary&lt;string, object&gt;{ { &quot;myName&quot;, &quot;mark&quot; } }, 
        ///     &quot;g.V().has('name', myName)&quot;)
        /// </code>
        /// </example>
        public SimpleGraphStatement(IDictionary<string, object> values, string query)
        {
            Query = query ?? throw new ArgumentNullException("query");
            ValuesDictionary = values ?? throw new ArgumentNullException("values");
        }

        internal override IStatement GetIStatement(GraphOptions options)
        {
            return null;
        }
    }
}
