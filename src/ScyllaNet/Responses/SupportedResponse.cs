// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Responses
{
    internal class SupportedResponse : Response
    {
        public const byte OpCode = 0x06;
        public OutputOptions Output;

        internal SupportedResponse(Frame frame) : base(frame)
        {
            Output = new OutputOptions(Reader);
        }

        internal static SupportedResponse Create(Frame frame)
        {
            return new SupportedResponse(frame);
        }
    }
}
