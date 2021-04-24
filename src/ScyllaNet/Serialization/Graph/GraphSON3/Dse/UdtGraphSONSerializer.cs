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
    internal class UdtGraphSONSerializer : IComplexTypeGraphSONSerializer
    {
        /// <inheritdoc />
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

            var type = (Type)objectData.GetType();
            var map = genericSerializer.GetUdtMapByType(type);

            if (map == null)
            {
                result = null;
                return false;
            }

            var dict = GetUdtTypeDefinition(map, genericSerializer);

            var value = (object)objectData;
            var values = new List<object>();

            foreach (var field in map.Definition.Fields)
            {
                object fieldValue = null;
                var prop = map.GetPropertyForUdtField(field.Name);
                var fieldTargetType = genericSerializer.GetClrTypeForGraph(field.TypeCode, field.TypeInfo);
                if (prop != null)
                {
                    fieldValue = prop.GetValue(value, null);
                    if (!fieldTargetType.GetTypeInfo().IsAssignableFrom(prop.PropertyType.GetTypeInfo()))
                    {
                        fieldValue = UdtMap.TypeConverter.ConvertToDbFromUdtFieldValue(prop.PropertyType,
                            fieldTargetType,
                            fieldValue);
                    }
                }

                values.Add(fieldValue);
            }

            dict.Add("value", values.Select(serializer.ToDict));
            result = GraphSONUtil.ToTypedValue("UDT", dict, "dse");
            return true;
        }

        private Dictionary<string, dynamic> GetUdtTypeDefinition(UdtMap map, IGenericSerializer genericSerializer)
        {
            return ComplexTypeDefinitionHelper.GetUdtTypeDefinition(
                new Dictionary<string, dynamic>(), map, genericSerializer);
        }
    }
}
