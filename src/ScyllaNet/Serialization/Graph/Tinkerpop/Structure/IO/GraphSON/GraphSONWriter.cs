// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.GraphSON3.Tinkerpop;

using Newtonsoft.Json;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    /// <summary>
    ///     Allows to serialize objects to GraphSON.
    /// </summary>
    internal abstract class GraphSONWriter : IGraphSONWriter
    {
        /// <summary>
        /// Contains the information of serializers by type.
        /// </summary>
        protected readonly Dictionary<Type, IGraphSONSerializer> Serializers = new Dictionary
            <Type, IGraphSONSerializer>
            {
                //{typeof(ITraversal), new TraversalSerializer()},
                //{typeof(Bytecode), new BytecodeSerializer()},
                //{typeof(Binding), new BindingSerializer()},
                //{typeof(RequestMessage), new RequestMessageSerializer()},
                {typeof(int), new Int32Converter()},
                {typeof(long), new Int64Converter()},
                {typeof(float), new FloatConverter()},
                {typeof(double), new DoubleConverter()},
                {typeof(Guid), new UuidSerializer()},
                {typeof(Type), new ClassSerializer()},
                {typeof(EnumWrapper), new EnumSerializer()},
                //{typeof(P), new PSerializer()},
                //{typeof(TextP), new TextPSerializer()},
                //{typeof(Vertex), new VertexSerializer()},
                //{typeof(Edge), new EdgeSerializer()},
                //{typeof(Property), new PropertySerializer()},
                //{typeof(VertexProperty), new VertexPropertySerializer()},
                //{typeof(AbstractTraversalStrategy), new TraversalStrategySerializer()},
                //{typeof(ILambda), new LambdaSerializer()},

                //Extended
                {typeof(decimal), new DecimalConverter()},
                {typeof(TimeSpan), new DurationSerializer()},
                {typeof(BigInteger), new BigIntegerSerializer()},
                {typeof(byte), new ByteConverter()},
                {typeof(byte[]), new ByteBufferSerializer()},
                {typeof(char), new CharConverter() },
                {typeof(short), new Int16Converter() }
            };

        /// <summary>
        ///     Initializes a new instance of the <see cref="GraphSONWriter" /> class.
        /// </summary>
        protected GraphSONWriter()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GraphSONWriter" /> class.
        /// </summary>
        /// <param name="customSerializerByType">
        ///     <see cref="IGraphSONSerializer" /> serializers identified by their
        ///     <see cref="Type" />.
        /// </param>
        protected GraphSONWriter(IReadOnlyDictionary<Type, IGraphSONSerializer> customSerializerByType)
        {
            foreach (var serializerAndType in customSerializerByType)
            {
                Serializers[serializerAndType.Key] = serializerAndType.Value;
            }
        }

        /// <summary>
        ///     Serializes an object to GraphSON.
        /// </summary>
        /// <param name="objectData">The object to serialize.</param>
        /// <returns>The serialized GraphSON.</returns>
        public virtual string WriteObject(dynamic objectData)
        {
            return JsonConvert.SerializeObject(ToDict(objectData));
        }

        /// <summary>
        ///     Transforms an object into its GraphSON representation including type information.
        /// </summary>
        /// <param name="objectData">The object to transform.</param>
        /// <returns>A GraphSON representation of the object ready to be serialized.</returns>
        public virtual dynamic ToDict(dynamic objectData)
        {
            var type = objectData.GetType();

            IGraphSONSerializer serializer = TryGetSerializerFor(type);

            if (serializer != null)
            {
                return serializer.Dictify(objectData, this);
            }

            if (IsDictionaryType(type))
            {
                return DictToGraphSONDict(objectData);
            }

            if (IsCollectionType(type))
            {
                return CollectionToGraphSONCollection(objectData);
            }

            return objectData;
        }

        private IGraphSONSerializer TryGetSerializerFor(Type type)
        {
            if (Serializers.ContainsKey(type))
            {
                return Serializers[type];
            }
            foreach (var supportedType in Serializers.Keys)
            {
                if (supportedType.IsAssignableFrom(type))
                {
                    return Serializers[supportedType];
                }
            }

            return null;
        }

        private bool IsDictionaryType(Type type)
        {
            return type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
        }

        private Dictionary<string, dynamic> DictToGraphSONDict(dynamic dict)
        {
            var graphSONDict = new Dictionary<string, dynamic>();
            foreach (var keyValue in dict)
            {
                graphSONDict.Add(ToDict(keyValue.Key), ToDict(keyValue.Value));
            }

            return graphSONDict;
        }

        private bool IsCollectionType(Type type)
        {
            return type.GetInterfaces().Contains(typeof(ICollection));
        }

        private IEnumerable<dynamic> CollectionToGraphSONCollection(dynamic collection)
        {
            foreach (var e in collection)
            {
                yield return ToDict(e);
            }
        }
    }
}
