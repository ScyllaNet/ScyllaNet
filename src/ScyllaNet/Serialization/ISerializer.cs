// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Serialization
{
    /// <summary>
    /// Serializer instance tied to a specific protocol version.
    /// </summary>
    internal interface ISerializer : IGenericSerializer
    {
        /// <summary>
        /// Protocol version tied to this serializer instance.
        /// </summary>
        ProtocolVersion ProtocolVersion { get; }

        object Deserialize(byte[] buffer, int offset, int length, ColumnTypeCode typeCode, IColumnInfo typeInfo);

        byte[] Serialize(object value);

        /// <summary>
        /// Create a new serializer with the provided protocol version.
        /// </summary>
        ISerializer CloneWithProtocolVersion(ProtocolVersion version);
    }
}
