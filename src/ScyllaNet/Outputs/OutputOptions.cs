// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net
{
    internal class OutputOptions : IOutput
    {
        private readonly Dictionary<string, string[]> _options;

        public System.Guid? TraceId { get; internal set; }

        public IDictionary<string, string[]> Options
        {
            get { return _options; }
        }

        internal OutputOptions(FrameReader reader)
        {
            _options = new Dictionary<string, string[]>();
            int n = reader.ReadUInt16();
            for (var i = 0; i < n; i++)
            {
                var k = reader.ReadString();
                var v = reader.ReadStringList();
                _options.Add(k, v);
            }
        }

        public void Dispose()
        {
        }
    }
}
