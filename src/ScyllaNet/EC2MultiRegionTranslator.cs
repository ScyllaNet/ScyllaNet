// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Net;
using System.Net.Sockets;
using Scylla.Net.Tasks;

namespace Scylla.Net
{
    /// <summary>
    /// An <see cref="IAddressTranslator"/> implementation for multi-region EC2 deployments where clients are also
    /// deployed in EC2 in order to optimizes network costs, as Amazon charges more for communication over public IPs.
    /// <para>
    /// Its distinctive feature is that it translates addresses according to the location of the server host:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Addresses in different EC2 regions (than the client) are unchanged</description></item>
    /// <item><description>Addresses in the same EC2 region are translated to private IPs</description></item>
    /// </list>
    /// </summary>
    public class EC2MultiRegionTranslator : IAddressTranslator
    {
        private static readonly Logger Logger = new Logger(typeof(EC2MultiRegionTranslator));

        /// <summary>
        /// Addresses in the same EC2 region are translated to private IPs and addresses in different EC2 regions
        /// (than the client) are unchanged.
        /// </summary>
        public IPEndPoint Translate(IPEndPoint endpoint)
        {
            // Reverse dns resolution to obtain the name and lookup addresses for the name
            IPHostEntry hostEntry = null;
            try
            {
                hostEntry = TaskHelper.WaitToComplete(Dns.GetHostEntryAsync(endpoint.Address));
            }
            catch (SocketException ex)
            {
                // Unresolved / Device not found
                Logger.Error(ex);
            }

            var hostName = hostEntry?.HostName;
            if (hostName == null)
            {
                return endpoint;
            }

            var lookupAddresses = TaskHelper.WaitToComplete(Dns.GetHostAddressesAsync(hostName));
            if (lookupAddresses.Length == 0)
            {
                return endpoint;
            }

            return new IPEndPoint(lookupAddresses[0] ?? endpoint.Address, endpoint.Port);
        }
    }
}
