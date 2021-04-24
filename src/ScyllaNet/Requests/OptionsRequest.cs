// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Requests
{
    internal class OptionsRequest : BaseRequest
    {
        public const byte OptionsOpCode = 0x05;

        public OptionsRequest() : base(false, null)
        {
        }

        protected override byte OpCode => OptionsRequest.OptionsOpCode;

        /// <inheritdoc />
        public override ResultMetadata ResultMetadata => null;

        protected override void WriteBody(FrameWriter wb)
        {
            // OPTIONS requests have a header only
        }
    }
}
