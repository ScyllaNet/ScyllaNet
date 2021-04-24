// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    internal class OutputUnprepared : OutputError
    {
        private readonly PreparedQueryNotFoundInfo _info = new PreparedQueryNotFoundInfo();

        protected override void Load(FrameReader cb)
        {
            var len = cb.ReadInt16();
            _info.UnknownId = new byte[len];
            cb.Read(_info.UnknownId, 0, len);
        }

        public override DriverException CreateException()
        {
            return new PreparedQueryNotFoundException(Message, _info.UnknownId);
        }
    }
}
