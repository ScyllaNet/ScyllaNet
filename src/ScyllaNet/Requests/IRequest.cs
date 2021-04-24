// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.IO;
using Scylla.Net.Serialization;

namespace Scylla.Net.Requests
{
    internal interface IRequest
    {
        bool TracingEnabled { get; }

        /// <summary>
        /// Gets or sets the custom payload to be set with this request
        /// </summary>
        IDictionary<string, byte[]> Payload { get; }

        /// <summary>
        /// Writes the frame for this request on the provided stream
        /// </summary>
        int WriteFrame(short streamId, MemoryStream stream, ISerializer connectionSerializer);

        /// <summary>
        /// Result Metadata to parse the response rows. Only EXECUTE requests set this value so it will be null
        /// for other types of requests.
        /// </summary>
        ResultMetadata ResultMetadata { get; }
    }
}
