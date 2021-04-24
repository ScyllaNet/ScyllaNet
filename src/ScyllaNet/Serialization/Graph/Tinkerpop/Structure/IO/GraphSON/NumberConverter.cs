// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using Scylla.Net.DataStax.Graph.Internal;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    internal abstract class NumberConverter : IGraphSONDeserializer, IGraphSONSerializer
    {
        protected abstract string GraphSONTypeName { get; }
        protected abstract Type HandledType { get; }
        protected virtual string Prefix => "g";
        protected virtual bool StringifyValue => false;

        public dynamic Objectify(JToken graphsonObject, IGraphSONReader reader)
        {
            return graphsonObject.ToObject(HandledType);
        }

        public Dictionary<string, dynamic> Dictify(dynamic objectData, IGraphSONWriter writer)
        {
            object value = objectData;
            if (StringifyValue)
            {
                value = string.Format(CultureInfo.InvariantCulture, "{0}", value);
            }
            return GraphSONUtil.ToTypedValue(GraphSONTypeName, value, Prefix);
        }
    }
}
