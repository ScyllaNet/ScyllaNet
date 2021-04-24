// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.Requests
{
    internal class RegisterForEventRequest : BaseRequest
    {
        public const byte RegisterOpCode = 0x0B;

        private readonly List<string> _eventTypes;

        public RegisterForEventRequest(CassandraEventType eventTypes) : base(false, null)
        {
            _eventTypes = new List<string>();
            if ((eventTypes & CassandraEventType.StatusChange) == CassandraEventType.StatusChange)
            {
                _eventTypes.Add("STATUS_CHANGE");
            }
            if ((eventTypes & CassandraEventType.TopologyChange) == CassandraEventType.TopologyChange)
            {
                _eventTypes.Add("TOPOLOGY_CHANGE");
            }
            if ((eventTypes & CassandraEventType.SchemaChange) == CassandraEventType.SchemaChange)
            {
                _eventTypes.Add("SCHEMA_CHANGE");
            }
        }

        protected override byte OpCode => RegisterForEventRequest.RegisterOpCode;

        /// <inheritdoc />
        public override ResultMetadata ResultMetadata => null;

        protected override void WriteBody(FrameWriter wb)
        {
            wb.WriteStringList(_eventTypes);
        }
    }
}
