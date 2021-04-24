// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using Scylla.Net.DataStax.Graph.Internal;

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// This statement is used by the C# Graph extension. It should not be used directly by users as it requires
    /// specific types, serializers and deserializers that the C# Graph Extension provides automatically.
    /// </summary>
    public class FluentGraphStatement : GraphStatement
    {
        private FluentGraphStatement(
            object queryBytecode, 
            IReadOnlyDictionary<GraphProtocol, IReadOnlyDictionary<Type, IGraphSONSerializer>> customSerializers, 
            IReadOnlyDictionary<GraphProtocol, IReadOnlyDictionary<string, IGraphSONDeserializer>> customDeserializers,
            bool deserializeGraphNodes)
        {
            DeserializeGraphNodes = deserializeGraphNodes;
            QueryBytecode = queryBytecode;
            CustomSerializers = customSerializers;
            CustomDeserializers = customDeserializers;
        }
        
        public FluentGraphStatement(
            object queryBytecode, 
            IReadOnlyDictionary<GraphProtocol, IReadOnlyDictionary<Type, IGraphSONSerializer>> customSerializers, 
            IReadOnlyDictionary<GraphProtocol, IReadOnlyDictionary<string, IGraphSONDeserializer>> customDeserializers)
            : this(queryBytecode, customSerializers, customDeserializers, false)
        {
        }
        
        public FluentGraphStatement(
            object queryBytecode,
            IReadOnlyDictionary<GraphProtocol, IReadOnlyDictionary<Type, IGraphSONSerializer>> customSerializers) 
            : this(queryBytecode, customSerializers, null, true)
        {
        }

        /// <summary>
        /// Bytecode of the query represented by this statement.
        /// </summary>
        public object QueryBytecode { get; }
        
        internal bool DeserializeGraphNodes { get; }

        internal IReadOnlyDictionary<GraphProtocol, IReadOnlyDictionary<Type, IGraphSONSerializer>> CustomSerializers { get; }
        
        internal IReadOnlyDictionary<GraphProtocol, IReadOnlyDictionary<string, IGraphSONDeserializer>> CustomDeserializers { get; }

        internal override IStatement GetIStatement(GraphOptions options)
        {
            return null;
        }
    }
}
