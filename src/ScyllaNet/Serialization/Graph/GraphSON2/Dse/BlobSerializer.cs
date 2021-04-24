// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Dse
{
    internal class BlobSerializer : StringBasedSerializer
    {
        private const string Prefix = "dse";
        private const string TypeKey = "Blob";

        public BlobSerializer() : base(BlobSerializer.Prefix, BlobSerializer.TypeKey)
        {
        }

        public static string TypeName => GraphSONUtil.FormatTypeName(BlobSerializer.Prefix, BlobSerializer.TypeKey);

        protected override string ToString(dynamic obj)
        {
            byte[] buf = obj;
            return Convert.ToBase64String(buf);
        }

        protected override dynamic FromString(string str)
        {
            return Convert.FromBase64String(str);
        }
    }
}
