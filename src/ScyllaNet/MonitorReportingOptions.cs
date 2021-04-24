// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    /// Options related to Monitor Reporting.
    /// This feature is not supported with Apache Cassandra clusters for now, so in that case this feature will
    /// always be disabled even if it is set as enabled with <see cref="SetMonitorReportingEnabled"/>.
    /// </summary>
    public sealed class MonitorReportingOptions
    {
        internal const long DefaultStatusEventDelayMilliseconds = 300000L;

        internal const bool DefaultMonitorReportingEnabled = true;
        
        internal long StatusEventDelayMilliseconds { get; private set; } = MonitorReportingOptions.DefaultStatusEventDelayMilliseconds;

        /// <summary>
        /// This property is used to determine whether Monitor Reporting is enabled or not.
        /// </summary>
        public bool MonitorReportingEnabled { get; private set; } = MonitorReportingOptions.DefaultMonitorReportingEnabled;
        
        /// <summary>
        /// Determines whether or not events are sent to the connected cluster for monitor reporting.
        /// </summary>
        /// <remarks>If not set through this method, the default value (<code>true</code>) will be used.</remarks>
        /// <param name="monitorReportingEnabled">Flag that controls whether monitor reporting is enabled or disabled.</param>
        /// <returns>This MonitorReportingOptions instance.</returns>
        public MonitorReportingOptions SetMonitorReportingEnabled(bool monitorReportingEnabled)
        {
            MonitorReportingEnabled = monitorReportingEnabled;
            return this;
        }

        internal MonitorReportingOptions SetStatusEventDelayMilliseconds(long delay)
        {
            StatusEventDelayMilliseconds = delay;
            return this;
        } 
    }
}
