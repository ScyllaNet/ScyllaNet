// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    internal class Int64Converter : NumberConverter
    {
        protected override string GraphSONTypeName => "Int64";
        protected override Type HandledType => typeof(long);
    }
}
