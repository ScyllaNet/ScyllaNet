﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using Scylla.Net.DataStax.Graph.Internal;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    internal class ClassSerializer : IGraphSONSerializer
    {
        public Dictionary<string, dynamic> Dictify(dynamic objectData, IGraphSONWriter writer)
        {
            var type = (Type) objectData;
            return writer.ToDict(Activator.CreateInstance(type));
        }
    }
}
