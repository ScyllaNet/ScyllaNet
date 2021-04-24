// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Serialization
{
    internal interface ISerializerManager
    {
        ProtocolVersion CurrentProtocolVersion { get; }

        void ChangeProtocolVersion(ProtocolVersion version);

        /// <summary>
        /// Get a serializer for the current protocol version.
        /// </summary>
        /// <returns></returns>
        ISerializer GetCurrentSerializer();

        ///// <summary>
        ///// Adds a UDT mapping definition
        ///// </summary>
        void SetUdtMap(string name, UdtMap map);

        IGenericSerializer GetGenericSerializer();
    }
}
