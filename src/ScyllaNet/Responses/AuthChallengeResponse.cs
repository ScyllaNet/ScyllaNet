// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Responses
{
    internal class AuthChallengeResponse : Response
    {
        public const byte OpCode = 0x0E;

        public byte[] Token;

        internal AuthChallengeResponse(Frame frame)
            : base(frame)
        {
            Token = Reader.ReadBytes();
        }

        internal static AuthChallengeResponse Create(Frame frame)
        {
            return new AuthChallengeResponse(frame);
        }
    }
}
