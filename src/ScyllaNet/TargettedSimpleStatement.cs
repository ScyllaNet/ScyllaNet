// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    internal class TargettedSimpleStatement : SimpleStatement
    {
        /// <summary>
        /// The preferred host to be used by the load balancing policy.
        /// </summary>
        public Host PreferredHost { get; set; }

        public TargettedSimpleStatement(string query, params object[] values)
            : base(query, values)
        {

        }

        public TargettedSimpleStatement(string query)
            : base(query)
        {

        }
    }
}
