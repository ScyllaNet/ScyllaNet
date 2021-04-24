// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading;

namespace Scylla.Net
{
    public class RetryLoadBalancingPolicy : ILoadBalancingPolicy
    {
        public EventHandler<RetryLoadBalancingPolicyEventArgs> ReconnectionEvent;

        public RetryLoadBalancingPolicy(ILoadBalancingPolicy loadBalancingPolicy, IReconnectionPolicy reconnectionPolicy)
        {
            ReconnectionPolicy = reconnectionPolicy;
            LoadBalancingPolicy = loadBalancingPolicy;
        }

        public IReconnectionPolicy ReconnectionPolicy { get; }

        public ILoadBalancingPolicy LoadBalancingPolicy { get; }

        public void Initialize(ICluster cluster)
        {
            LoadBalancingPolicy.Initialize(cluster);
        }

        public HostDistance Distance(Host host)
        {
            return LoadBalancingPolicy.Distance(host);
        }

        public IEnumerable<Host> NewQueryPlan(string keyspace, IStatement query)
        {
            var schedule = ReconnectionPolicy.NewSchedule();
            while (true)
            {
                var childQueryPlan = LoadBalancingPolicy.NewQueryPlan(keyspace, query);
                foreach (var host in childQueryPlan)
                {
                    yield return host;
                }

                if (ReconnectionEvent != null)
                {
                    var ea = new RetryLoadBalancingPolicyEventArgs(schedule.NextDelayMs());
                    ReconnectionEvent(this, ea);
                    if (ea.Cancel)
                    {
                        break;
                    }
                }
                else
                {
                    Thread.Sleep((int) schedule.NextDelayMs());
                }
            }
        }
    }
}
