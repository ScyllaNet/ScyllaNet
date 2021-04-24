// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net
{
    /// <summary>
    /// Defines extension methods for graph protocol versions
    /// </summary>
    internal static class GraphProtocolExtensions
    {
        private static readonly IDictionary<GraphProtocol, string> EnumToNameMap =
            new Dictionary<GraphProtocol, string>
            {
                { GraphProtocol.GraphSON1, "graphson-1.0" },
                { GraphProtocol.GraphSON2, "graphson-2.0" },
                { GraphProtocol.GraphSON3, "graphson-3.0" },
            };

        public static string GetInternalRepresentation(this GraphProtocol? version)
        {
            return version == null ? "null" : version.Value.GetInternalRepresentation();
        }

        public static string GetInternalRepresentation(this GraphProtocol version)
        {
            return GraphProtocolExtensions.EnumToNameMap[version];
        }
    }
}
