// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net;

using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.GraphSON2;
using Scylla.Net.Serialization.Graph.GraphSON2.Tinkerpop;
using Scylla.Net.Serialization.Graph.GraphSON3.Dse;
using Scylla.Net.Serialization.Graph.GraphSON3.Tinkerpop;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON3
{
    /// <inheritdoc />
    internal class CustomGraphSON3Writer : CustomGraphSON2Writer
    {
        private static readonly IDictionary<Type, IGraphSONSerializer> CustomGraphSON3SpecificSerializers =
            new Dictionary<Type, IGraphSONSerializer>
            {
                { typeof(IList<object>), new ListSerializer() },
                { typeof(List<object>), new ListSerializer() },
                { typeof(ISet<object>), new SetSerializer() },
                { typeof(HashSet<object>), new SetSerializer() },
                { typeof(IDictionary<object, object>), new MapSerializer() },
                { typeof(Dictionary<object, object>), new MapSerializer() },
                { typeof(IPAddress), new InetAddressSerializer() },
                { typeof(Duration), new Duration3Serializer() },
                { typeof(byte[]), new ByteBufferSerializer() },
            };

        private static Dictionary<Type, IGraphSONSerializer> DefaultSerializers { get; } =
            new EmptyGraphSON2Writer().GetSerializers();

        static CustomGraphSON3Writer()
        {
            CustomGraphSON2Writer.AddGraphSON2Serializers(CustomGraphSON3Writer.DefaultSerializers);
            CustomGraphSON3Writer.AddGraphSON3Serializers(CustomGraphSON3Writer.DefaultSerializers);
        }

        protected static void AddGraphSON3Serializers(IDictionary<Type, IGraphSONSerializer> dictionary)
        {
            foreach (var kv in CustomGraphSON3Writer.CustomGraphSON3SpecificSerializers)
            {
                dictionary[kv.Key] = kv.Value;
            }
        }

        public CustomGraphSON3Writer(
            IReadOnlyDictionary<Type, IGraphSONSerializer> customSerializers,
            IGraphSONWriter writer) :
            base(CustomGraphSON3Writer.DefaultSerializers, customSerializers, writer)
        {
        }

        protected override dynamic DictToGraphSONDict(dynamic dict)
        {
            return Serializers[typeof(IDictionary<object, object>)].Dictify(dict, Writer);
        }

        protected override dynamic SetToGraphSONSet(dynamic collection)
        {
            return Serializers[typeof(ISet<object>)].Dictify(collection, Writer);
        }

        protected override dynamic ListToGraphSONList(dynamic collection)
        {
            return Serializers[typeof(IList<object>)].Dictify(collection, Writer);
        }
    }
}
