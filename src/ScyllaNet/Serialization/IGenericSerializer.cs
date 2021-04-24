// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.Serialization
{
    /// <summary>
    /// Serializer that handles any protocol version.
    /// </summary>
    internal interface IGenericSerializer
    {
        object Deserialize(ProtocolVersion version, byte[] buffer, int offset, int length, ColumnTypeCode typeCode, IColumnInfo typeInfo);

        byte[] Serialize(ProtocolVersion version, object value);

        Type GetClrType(ColumnTypeCode typeCode, IColumnInfo typeInfo);

        Type GetClrTypeForGraph(ColumnTypeCode typeCode, IColumnInfo typeInfo);

        Type GetClrTypeForCustom(IColumnInfo typeInfo);

        ColumnTypeCode GetCqlType(Type type, out IColumnInfo typeInfo);

        /// <summary>
        /// Performs a lightweight validation to determine if the source type and target type matches.
        /// It isn't more strict to support miscellaneous uses of the driver, like direct inputs of blobs and all that. (backward compatibility)
        /// </summary>
        bool IsAssignableFrom(CqlColumn column, object value);

        UdtMap GetUdtMapByName(string name);

        UdtMap GetUdtMapByType(Type type);
    }
}
