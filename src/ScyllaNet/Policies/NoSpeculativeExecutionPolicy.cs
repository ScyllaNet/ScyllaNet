// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    /// A <see cref="ISpeculativeExecutionPolicy"/> that never schedules speculative executions.
    /// </summary>
    public class NoSpeculativeExecutionPolicy : ISpeculativeExecutionPolicy
    {
        private static readonly ISpeculativeExecutionPlan Plan = new NoSpeculativeExecutionPlan();
        public static readonly NoSpeculativeExecutionPolicy Instance = new NoSpeculativeExecutionPolicy();

        private NoSpeculativeExecutionPolicy()
        {
            
        }

        public void Dispose()
        {
            
        }

        public void Initialize(ICluster cluster)
        {
            
        }

        public ISpeculativeExecutionPlan NewPlan(string keyspace, IStatement statement)
        {
            return Plan;
        }

        private class NoSpeculativeExecutionPlan : ISpeculativeExecutionPlan
        {
            public long NextExecution(Host lastQueried)
            {
                return 0L;
            }
        }
    }
}
