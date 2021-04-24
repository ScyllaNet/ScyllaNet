// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.MetadataHelpers
{
    internal class DatacenterInfo
    {
        private readonly HashSet<string> _racks;

        public DatacenterInfo()
        {
            _racks = new HashSet<string>();
        }

        public int HostLength { get; set; }

        public ISet<string> Racks { get { return _racks; } }

        public void AddRack(string name)
        {
            if (name == null)
            {
                return;
            }
            _racks.Add(name);
        }
    }

}
