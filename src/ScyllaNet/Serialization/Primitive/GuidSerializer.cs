// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Serialization.Primitive
{
    internal class GuidSerializer : TypeSerializer<Guid>
    {
        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Uuid; }
        }

        public override Guid Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            return new Guid(GuidShuffle(buffer, offset));
        }

        public override byte[] Serialize(ushort protocolVersion, Guid value)
        {
            return GuidShuffle(value.ToByteArray());
        }
    }
}
