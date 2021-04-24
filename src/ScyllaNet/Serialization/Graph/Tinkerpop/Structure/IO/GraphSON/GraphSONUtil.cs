// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using Scylla.Net.DataStax.Graph.Internal;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    /// <summary>
    ///     Provides helper methods for GraphSON serialization.
    /// </summary>
    internal static class GraphSONUtil
    {
        /// <summary>
        ///     Transforms a value intos its GraphSON representation including type information.
        /// </summary>
        /// <param name="typename">The name of the type.</param>
        /// <param name="value">The value to transform.</param>
        /// <param name="prefix">A namespace prefix for the typename.</param>
        /// <returns>The GraphSON representation including type information.</returns>
        public static Dictionary<string, dynamic> ToTypedValue(string typename, dynamic value, string prefix = "g")
        {
            var typedValue = new Dictionary<string, dynamic>
            {
                {GraphSONTokens.TypeKey, GraphSONUtil.FormatTypeName(prefix, typename)}
            };
            if (value != null)
            {
                typedValue[GraphSONTokens.ValueKey] = value;
            }

            return typedValue;
        }

        /// <summary>
        ///     Formats a type name with its prefix to a GraphSON TypeID.
        /// </summary>
        /// <param name="namespacePrefix">The namespace prefix (default is "g").</param>
        /// <param name="typeName">The name of the type.</param>
        /// <returns>The formatted TypeID.</returns>
        public static string FormatTypeName(string namespacePrefix, string typeName)
        {
            return $"{namespacePrefix}:{typeName}";
        }

        /// <summary>
        /// Converts a Collection to a representation of g:List or g:Set
        /// </summary>
        internal static Dictionary<string, dynamic> ToCollection(dynamic objectData, IGraphSONWriter writer,
                                                               string typename)
        {
            var collection = objectData as IEnumerable;
            if (collection == null)
            {
                throw new InvalidOperationException("Object must implement IEnumerable");
            }
            var result = new List<object>();
            foreach (var item in collection)
            {
                result.Add(writer.ToDict(item));
            }
            return GraphSONUtil.ToTypedValue(typename, result);
        }
    }
}
