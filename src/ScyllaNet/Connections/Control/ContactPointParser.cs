// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net;

namespace Scylla.Net.Connections.Control
{
    internal class ContactPointParser : IContactPointParser
    {
        private readonly IDnsResolver _dnsResolver;
        private readonly ProtocolOptions _protocolOptions;
        private readonly IServerNameResolver _serverNameResolver;
        private readonly bool _keepContactPointsUnresolved;

        public ContactPointParser(
            IDnsResolver dnsResolver, 
            ProtocolOptions protocolOptions, 
            IServerNameResolver serverNameResolver,
            bool keepContactPointsUnresolved)
        {
            _dnsResolver = dnsResolver ?? throw new ArgumentNullException(nameof(dnsResolver));
            _protocolOptions = protocolOptions ?? throw new ArgumentNullException(nameof(protocolOptions));
            _serverNameResolver = serverNameResolver ?? throw new ArgumentNullException(nameof(serverNameResolver));
            _keepContactPointsUnresolved = keepContactPointsUnresolved;
        }

        public IEnumerable<IContactPoint> ParseContactPoints(IEnumerable<object> providedContactPoints)
        {
            var result = new HashSet<IContactPoint>();
            foreach (var contactPoint in providedContactPoints)
            {
                IContactPoint parsedContactPoint;

                switch (contactPoint)
                {
                    case IContactPoint typedContactPoint:
                        parsedContactPoint = typedContactPoint;
                        break;
                    case IPEndPoint ipEndPointContactPoint:
                        parsedContactPoint = new IpLiteralContactPoint(ipEndPointContactPoint, _serverNameResolver);
                        break;
                    case IPAddress ipAddressContactPoint:
                        parsedContactPoint = new IpLiteralContactPoint(ipAddressContactPoint, _protocolOptions, _serverNameResolver);
                        break;
                    case string contactPointText:
                    {
                        if (IPAddress.TryParse(contactPointText, out var ipAddress))
                        {
                            parsedContactPoint = new IpLiteralContactPoint(ipAddress, _protocolOptions, _serverNameResolver);
                        }
                        else
                        {
                            parsedContactPoint = new HostnameContactPoint(
                                _dnsResolver, 
                                _protocolOptions,
                                _serverNameResolver, 
                                _keepContactPointsUnresolved, 
                                contactPointText);
                        }

                        break;
                    }
                    default:
                        throw new InvalidOperationException("Contact points should be either string or IPEndPoint instances");
                }
                
                if (result.Contains(parsedContactPoint))
                {
                    Cluster.Logger.Warning("Found duplicate contact point: {0}. Ignoring it.", contactPoint.ToString());
                    continue;
                }

                result.Add(parsedContactPoint);
            }

            return result;
        }
    }
}
