// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    internal class DoubleConverter : NumberConverter
    {
        protected override string GraphSONTypeName => "Double";
        protected override Type HandledType => typeof(double);
    }
}
