// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.Responses;

namespace Scylla.Net
{
    internal class OutputSchemaChange : IOutput
    {
        public SchemaChangeEventArgs SchemaChangeEventArgs { get; }

        public Guid? TraceId { get; }

        internal OutputSchemaChange(ProtocolVersion protocolVersion, FrameReader reader, Guid? traceId)
        {
            TraceId = traceId;
            SchemaChangeEventArgs = EventResponse.ParseSchemaChangeBody(protocolVersion, reader);
        }

        public void Dispose()
        {
        }
    }
}
