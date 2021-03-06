// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Geometry;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Dse
{
    internal class LineStringSerializer : StringBasedSerializer
    {
        private const string Prefix = "dse";
        private const string TypeKey = "LineString";

        public LineStringSerializer() : base(LineStringSerializer.Prefix, LineStringSerializer.TypeKey)
        {
        }

        public static string TypeName => GraphSONUtil.FormatTypeName(LineStringSerializer.Prefix, LineStringSerializer.TypeKey);

        protected override string ToString(dynamic obj)
        {
            LineString p = obj;
            return p.ToString();
        }

        protected override dynamic FromString(string str)
        {
            return LineString.Parse(str);
        }
    }
}
