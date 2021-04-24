// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    internal class FrameHeader
    {
        public const int MaxFrameSize = 256*1024*1024;

        /// <summary>
        /// Protocol version byte (in case of responses 0x81, 0x82, ... in case of requests 0x01, 0x02)
        /// </summary>
        private byte _versionByte;

        /// <summary>
        /// Returns the length of the frame body 
        /// </summary>
        public int BodyLength { get; private set; }

        /// <summary>
        /// Flags applying to this frame..
        /// </summary>
        public HeaderFlags Flags { get; set; }
        
        public byte Opcode { get; set; }
        
        public short StreamId { get; set; }

        /// <summary>
        /// Protocol version of the protocol (1, 2, 3)
        /// </summary>
        public ProtocolVersion Version
        {
            get
            {
                return (ProtocolVersion)(byte)(_versionByte & 0x7f);
            }
        }

        /// <summary>
        /// Determines if the response is valid by checking the version byte
        /// </summary>
        public bool IsValidResponse()
        {
            return _versionByte >> 7 == 1 && (_versionByte & 0x7f) > 0;
        }

        /// <summary>
        /// Parses the first 8 or 9 bytes and returns a FrameHeader
        /// </summary>
        public static FrameHeader ParseResponseHeader(ProtocolVersion version, byte[] buffer, int offset)
        {
            var header = new FrameHeader()
            {
                _versionByte = buffer[offset++],
                Flags = (HeaderFlags)buffer[offset++]
            };
            if (!version.Uses2BytesStreamIds())
            {
                //Stream id is a signed byte in v1 and v2 of the protocol
                header.StreamId =  (sbyte)buffer[offset++];
            }
            else
            {
                header.StreamId = BeConverter.ToInt16(buffer, offset);
                offset += 2;
            }
            header.Opcode = buffer[offset++];
            header.BodyLength = BeConverter.ToInt32(Utils.SliceBuffer(buffer, offset, 4));
            return header;
        }

        /// <summary>
        /// Parses the first 8 or 9 bytes from multiple buffers and returns a FrameHeader
        /// </summary>
        public static FrameHeader ParseResponseHeader(ProtocolVersion version, byte[] buffer1, byte[] buffer2)
        {
            var headerBuffer = new byte[!version.Uses2BytesStreamIds() ? 8 : 9];
            Buffer.BlockCopy(buffer1, 0, headerBuffer, 0, buffer1.Length);
            Buffer.BlockCopy(buffer2, 0, headerBuffer, buffer1.Length, headerBuffer.Length - buffer1.Length);
            return ParseResponseHeader(version, headerBuffer, 0);
        }

        /// <summary>
        /// Gets the protocol version based on the first byte of the header
        /// </summary>
        public static ProtocolVersion GetProtocolVersion(byte[] buffer)
        {
            return (ProtocolVersion)(buffer[0] & 0x7F);
        }
    }
}
