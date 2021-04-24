﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Scylla.Net.Collections;
using Scylla.Net.Connections;
using Scylla.Net.Connections.Control;

namespace Scylla.Net
{
    internal class Hosts : IEnumerable<Host>
    {
        private readonly CopyOnWriteDictionary<IPEndPoint, Host> _hosts = new CopyOnWriteDictionary<IPEndPoint, Host>();

        /// <summary>
        /// Event that gets triggered when a host is considered as DOWN (not available)
        /// </summary>
        internal event Action<Host> Down;

        /// <summary>
        /// Event that gets triggered when a host is considered back UP (available for queries)
        /// </summary>
        internal event Action<Host> Up;

        /// <summary>
        /// Event that gets triggered when a new host has been added to the pool
        /// </summary>
        internal event Action<Host> Added;

        /// <summary>
        /// Event that gets triggered when a host has been removed
        /// </summary>
        internal event Action<Host> Removed;

        /// <summary>
        /// Gets the total amount of hosts in the cluster
        /// </summary>
        internal int Count
        {
            get { return _hosts.Count; }
        }

        public bool TryGet(IPEndPoint endpoint, out Host host)
        {
            return _hosts.TryGetValue(endpoint, out host);
        }

        public ICollection<Host> ToCollection()
        {
            return _hosts.Values;
        }

        /// <summary>
        /// Adds the host if not exists
        /// </summary>
        public Host Add(IPEndPoint key)
        {
            return Add(key, null);
        }
        
        /// <summary>
        /// Adds the host if not exists
        /// </summary>
        public Host Add(IPEndPoint key, IContactPoint contactPoint)
        {
            var newHost = new Host(key, contactPoint);
            var host = _hosts.GetOrAdd(key, newHost);
            if (!ReferenceEquals(newHost, host))
            {
                //The host was not added, return the existing host
                return host;
            }
            //The node was added
            host.Down += OnHostDown;
            host.Up += OnHostUp;
            Added?.Invoke(newHost);
            return host;
        }

        private void OnHostDown(Host sender)
        {
            Down?.Invoke(sender);
        }

        private void OnHostUp(Host sender)
        {
            Up?.Invoke(sender);
        }

        public void RemoveIfExists(IPEndPoint ep)
        {
            if (!_hosts.TryRemove(ep, out Host host))
            {
                //The host does not exists
                return;
            }
            host.Down -= OnHostDown;
            host.Up -= OnHostUp;
            host.SetAsRemoved();
            Removed?.Invoke(host);
        }

        public IEnumerable<IPEndPoint> AllEndPointsToCollection()
        {
            return _hosts.Keys;
        }

        public IEnumerator<Host> GetEnumerator()
        {
            return _hosts.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _hosts.Values.GetEnumerator();
        }
    }
}
