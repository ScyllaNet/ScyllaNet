// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.ProtocolEvents
{
    /// <summary>
    /// For unit testing purposes. Wrapper around <see cref="System.Threading.Timer"/>.
    /// </summary>
    internal interface ITimer : IDisposable
    {
        void Cancel();

        void Change(Action action, TimeSpan due);
    }
}
