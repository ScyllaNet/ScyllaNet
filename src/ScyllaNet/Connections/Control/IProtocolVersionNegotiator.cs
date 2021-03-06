// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Threading.Tasks;
using Scylla.Net.Serialization;

namespace Scylla.Net.Connections.Control
{
    internal interface IProtocolVersionNegotiator
    {
        Task<IConnection> ChangeProtocolVersion(Configuration config, ISerializerManager serializer, ProtocolVersion nextVersion, IConnection previousConnection, UnsupportedProtocolVersionException ex = null, ProtocolVersion? previousVersion = null);
        
        Task<IConnection> NegotiateVersionAsync(Configuration config, Metadata metadata, IConnection connection, ISerializerManager serializer);
    }
}
