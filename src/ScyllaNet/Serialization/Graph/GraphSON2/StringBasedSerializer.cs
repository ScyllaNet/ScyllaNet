// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Graph.Internal;
using Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.GraphSON2
{
    internal abstract class StringBasedSerializer : IGraphSONSerializer, IGraphSONDeserializer
    {
        private readonly string _typeKey;
        private readonly string _prefix;

        protected StringBasedSerializer(string prefix, string typeKey)
        {
            _typeKey = typeKey;
            _prefix = prefix;
        }
        
        public Dictionary<string, dynamic> Dictify(dynamic objectData, IGraphSONWriter writer)
        {
            return GraphSONUtil.ToTypedValue(_typeKey, objectData == null ? null : ToString(objectData), _prefix);
        }

        public dynamic Objectify(JToken graphsonObject, IGraphSONReader reader)
        {
            var str = TokenToString(graphsonObject);
            if (str == null)
            {
                return null;
            }

            return FromString(str);
        }

        protected virtual string TokenToString(JToken token)
        {
            return token.ToObject<string>();
        }
        
        protected abstract string ToString(dynamic obj);

        protected abstract dynamic FromString(string str);
    }
}
