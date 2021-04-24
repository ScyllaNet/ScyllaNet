// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Tinkerpop
{
    internal class Duration2Serializer : StringBasedSerializer
    {
        private const string Prefix = "gx";
        private const string TypeKey = "Duration";

        public Duration2Serializer() : base(Duration2Serializer.Prefix, Duration2Serializer.TypeKey)
        {
        }

        public static string TypeName => GraphSONUtil.FormatTypeName(Duration2Serializer.Prefix, Duration2Serializer.TypeKey);

        protected override string ToString(dynamic obj)
        {
            Duration tinkerpopInstant = obj;
            return tinkerpopInstant.ToJavaDurationString();
        }

        protected override dynamic FromString(string str)
        {
            return Duration.Parse(str);
        }
    }
}
