// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Scylla.Net.Connections.Control
{
    internal class IpLiteralContactPoint : IContactPoint
    {
        private readonly IPEndPoint _ipEndPoint;
        private readonly ProtocolOptions _protocolOptions = null;
        private readonly IPAddress _ipAddress;
        private readonly IServerNameResolver _serverNameResolver;

        public IpLiteralContactPoint(IPEndPoint ipEndPoint, IServerNameResolver serverNameResolver)
        {
            _ipEndPoint = ipEndPoint ?? throw new ArgumentNullException(nameof(ipEndPoint));
            _serverNameResolver = serverNameResolver ?? throw new ArgumentNullException(nameof(serverNameResolver));
        }

        public IpLiteralContactPoint(IPAddress ipAddress, ProtocolOptions protocolOptions, IServerNameResolver serverNameResolver)
        {
            _ipAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
            _protocolOptions = protocolOptions ?? throw new ArgumentNullException(nameof(protocolOptions));
            _serverNameResolver = serverNameResolver ?? throw new ArgumentNullException(nameof(serverNameResolver));
        }

        public bool CanBeResolved => false;

        public string StringRepresentation => _ipEndPoint != null ? _ipEndPoint.ToString() : $"{_ipAddress}";

        public override string ToString()
        {
            return StringRepresentation;
        }

        private bool TypedEquals(IpLiteralContactPoint other)
        {
            return object.Equals(_ipEndPoint, other._ipEndPoint) && object.Equals(_ipAddress, other._ipAddress);
        }

        public bool Equals(IContactPoint other)
        {
            return Equals((object)other);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is IpLiteralContactPoint typedObj)
            {
                return TypedEquals(typedObj);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Utils.CombineHashCodeWithNulls(new object[] {_ipEndPoint, _ipAddress});
        }

        public Task<IEnumerable<IConnectionEndPoint>> GetConnectionEndPointsAsync(bool refreshCache)
        {
            var result = new List<IConnectionEndPoint>(1)
            {
                _ipEndPoint != null
                    ? new ConnectionEndPoint(_ipEndPoint, _serverNameResolver, this)
                    : new ConnectionEndPoint(new IPEndPoint(_ipAddress, _protocolOptions.Port), _serverNameResolver, this)
            };

            return Task.FromResult(result.AsEnumerable());
        }
    }
}
