// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.IO;

namespace Scylla.Net
{
    /// <summary>
    /// Defines the methods for frame compression and decompression
    /// </summary>
    public interface IFrameCompressor
    {
        /// <summary>
        /// Creates and returns stream (clear text) using the provided compressed <c>stream</c> as input.
        /// </summary>
        Stream Decompress(Stream stream);
    }
}
