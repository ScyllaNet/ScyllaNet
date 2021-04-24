// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.IO;

namespace Scylla.Net.Compression
{
    internal class SnappyCompressor : IFrameCompressor
    {
        public Stream Decompress(Stream stream)
        {
            var buffer = Utils.ReadAllBytes(stream, 0);
            var outputBuffer = Snappy.SnappyDecompressor.Uncompress(buffer, 0, buffer.Length);
            return new MemoryStream(outputBuffer, 0, outputBuffer.Length, false, true);
        }
    }
}
