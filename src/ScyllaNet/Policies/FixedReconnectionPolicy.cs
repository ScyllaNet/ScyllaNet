﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    /// <summary>
    /// Represents a reconnection policy that is possible to specify custom reconnection delays for each attempt.
    /// </summary>
    public class FixedReconnectionPolicy : IReconnectionPolicy
    {
        private readonly long[] _delays;

        /// <summary>
        /// Creates a new instance of a reconnection policy for which is possible to specify custom reconnection delays for each attempt.
        /// <para>The last delay provided will be used for the rest of the attempts.</para>
        /// </summary>
        public FixedReconnectionPolicy(params long[] delays)
        {
            if (delays == null)
            {
                throw new ArgumentNullException("delays");
            }
            if (delays.Length == 0)
            {
                throw new ArgumentException("You should provide at least one delay time in milliseconds");
            }
            _delays = delays;
        }

        /// <summary>
        /// Gets a copy of the provided <see cref="_delays"/> array.
        /// </summary>
        public long[] Delays => (long[])_delays.Clone();

        public IReconnectionSchedule NewSchedule()
        {
            return new FixedReconnectionSchedule(_delays);
        }

        private class FixedReconnectionSchedule : IReconnectionSchedule
        {
            private readonly long[] _delays;

            private int _index;

            public FixedReconnectionSchedule(params long[] delays)
            {
                _delays = delays;
            }

            public long NextDelayMs()
            {
                if (_index >= _delays.Length)
                {
                    return _delays[_delays.Length - 1];
                }
                return _delays[_index++];
            }
        }
    }
}
