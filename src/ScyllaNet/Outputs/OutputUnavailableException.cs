// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    internal class OutputUnavailableException : OutputError
    {
        private ConsistencyLevel _consistency;
        private int _required;
        private int _alive;

        protected override void Load(FrameReader cb)
        {
            _consistency = (ConsistencyLevel) cb.ReadInt16();
            _required = cb.ReadInt32();
            _alive = cb.ReadInt32();
        }

        public override DriverException CreateException()
        {
            return new UnavailableException(_consistency, _required, _alive);
        }
    }
}
