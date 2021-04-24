// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    public class SchemaChangedEventArgs : EventArgs
    {
        public enum Kind
        {
            Created,
            Dropped,
            Updated
        }

        public string Keyspace;
        public string Table;
        public Kind What;
    }
}
