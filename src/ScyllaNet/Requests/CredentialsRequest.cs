// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Scylla.Net.Requests
{
    internal class CredentialsRequest : BaseRequest
    {
        public const byte CredentialsRequestOpCode = 0x04;

        private readonly IDictionary<string, string> _credentials;

        public CredentialsRequest(IDictionary<string, string> credentials) : base(false, null)
        {
            _credentials = credentials;
        }

        protected override byte OpCode => CredentialsRequest.CredentialsRequestOpCode;

        /// <inheritdoc />
        public override ResultMetadata ResultMetadata => null;

        protected override void WriteBody(FrameWriter wb)
        {
            if (wb.Serializer.ProtocolVersion != ProtocolVersion.V1)
            {
                throw new NotSupportedException("Credentials request is only supported in C* = 1.2.x");
            }

            wb.WriteUInt16((ushort)_credentials.Count);
            foreach (var kv in _credentials)
            {
                wb.WriteString(kv.Key);
                wb.WriteString(kv.Value);
            }
        }
    }
}
