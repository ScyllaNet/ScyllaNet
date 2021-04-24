// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Net;

namespace Scylla.Net
{
    internal class OutputWriteTimeout : OutputError
    {
        private int _blockFor;
        private ConsistencyLevel _consistencyLevel;
        private int _received;
        private string _writeType;
        private readonly bool _isFailure;
        private int _failures;
        private IDictionary<IPAddress, int> _reasons;

        internal OutputWriteTimeout(bool isFailure)
        {
            _isFailure = isFailure;
        }

        protected override void Load(FrameReader reader)
        {
            _consistencyLevel = (ConsistencyLevel) reader.ReadInt16();
            _received = reader.ReadInt32();
            _blockFor = reader.ReadInt32();
            if (_isFailure)
            {
                _failures = reader.ReadInt32();

                if (reader.Serializer.ProtocolVersion.SupportsFailureReasons())
                {
                    _reasons = OutputReadTimeout.GetReasonsDictionary(reader, _failures);
                }
            }
            _writeType = reader.ReadString();
        }

        public override DriverException CreateException()
        {
            if (_isFailure)
            {
                if (_reasons != null)
                {
                    // The message in this protocol provided a full map with the reasons of the failures.
                    return new WriteFailureException(_consistencyLevel, _received, _blockFor, _writeType, _reasons);
                }
                return new WriteFailureException(_consistencyLevel, _received, _blockFor, _writeType, _failures);
            }
            return new WriteTimeoutException(_consistencyLevel, _received, _blockFor, _writeType);
        }
    }
}
