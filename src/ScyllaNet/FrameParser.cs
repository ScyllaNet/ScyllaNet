// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using Scylla.Net.Requests;
using Scylla.Net.Responses;

namespace Scylla.Net
{
    /// <summary>
    /// Parses the frame into a response
    /// </summary>
    internal class FrameParser
    {
        /// <summary>
        /// A factory to get the response handlers 
        /// </summary>
        private static readonly Dictionary<byte, Func<Frame, Response>> _responseHandlerFactory = 
            new Dictionary<byte, Func<Frame, Response>>
        {
            {AuthenticateResponse.OpCode, AuthenticateResponse.Create},
            {ErrorResponse.OpCode, ErrorResponse.Create},
            {EventResponse.OpCode, EventResponse.Create},
            {ReadyResponse.OpCode, ReadyResponse.Create},
            {ResultResponse.OpCode, ResultResponse.Create},
            {SupportedResponse.OpCode, SupportedResponse.Create},
            {AuthSuccessResponse.OpCode, AuthSuccessResponse.Create},
            {AuthChallengeResponse.OpCode, AuthChallengeResponse.Create}
        };

        /// <summary>
        /// Parses the response frame
        /// </summary>
        public static Response Parse(Frame frame)
        {
            var opcode = frame.Header.Opcode;
            if (!_responseHandlerFactory.ContainsKey(opcode))
            {
                throw new DriverInternalError("Unknown Response Frame type " + opcode);
            }
            return _responseHandlerFactory[opcode](frame);
        }
    }
}
