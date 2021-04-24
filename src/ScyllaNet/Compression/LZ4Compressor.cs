// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.IO;

using K4os.Compression.LZ4;

namespace Scylla.Net.Compression
{
    internal class LZ4Compressor : IFrameCompressor
    {
        public Stream Decompress(Stream stream)
        {
            var buffer = Utils.ReadAllBytes(stream, 0);
            if (buffer.Length < 4)
            {
                throw new DriverInternalError("Corrupt literal length");
            }

            var outputLengthBytes = new byte[4];
            Buffer.BlockCopy(buffer, 0, outputLengthBytes, 0, 4);
            Array.Reverse(outputLengthBytes);
            var outputLength = BitConverter.ToInt32(outputLengthBytes, 0);
            var outputBuffer = new byte[outputLength];
            if (outputLength != 0)
            {
                var uncompressedSize = LZ4Codec.Decode(buffer, 4, buffer.Length - 4, outputBuffer, 0, outputLength);
                if (uncompressedSize < 0)
                {
                    throw new DriverInternalError("LZ4 decoded buffer has a larger size than the expected uncompressed size");
                }

                if (outputLength != uncompressedSize)
                {
                    throw new DriverInternalError(string.Format("Recorded length is {0} bytes but actual length after decompression is {1} bytes ",
                        outputLength,
                        uncompressedSize));
                }
            }

            return new MemoryStream(outputBuffer, 0, outputLength, false, true);
        }
    }
}
