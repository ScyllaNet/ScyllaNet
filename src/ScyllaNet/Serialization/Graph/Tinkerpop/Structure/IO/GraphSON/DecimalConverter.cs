// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Serialization.Graph.Tinkerpop.Structure.IO.GraphSON
{
    internal class DecimalConverter : NumberConverter
    {
        protected override string GraphSONTypeName => "BigDecimal";
        protected override Type HandledType => typeof(decimal);
        protected override string Prefix => "gx";
        protected override bool StringifyValue => true;
    }
}
