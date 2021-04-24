// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

using Scylla.Net.Serialization;

namespace Scylla.Net.Requests
{
    internal class PrepareRequest : BaseRequest
    {
        public const byte PrepareOpCode = 0x09;

        private readonly PrepareFlags? _prepareFlags = PrepareFlags.None;

        [Flags]
        internal enum PrepareFlags
        {
            None = 0,
            WithKeyspace = 0x01
        }

        /// <summary>
        /// Gets the keyspace for the query, only defined when keyspace is different than the current keyspace.
        /// </summary>
        public string Keyspace { get; }

        /// <summary>
        /// The CQL string to be prepared
        /// </summary>
        public string Query { get; set; }
        
        /// <inheritdoc />
        public override ResultMetadata ResultMetadata => null;

        public PrepareRequest(
            ISerializer serializer, string cqlQuery, string keyspace, IDictionary<string, byte[]> payload)
            : base(serializer, false, payload)
        {
            Query = cqlQuery;
            Keyspace = keyspace;

            if (!serializer.ProtocolVersion.SupportsKeyspaceInRequest())
            {
                // if the keyspace parameter is not supported then prepare flags aren't either
                _prepareFlags = null;
                
                // and also no other optional parameter is supported
                return;
            }
            
            if (keyspace != null)
            {
                _prepareFlags |= PrepareFlags.WithKeyspace;
            }
        }

        protected override byte OpCode => PrepareRequest.PrepareOpCode;

        protected override void WriteBody(FrameWriter wb)
        {
            wb.WriteLongString(Query);

            if (_prepareFlags != null)
            {
                wb.WriteInt32((int)_prepareFlags);
                if (_prepareFlags.Value.HasFlag(PrepareFlags.WithKeyspace))
                {
                    wb.WriteString(Keyspace);
                }
            }
        }
    }
}
