// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Requests
{
    internal class AuthResponseRequest : BaseRequest
    {
        public const byte AuthResponseOpCode = 0x0F;

        private readonly byte[] _token;

        public AuthResponseRequest(byte[] token) : base(false, null)
        {
            _token = token;
        }

        protected override byte OpCode => AuthResponseRequest.AuthResponseOpCode;

        /// <inheritdoc />
        public override ResultMetadata ResultMetadata => null;

        protected override void WriteBody(FrameWriter wb)
        {
            wb.WriteBytes(_token);
        }
    }
}
