﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Diagnostics;
using System.Threading;
using Scylla.Net.Connections;
using Scylla.Net.Metrics.Abstractions;
using Scylla.Net.Metrics.Registries;
using Scylla.Net.Observers.Abstractions;
using Scylla.Net.Responses;

namespace Scylla.Net.Observers
{
    internal class MetricsOperationObserver : IOperationObserver
    {
        private readonly bool _enabledNodeTimerMetrics;
        private static readonly Logger Logger = new Logger(typeof(MetricsOperationObserver));
        private static readonly long Factor = 1000L * 1000L * 1000L / Stopwatch.Frequency;

        private readonly IDriverTimer _operationTimer;
        private long _startTimestamp;

        public MetricsOperationObserver(INodeMetrics nodeMetrics, bool enabledNodeTimerMetrics)
        {
            _enabledNodeTimerMetrics = enabledNodeTimerMetrics;
            _operationTimer = nodeMetrics.CqlMessages;
        }

        public void OnOperationSend(long requestSize, long timestamp)
        {
            if (!_enabledNodeTimerMetrics)
            {
                return;
            }

            try
            {
                Volatile.Write(ref _startTimestamp, timestamp);
            }
            catch (Exception ex)
            {
                MetricsOperationObserver.LogError(ex);
            }
        }

        public void OnOperationReceive(IRequestError error, Response response, long timestamp)
        {
            if (!_enabledNodeTimerMetrics)
            {
                return;
            }

            try
            {
                var startTimestamp = Volatile.Read(ref _startTimestamp);
                if (startTimestamp == 0)
                {
                    MetricsOperationObserver.Logger.Warning("Start timestamp wasn't recorded, discarding this measurement.");
                    return;
                }

                _operationTimer.Record((timestamp - startTimestamp) * MetricsOperationObserver.Factor);
            }
            catch (Exception ex)
            {
                MetricsOperationObserver.LogError(ex);
            }
        }

        private static void LogError(Exception ex)
        {
            MetricsOperationObserver.Logger.Warning("An error occured while recording metrics for a connection operation. Exception = {0}", ex.ToString());
        }
    }
}
