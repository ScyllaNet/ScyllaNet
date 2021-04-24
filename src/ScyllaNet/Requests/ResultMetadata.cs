// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Requests
{
    internal class ResultMetadata
    {
        public ResultMetadata(byte[] resultMetadataId, RowSetMetadata rowSetMetadata)
        {
            ResultMetadataId = resultMetadataId;
            RowSetMetadata = rowSetMetadata;
        }

        public byte[] ResultMetadataId { get; }

        public RowSetMetadata RowSetMetadata { get; }

        public bool ContainsColumnDefinitions()
        {
            if (RowSetMetadata == null)
            {
                return false;
            }

            return RowSetMetadata.Columns != null && RowSetMetadata.Columns.Length > 0;
        }
    }
}
