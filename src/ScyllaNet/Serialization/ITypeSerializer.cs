// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Serialization
{
    internal interface ITypeSerializer
    {
        Type Type { get; }

        IColumnInfo TypeInfo { get; }

        ColumnTypeCode CqlType { get; }

        object Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo);

        byte[] Serialize(ushort protocolVersion, object obj);
    }
}
