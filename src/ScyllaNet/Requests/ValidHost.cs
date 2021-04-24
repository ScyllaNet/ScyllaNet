// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Requests
{
    internal class ValidHost
    {
        private ValidHost(Host host, HostDistance distance)
        {
            Host = host;
            Distance = distance;
        }

        public Host Host { get; }

        public HostDistance Distance { get; }

        /// <summary>
        /// Builds a <see cref="ValidHost"/> instance.
        /// </summary>
        /// <returns>Newly built instance if valid or <code>null</code> if not valid
        /// (e.g. the host is ignored or the driver sees it as down)</returns>
        public static ValidHost New(Host host, HostDistance distance)
        {
            if (distance == HostDistance.Ignored)
            {
                // We should not use an ignored host
                return null;
            }
            
            if (!host.IsUp)
            {
                // The host is not considered UP by the driver.
                // We could have filtered earlier by hosts that are considered UP, but we must
                // check the host distance first.
                return null;
            }

            return new ValidHost(host, distance);
        }
    }
}
