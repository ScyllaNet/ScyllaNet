// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Structure
{
    internal class PropertySerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, IGraphSONWriter writer)
        {
            if (objectData is IPropertyWithElement propertyWithElement)
            {
                var elementDict = CreateElementDict(propertyWithElement.Element, writer);
                var valueDict = new Dictionary<string, dynamic>
                {
                    {"key", propertyWithElement.Name},
                    {"value", writer.ToDict(propertyWithElement.Value)},
                    {"element", elementDict}
                };
                return GraphSONUtil.ToTypedValue(nameof(Property), valueDict);
            }
            else
            {
                IProperty property = objectData;
                var valueDict = new Dictionary<string, dynamic>
                {
                    {"key", property.Name},
                    {"value", writer.ToDict(property.Value)}
                };
                return GraphSONUtil.ToTypedValue(nameof(Property), valueDict);
            }
        }

        private dynamic CreateElementDict(IGraphNode graphNode, IGraphSONWriter writer)
        {
            if (graphNode == null)
            {
                return null;
            }
            
            var serializedElement = writer.ToDict(graphNode);
            Dictionary<string, dynamic> elementDict = serializedElement;
            if (elementDict.ContainsKey(GraphSONTokens.ValueKey))
            {
                var elementValueSerialized = elementDict[GraphSONTokens.ValueKey];
                Dictionary<string, dynamic> elementValueDict = elementValueSerialized;
                if (elementValueDict != null)
                {
                    elementValueDict.Remove("outVLabel");
                    elementValueDict.Remove("inVLabel");
                    elementValueDict.Remove("properties");
                    elementValueDict.Remove("value");
                }
            }
            return serializedElement;
        }
    }
}
