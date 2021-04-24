// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Threading.Tasks;
using Scylla.Net.Serialization;

namespace Scylla.Net.Connections.Control
{
    internal class ProtocolVersionNegotiator : IProtocolVersionNegotiator
    {
        public async Task<IConnection> NegotiateVersionAsync(
            Configuration config,
            Metadata metadata,
            IConnection connection,
            ISerializerManager serializer)
        {
            var commonVersion = serializer.CurrentProtocolVersion.GetHighestCommon(config, metadata.Hosts);
            if (commonVersion != serializer.CurrentProtocolVersion)
            {
                // Current connection will be closed and reopened
                connection = await ChangeProtocolVersion(
                    config, serializer, commonVersion, connection).ConfigureAwait(false);
            }

            return connection;
        }

        public async Task<IConnection> ChangeProtocolVersion(
            Configuration config,
            ISerializerManager serializer,
            ProtocolVersion nextVersion,
            IConnection previousConnection,
            UnsupportedProtocolVersionException ex = null,
            ProtocolVersion? previousVersion = null)
        {
            if (!nextVersion.IsSupported(config) || nextVersion == previousVersion)
            {
                nextVersion = nextVersion.GetLowerSupported(config);
            }

            if (nextVersion == 0)
            {
                if (ex != null)
                {
                    // We have downgraded the version until is 0 and none of those are supported
                    throw ex;
                }

                // There was no exception leading to the downgrade, signal internal error
                throw new DriverInternalError("Connection was unable to STARTUP using protocol version 0");
            }

            ControlConnection.Logger.Info(ex != null
                ? $"{ex.Message}, trying with version {nextVersion:D}"
                : $"Changing protocol version to {nextVersion:D}");

            serializer.ChangeProtocolVersion(nextVersion);

            previousConnection.Dispose();

            var c = config.ConnectionFactory.CreateUnobserved(
                serializer.GetCurrentSerializer(),
                previousConnection.EndPoint,
                config);
            try
            {
                await c.Open().ConfigureAwait(false);
                return c;
            }
            catch
            {
                c.Dispose();
                throw;
            }
        }
    }
}
