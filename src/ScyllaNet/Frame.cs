// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.IO;
using Scylla.Net.Requests;
using Scylla.Net.Serialization;

namespace Scylla.Net
{
    internal class Frame
    {
        /// <summary>
        /// The 8 byte protocol header
        /// </summary>
        public FrameHeader Header { get; }

        /// <summary>
        /// A stream containing the frame body
        /// </summary>
        public Stream Body { get; }

        /// <summary>
        /// Gets the serializer instance to be used for this frame
        /// </summary>
        public ISerializer Serializer { get; }

        /// <summary>
        /// Metadata to parse the result. Can be null.
        /// </summary>
        public ResultMetadata ResultMetadata { get; }

        public Frame(FrameHeader header, Stream body, ISerializer serializer, ResultMetadata resultMetadata)
        {
            Header = header ?? throw new ArgumentNullException(nameof(header));
            Body = body ?? throw new ArgumentNullException(nameof(body));
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            ResultMetadata = resultMetadata;
        }
    }
}
