// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Runtime.InteropServices;

namespace Scylla.Net
{
    /// <summary>
    /// A timestamp generator that guarantees monotonically increasing timestamps among all client threads
    /// and logs warnings when timestamps drift in the future, using Win API high precision
    /// <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/hh706895.aspx">
    /// GetSystemTimePreciseAsFileTime()</see> method call available in Windows 8+ and Windows Server 2012+.
    /// </summary>
    public class AtomicMonotonicWinApiTimestampGenerator : AtomicMonotonicTimestampGenerator
    {
        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.Winapi)]
        private static extern void GetSystemTimePreciseAsFileTime(out long filetime);
        
        protected sealed override long GetTimestamp()
        {
            GetSystemTimePreciseAsFileTime(out var preciseTime);
            var timestamp = DateTime.FromFileTimeUtc(preciseTime);
            return (timestamp.Ticks - UnixEpochTicks)/TicksPerMicrosecond;
        }

        /// <summary>
        /// Creates a new instance of <see cref="AtomicMonotonicTimestampGenerator"/>.
        /// </summary>
        /// <param name="warningThreshold">
        /// Determines how far in the future timestamps are allowed to drift before a warning is logged, expressed
        /// in milliseconds. Default: <c>1000</c>
        /// </param>
        /// <param name="minLogInterval">
        /// In case of multiple log events, it determines the time separation between log events, expressed in 
        /// milliseconds. Use 0 to disable. Default: <c>1000</c>.
        /// </param>
        /// <exception cref="NotSupportedException" />
        public AtomicMonotonicWinApiTimestampGenerator(
            int warningThreshold = DefaultWarningThreshold,
            int minLogInterval = DefaultMinLogInterval) : this(warningThreshold, minLogInterval, Logger)
        {
            
        }

        internal AtomicMonotonicWinApiTimestampGenerator(int warningThreshold, int minLogInterval, Logger logger)
            : base(warningThreshold, minLogInterval, logger)
        {
            // Try using Win 8+ API
            try
            {
                GetTimestamp();
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new NotSupportedException("Win API method GetSystemTimePreciseAsFileTime() not supported", ex);
            }
        }
    }
}
