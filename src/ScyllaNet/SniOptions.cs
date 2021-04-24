// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Net;

namespace Scylla.Net
{
    /// <summary>
    /// This class contains properties related to the proxy when using SNI.
    /// </summary>
    internal class SniOptions : IEquatable<SniOptions>
    {
        public SniOptions(IPAddress ip, int port, string name)
        {
            Ip = ip;
            Port = port;
            Name = name;
        }

        public IPAddress Ip { get; }

        public string Name { get; }

        public int Port { get; }

        public bool IsIp => Ip != null;

        public string StringRepresentation => IsIp ? $"{Ip}:{Port}" : Name;

        private bool TypedEquals(SniOptions other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(Ip, other.Ip) && string.Equals(Name, other.Name) && Port == other.Port;
        }

        public bool Equals(SniOptions other)
        {
            return TypedEquals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is SniOptions sniOptions)
            {
                return TypedEquals(sniOptions);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Utils.CombineHashCodeWithNulls(new object[]
            {
                Ip,
                Port,
                Name
            });
        }
    }
}
