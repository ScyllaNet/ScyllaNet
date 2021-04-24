// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Responses
{
    internal class AuthenticateResponse : Response
    {
        public const byte OpCode = 0x03;

        public string Authenticator;

        internal AuthenticateResponse(Frame frame) : base(frame)
        {
            Authenticator = Reader.ReadString();
        }

        internal static AuthenticateResponse Create(Frame frame)
        {
            return new AuthenticateResponse(frame);
        }
    }
}
