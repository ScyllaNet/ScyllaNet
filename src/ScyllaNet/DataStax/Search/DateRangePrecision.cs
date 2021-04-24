﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.DataStax.Search
{
    /// <summary>
    /// Defines the possible values of date range precision.
    /// </summary>
    public enum DateRangePrecision : byte
    {
        /// <summary>
        /// Year precision. Any timestamp precision beyond the year portion will be ignored.
        /// </summary>
        Year = 0,
        
        /// <summary>
        /// Year precision. Any timestamp precision beyond the years portion will be ignored.
        /// </summary>
        Month = 1,
        
        /// <summary>
        /// Day precision. Any timestamp precision beyond the days portion will be ignored.
        /// </summary>
        Day = 2,
        
        /// <summary>
        /// Hour precision. Any timestamp precision beyond the hours portion will be ignored.
        /// </summary>
        Hour = 3,
        
        /// <summary>
        /// Minute precision. Any timestamp precision beyond the minutes portion will be ignored.
        /// </summary>
        Minute = 4,
        
        /// <summary>
        /// Second precision. Any timestamp precision beyond the seconds portion will be ignored.
        /// </summary>
        Second = 5,

        /// <summary>
        /// Millisecond precision.
        /// </summary>
        Millisecond = 6
    }
}
