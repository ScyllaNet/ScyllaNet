// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Responses
{
    internal class ReadyResponse : Response
    {
        public const byte OpCode = 0x02;

        internal ReadyResponse(Frame frame)
            : base(frame)
        {
        }

        internal static ReadyResponse Create(Frame frame)
        {
            return new ReadyResponse(frame);
        }
    }
}
