// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Tinkerpop
{
    internal class LocalDateSerializer : StringBasedSerializer
    {
        private const string Prefix = "gx";
        private const string TypeKey = "LocalDate";

        public LocalDateSerializer() : base(LocalDateSerializer.Prefix, LocalDateSerializer.TypeKey)
        {
        }

        public static string TypeName =>
            GraphSONUtil.FormatTypeName(LocalDateSerializer.Prefix, LocalDateSerializer.TypeKey);

        protected override string ToString(dynamic obj)
        {
            LocalDate date = obj;
            return date.ToString();
        }

        protected override dynamic FromString(string str)
        {
            return LocalDate.Parse(str);
        }
    }
}
