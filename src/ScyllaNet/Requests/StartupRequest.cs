// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Scylla.Net.Requests
{
    internal class StartupRequest : BaseRequest
    {
        public const byte StartupOpCode = 0x01;
        private readonly IReadOnlyDictionary<string, string> _options;

        public StartupRequest(IReadOnlyDictionary<string, string> startupOptions) : base(false, null)
        {
            _options = startupOptions ?? throw new ArgumentNullException(nameof(startupOptions));
        }

        protected override byte OpCode => StartupRequest.StartupOpCode;

        /// <inheritdoc />
        public override ResultMetadata ResultMetadata => null;

        protected override void WriteBody(FrameWriter wb)
        {
            wb.WriteUInt16((ushort)_options.Count);
            foreach (var kv in _options)
            {
                wb.WriteString(kv.Key);
                wb.WriteString(kv.Value);
            }
        }
    }
}
