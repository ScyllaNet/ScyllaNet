// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Net;

namespace Scylla.Net.Serialization.Primitive
{
    internal class IpAddressSerializer : TypeSerializer<IPAddress>
    {
        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Inet; }
        }

        public override IPAddress Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            if (length == 4 || length == 16)
            {
                return new IPAddress(Utils.FromOffset(buffer, offset, length));
            }
            throw new DriverInternalError("Invalid length of Inet address: " + length);
        }

        public override byte[] Serialize(ushort protocolVersion, IPAddress value)
        {
            return value.GetAddressBytes();
        }
    }
}
