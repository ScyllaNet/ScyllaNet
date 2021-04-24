// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    internal class OutputAlreadyExists : OutputError
    {
        private readonly AlreadyExistsInfo _info = new AlreadyExistsInfo();

        protected override void Load(FrameReader cb)
        {
            _info.Ks = cb.ReadString();
            _info.Table = cb.ReadString();
        }

        public override DriverException CreateException()
        {
            return new AlreadyExistsException(_info.Ks, _info.Table);
        }
    }
}
