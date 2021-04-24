// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    public class CqlColumn : ColumnDesc
    {
        /// <summary>
        /// Index of the column in the rowset
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// CLR Type of the column
        /// </summary>
        public Type Type { get; set; }
    }
}
