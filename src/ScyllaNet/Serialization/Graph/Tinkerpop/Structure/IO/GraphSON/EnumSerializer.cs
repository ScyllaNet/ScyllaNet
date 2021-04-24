// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using Scylla.Net.DataStax.Graph;
using Scylla.Net.DataStax.Graph.Internal;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    internal class EnumSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, IGraphSONWriter writer)
        {
            EnumWrapper enumToSerialize = objectData;
            return GraphSONUtil.ToTypedValue(enumToSerialize.EnumName, enumToSerialize.EnumValue);
        }
    }
}
