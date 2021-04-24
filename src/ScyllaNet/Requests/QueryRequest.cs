// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

using Scylla.Net.Serialization;

namespace Scylla.Net.Requests
{
    /// <summary>
    /// Represents a protocol QUERY request
    /// </summary>
    internal class QueryRequest : BaseRequest, IQueryRequest, ICqlRequest
    {
        public const byte QueryOpCode = 0x07;

        public ConsistencyLevel Consistency
        {
            get { return _queryOptions.Consistency; }
            set { _queryOptions.Consistency = value; }
        }

        public byte[] PagingState
        {
            get { return _queryOptions.PagingState; }
            set { _queryOptions.PagingState = value; }
        }

        public bool SkipMetadata
        {
            get { return _queryOptions.SkipMetadata; }
        }

        public int PageSize
        {
            get { return _queryOptions.PageSize; }
        }

        public ConsistencyLevel SerialConsistency
        {
            get { return _queryOptions.SerialConsistency; }
        }
        
        /// <inheritdoc />
        public override ResultMetadata ResultMetadata => null;

        public string Query { get { return _cqlQuery; } }

        private readonly string _cqlQuery;
        private readonly QueryProtocolOptions _queryOptions;

        public QueryRequest(
            ISerializer serializer,
            string cqlQuery,
            QueryProtocolOptions queryOptions,
            bool tracingEnabled,
            IDictionary<string, byte[]> payload) : base(serializer, tracingEnabled, payload)
        {
            _cqlQuery = cqlQuery;
            _queryOptions = queryOptions;

            if (queryOptions == null)
            {
                throw new ArgumentNullException("queryOptions");
            }

            if (queryOptions.SerialConsistency != ConsistencyLevel.Any && queryOptions.SerialConsistency.IsSerialConsistencyLevel() == false)
            {
                throw new RequestInvalidException("Non-serial consistency specified as a serial one.");
            }

            if (!serializer.ProtocolVersion.SupportsTimestamp())
            {
                //Features supported in protocol v3 and above
                if (queryOptions.RawTimestamp != null)
                {
                    throw new NotSupportedException("Timestamp for query is supported in Cassandra 2.1 and above.");
                }
                if (queryOptions.ValueNames != null && queryOptions.ValueNames.Count > 0)
                {
                    throw new NotSupportedException("Query parameter names feature is supported in Cassandra 2.1 and above.");
                }
            }
        }

        protected override byte OpCode => QueryRequest.QueryOpCode;

        protected override void WriteBody(FrameWriter wb)
        {
            wb.WriteLongString(_cqlQuery);
            _queryOptions.Write(wb, false);
        }

        public void WriteToBatch(FrameWriter wb)
        {
            //not a prepared query
            wb.WriteByte(0);
            wb.WriteLongString(_cqlQuery);
            if (_queryOptions.Values == null || _queryOptions.Values.Length == 0)
            {
                // No values
                wb.WriteUInt16(0);
            }
            else
            {
                wb.WriteUInt16((ushort)_queryOptions.Values.Length);
                foreach (var queryParameter in _queryOptions.Values)
                {
                    wb.WriteAsBytes(queryParameter);
                }
            }
        }
    }
}
