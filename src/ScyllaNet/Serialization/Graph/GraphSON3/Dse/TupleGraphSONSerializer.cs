// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON3.Dse
{
    /// <inheritdoc />
    internal class TupleGraphSONSerializer : IComplexTypeGraphSONSerializer
    {
        public bool TryDictify(
            dynamic objectData,
            IGraphSONWriter serializer,
            IGenericSerializer genericSerializer,
            out dynamic result)
        {
            if (objectData == null)
            {
                result = null;
                return false;
            }

            var tupleType = (Type)objectData.GetType();
            if (!Utils.IsTuple(tupleType))
            {
                result = null;
                return false;
            }

            var tupleTypeInfo = tupleType.GetTypeInfo();
            var subtypes = tupleTypeInfo.GetGenericArguments();
            var data = new List<object>();
            for (var i = 1; i <= subtypes.Length; i++)
            {
                var prop = tupleTypeInfo.GetProperty("Item" + i);
                if (prop != null)
                {
                    data.Add(prop.GetValue(objectData, null));
                }
            }

            var dict = new Dictionary<string, dynamic>
            {
                { "cqlType", "tuple" },
                { 
                    "definition", 
                    data.Select(elem => ComplexTypeDefinitionHelper.GetDefinitionByValue(genericSerializer, elem)) },
                { "value", data.Select(d => serializer.ToDict(d)) }
            };

            result = GraphSONUtil.ToTypedValue("Tuple", dict, "dse");
            return true;
        }
    }
}
