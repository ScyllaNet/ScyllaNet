// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Responses
{
    internal class ErrorResponse : Response
    {
        public const byte OpCode = 0x00;
        public OutputError Output;

        internal ErrorResponse(Frame frame)
            : base(frame)
        {
            var errorCode = Reader.ReadInt32();
            var message = Reader.ReadString();
            Output = OutputError.CreateOutputError(errorCode, message, Reader);
        }

        internal static ErrorResponse Create(Frame frame)
        {
            return new ErrorResponse(frame);
        }
    }
}
