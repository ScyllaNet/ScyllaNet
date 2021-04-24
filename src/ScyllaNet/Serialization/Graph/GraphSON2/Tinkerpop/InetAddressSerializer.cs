// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Net;

using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Tinkerpop
{
    internal class InetAddressSerializer : StringBasedSerializer
    {
        private const string Prefix = "gx";
        private const string TypeKey = "InetAddress";

        public InetAddressSerializer() : base(InetAddressSerializer.Prefix, InetAddressSerializer.TypeKey)
        {
        }

        public static string TypeName =>
            GraphSONUtil.FormatTypeName(InetAddressSerializer.Prefix, InetAddressSerializer.TypeKey);

        protected override string ToString(dynamic obj)
        {
            IPAddress addr = obj;
            return addr.ToString();
        }

        protected override dynamic FromString(string str)
        {
            return IPAddress.Parse(str);
        }
    }
}
