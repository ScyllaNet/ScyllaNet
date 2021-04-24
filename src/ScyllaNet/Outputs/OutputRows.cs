// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Threading;
using Scylla.Net.Requests;

// ReSharper disable CheckNamespace
namespace Scylla.Net
{
    internal class OutputRows : IOutput
    {
        private readonly int _rowLength;
        private const int ReusableBufferLength = 1024;
        private static readonly ThreadLocal<byte[]> ReusableBuffer = new ThreadLocal<byte[]>(() => new byte[ReusableBufferLength]);

        /// <summary>
        /// Gets or sets the RowSet parsed from the response
        /// </summary>
        public RowSet RowSet { get; set; }

        public Guid? TraceId { get; private set; }
        
        public RowSetMetadata ResultRowsMetadata { get; }

        internal OutputRows(FrameReader reader, ResultMetadata resultMetadata, Guid? traceId)
        {
            ResultRowsMetadata = new RowSetMetadata(reader);
            _rowLength = reader.ReadInt32();
            TraceId = traceId;
            RowSet = new RowSet();
            ProcessRows(RowSet, reader, resultMetadata);
        }

        /// <summary>
        /// Process rows and sets the paging event handler
        /// </summary>
        internal void ProcessRows(RowSet rs, FrameReader reader, ResultMetadata providedResultMetadata)
        {
            RowSetMetadata resultMetadata = null;

            // result metadata in the response takes precedence over the previously provided result metadata.
            if (ResultRowsMetadata != null)
            {
                resultMetadata = ResultRowsMetadata;
                rs.Columns = ResultRowsMetadata.Columns;
                rs.PagingState = ResultRowsMetadata.PagingState;
            }

            // if the response has no column definitions, then SKIP_METADATA was set by the driver
            // the driver only sets this flag for bound statements
            if (resultMetadata?.Columns == null)
            {
                resultMetadata = providedResultMetadata?.RowSetMetadata;
                rs.Columns = resultMetadata?.Columns;
            }

            for (var i = 0; i < _rowLength; i++)
            {
                rs.AddRow(ProcessRowItem(reader, resultMetadata));
            }
        }

        internal virtual Row ProcessRowItem(FrameReader reader, RowSetMetadata resultMetadata)
        {
            var rowValues = new object[resultMetadata.Columns.Length];
            for (var i = 0; i < resultMetadata.Columns.Length; i++)
            {
                var c = resultMetadata.Columns[i];
                var length = reader.ReadInt32();
                if (length < 0)
                {
                    rowValues[i] = null;
                    continue;
                }
                var buffer = GetBuffer(length, c.TypeCode);
                rowValues[i] = reader.ReadFromBytes(buffer, 0, length, c.TypeCode, c.TypeInfo);
            }

            return new Row(rowValues, resultMetadata.Columns, resultMetadata.ColumnIndexes);
        }

        /// <summary>
        /// Reduces allocations by reusing a 16-length buffer for types where is possible
        /// </summary>
        private static byte[] GetBuffer(int length, ColumnTypeCode typeCode)
        {
            if (length > ReusableBufferLength)
            {
                return new byte[length];
            }
            switch (typeCode)
            {
                //blob requires a new instance
                case ColumnTypeCode.Blob:
                case ColumnTypeCode.Inet:
                case ColumnTypeCode.Custom:
                case ColumnTypeCode.Decimal:
                    return new byte[length];
            }
            return ReusableBuffer.Value;
        }

        public void Dispose()
        {

        }
    }
}
