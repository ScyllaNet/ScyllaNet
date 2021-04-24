// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.GraphSON3.Tinkerpop
{
    internal class ByteBufferDeserializer : IGraphSONDeserializer
    {
        private const string Prefix = "gx";
        private const string TypeKey = "ByteBuffer";
        
        public static string TypeName =>
            GraphSONUtil.FormatTypeName(ByteBufferDeserializer.Prefix, ByteBufferDeserializer.TypeKey);

        public dynamic Objectify(JToken graphsonObject, IGraphSONReader reader)
        {
            var base64String = graphsonObject.ToObject<string>();
            return Convert.FromBase64String(base64String);
        }
    }
}
