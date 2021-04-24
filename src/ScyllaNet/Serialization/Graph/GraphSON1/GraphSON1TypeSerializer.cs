// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.DataStax.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.GraphSON1
{
    internal class GraphSON1TypeSerializer : IGraphTypeSerializer
    {
        private static readonly Func<Row, GraphNode> RowParser = 
            row => new GraphNode(new GraphSON1Node(row.GetValue<string>("gremlin"), false));

        public bool DefaultDeserializeGraphNodes => true;

        public GraphProtocol GraphProtocol => GraphProtocol.GraphSON1;

        /// <inheritdoc />
        public Func<Row, GraphNode> GetGraphRowParser()
        {
            return GraphSON1TypeSerializer.RowParser;
        }

        public object FromDb(JToken token, Type type)
        {
            throw new InvalidOperationException("Not supported.");
        }

        public object FromDb(JToken token, Type type, bool deserializeGraphNodes)
        {
            throw new InvalidOperationException("Not supported.");
        }

        public T FromDb<T>(JToken token)
        {
            throw new InvalidOperationException("Not supported.");
        }

        public string ToDb(object obj)
        {
            return JsonConvert.SerializeObject(obj, GraphSON1ContractResolver.Settings);
        }

        public bool ConvertFromDb(object obj, Type targetType, out dynamic result)
        {
            throw new InvalidOperationException("Not supported.");
        }
    }
}
