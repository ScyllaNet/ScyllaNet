// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Numerics;
using Scylla.Net.DataStax.Graph.Internal;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    internal class BigIntegerDeserializer : IGraphSONDeserializer
    {
        public dynamic Objectify(JToken graphsonObject, IGraphSONReader reader)
        {
            var bigInteger = graphsonObject.ToObject<string>();
            return BigInteger.Parse(bigInteger);
        }
    }
}
