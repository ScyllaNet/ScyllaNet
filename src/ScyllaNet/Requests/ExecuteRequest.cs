// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using Scylla.Net.Serialization;

namespace Scylla.Net.Requests
{
    /// <summary>
    /// Represents a protocol EXECUTE request
    /// </summary>
    internal class ExecuteRequest : BaseRequest, IQueryRequest, ICqlRequest
    {
        public const byte ExecuteOpCode = 0x0A;

        private readonly byte[] _id;
        private readonly QueryProtocolOptions _queryOptions;

        public ConsistencyLevel Consistency 
        { 
            get => _queryOptions.Consistency;
            set => _queryOptions.Consistency = value;
        }

        public byte[] PagingState
        {
            get => _queryOptions.PagingState;
            set => _queryOptions.PagingState = value;
        }

        public int PageSize => _queryOptions.PageSize;

        public ConsistencyLevel SerialConsistency => _queryOptions.SerialConsistency;
        
        public bool SkipMetadata => _queryOptions.SkipMetadata;

        /// <inheritdoc />
        public override ResultMetadata ResultMetadata { get; }

        public ExecuteRequest(
            ISerializer serializer, 
            byte[] id, 
            RowSetMetadata variablesMetadata, 
            ResultMetadata resultMetadata, 
            QueryProtocolOptions queryOptions,
            bool tracingEnabled, 
            IDictionary<string, byte[]> payload) : base(serializer, tracingEnabled, payload)
        {
            var protocolVersion = serializer.ProtocolVersion;
            if (variablesMetadata != null && queryOptions.Values.Length != variablesMetadata.Columns.Length)
            {
                throw new ArgumentException("Number of values does not match with number of prepared statement markers(?).");
            }
            _id = id;
            _queryOptions = queryOptions;

            if (protocolVersion.SupportsResultMetadataId())
            {
                ResultMetadata = resultMetadata;
            }

            if (queryOptions.SerialConsistency != ConsistencyLevel.Any 
                && queryOptions.SerialConsistency.IsSerialConsistencyLevel() == false)
            {
                throw new RequestInvalidException("Non-serial consistency specified as a serial one.");
            }

            if (queryOptions.RawTimestamp != null && !protocolVersion.SupportsTimestamp())
            {
                throw new NotSupportedException("Timestamp for query is supported in Cassandra 2.1 or above.");
            }
        }

        protected override byte OpCode => ExecuteRequest.ExecuteOpCode;

        protected override void WriteBody(FrameWriter wb)
        {
            wb.WriteShortBytes(_id);

            if (ResultMetadata != null)
            {
                wb.WriteShortBytes(ResultMetadata.ResultMetadataId);
            }

            _queryOptions.Write(wb, true);
        }
        
        public void WriteToBatch(FrameWriter wb)
        {
            wb.WriteByte(1); //prepared query
            wb.WriteShortBytes(_id);
            wb.WriteUInt16((ushort)_queryOptions.Values.Length);
            foreach (var queryParameter in _queryOptions.Values)
            {
                wb.WriteAsBytes(queryParameter);
            }
        }
    }
}
