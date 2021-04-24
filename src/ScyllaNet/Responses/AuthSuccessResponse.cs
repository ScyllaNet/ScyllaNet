// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Responses
{
    internal class AuthSuccessResponse : Response
    {
        public const byte OpCode = 0x10;

        public byte[] Token;

        internal AuthSuccessResponse(Frame frame)
            : base(frame)
        {
            Token = Reader.ReadBytes();
        }

        internal static AuthSuccessResponse Create(Frame frame)
        {
            return new AuthSuccessResponse(frame);
        }
    }
}
