// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Text;
using Scylla.Net.Collections;
using Scylla.Net.Connections;
using Scylla.Net.SessionManagement;

namespace Scylla.Net
{
    internal class SessionState : ISessionState
    {
        private static readonly IDictionary<Host, HostStateInfo> EmptyDictionary =
            new Dictionary<Host, HostStateInfo>(0);

        private readonly IDictionary<Host, HostStateInfo> _infos;

        internal SessionState(IDictionary<Host, HostStateInfo> infos)
        {
            _infos = infos;
        }

        public IReadOnlyCollection<Host> GetConnectedHosts()
        {
            return new ReadOnlyCollection<Host>(_infos.Keys);
        }

        public int GetOpenConnections(Host host)
        {
            return !_infos.TryGetValue(host, out var info) ? 0 : info.OpenConnections;
        }

        public int GetInFlightQueries(Host host)
        {
            return !_infos.TryGetValue(host, out var info) ? 0 : info.InFlightQueries;
        }

        public override string ToString()
        {
            var builder = new StringBuilder(3 + 70 * _infos.Count);
            builder.Append("{\n");
            foreach (var kv in _infos)
            {
                builder.Append($"  \"{kv.Key.Address}\": {{ openConnections: {kv.Value.OpenConnections}" +
                               $", inFlightQueries: {kv.Value.InFlightQueries} }},\n");
            }
            builder.Append("}");
            return builder.ToString();
        }

        internal static SessionState From(IInternalSession session)
        {
            var pools = session.GetPools();
            var result = new Dictionary<Host, HostStateInfo>();
            foreach (var kv in pools)
            {
                var host = session.Cluster.GetHost(kv.Key);
                if (host == null)
                {
                    continue;
                }
                result[host] = new HostStateInfo(kv.Value);
            }
            return new SessionState(result);
        }

        internal static SessionState Empty()
        {
            return new SessionState(EmptyDictionary);
        }

        internal class HostStateInfo
        {
            public int OpenConnections { get; }

            public int InFlightQueries { get; }
            
            public HostStateInfo(IHostConnectionPool pool)
            {
                OpenConnections = pool.OpenConnections;
                InFlightQueries = pool.InFlight;
            }
        }
    }
}
