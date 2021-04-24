// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    internal class OutputPrepared : IOutput
    {
        public RowSetMetadata VariablesRowsMetadata { get; }

        public RowSetMetadata ResultRowsMetadata { get; }

        public byte[] QueryId { get; }

        public byte[] ResultMetadataId { get; }

        public System.Guid? TraceId { get; internal set; }

        internal OutputPrepared(ProtocolVersion protocolVersion, FrameReader reader)
        {
            QueryId = reader.ReadShortBytes();

            if (protocolVersion.SupportsResultMetadataId())
            {
                ResultMetadataId = reader.ReadShortBytes();
            }

            VariablesRowsMetadata = new RowSetMetadata(reader, protocolVersion.SupportsPreparedPartitionKey());
            ResultRowsMetadata = new RowSetMetadata(reader, false);
        }
        
        // for testing
        internal OutputPrepared(byte[] queryId, RowSetMetadata rowSetVariablesRowsMetadata, RowSetMetadata resultRowsMetadata)
        {
            QueryId = queryId;
            VariablesRowsMetadata = rowSetVariablesRowsMetadata;
            ResultRowsMetadata = resultRowsMetadata;
        }

        public void Dispose()
        {
        }
    }
}
