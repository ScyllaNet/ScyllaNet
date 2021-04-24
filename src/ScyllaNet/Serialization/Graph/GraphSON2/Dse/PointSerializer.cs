// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Geometry;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Dse
{
    internal class PointSerializer : StringBasedSerializer
    {
        private const string Prefix = "dse";
        private const string TypeKey = "Point";

        public PointSerializer() : base(PointSerializer.Prefix, PointSerializer.TypeKey)
        {
        }

        public static string TypeName => GraphSONUtil.FormatTypeName(PointSerializer.Prefix, PointSerializer.TypeKey);

        protected override string ToString(dynamic obj)
        {
            Point pt = obj;
            return obj.ToString();
        }

        protected override dynamic FromString(string str)
        {
            return Point.Parse(str);
        }
    }
}
