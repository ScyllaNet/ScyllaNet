// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Data.Common;

namespace Scylla.Net.Data
{
    /// <summary>
    /// Implementation of the <see cref="System.Data.IDbDataAdapter"/> interface. Provides
    /// strong typing, but inherit most of the functionality needed to fully implement a DataAdapter.
    /// </summary>
    /// <inheritdoc />
    public class CqlDataAdapter : DbDataAdapter
    {
    }
}
