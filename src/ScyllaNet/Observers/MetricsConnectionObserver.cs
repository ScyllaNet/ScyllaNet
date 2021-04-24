// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

using Scylla.Net.Metrics.Registries;
using Scylla.Net.Observers.Abstractions;

namespace Scylla.Net.Observers
{
    internal class MetricsConnectionObserver : IConnectionObserver
    {
        private static readonly Logger Logger = new Logger(typeof(MetricsConnectionObserver));
        private readonly ISessionMetrics _sessionMetrics;
        private readonly INodeMetrics _nodeMetrics;
        private readonly bool _enabledNodeTimerMetrics;

        public MetricsConnectionObserver(ISessionMetrics sessionMetrics, INodeMetrics nodeMetrics, bool enabledNodeTimerMetrics)
        {
            _sessionMetrics = sessionMetrics;
            _nodeMetrics = nodeMetrics;
            _enabledNodeTimerMetrics = enabledNodeTimerMetrics;
        }

        public void OnBytesSent(long size)
        {
            try
            {
                _nodeMetrics.BytesSent.Mark(size);
                _sessionMetrics.BytesSent.Mark(size);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        public void OnBytesReceived(long size)
        {
            try
            {
                _nodeMetrics.BytesReceived.Mark(size);
                _sessionMetrics.BytesReceived.Mark(size);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        public void OnErrorOnOpen(Exception exception)
        {
            try
            {
                switch (exception)
                {
                    case AuthenticationException _:
                        _nodeMetrics.Errors.AuthenticationErrors.Increment(1);
                        break;

                    default:
                        _nodeMetrics.Errors.ConnectionInitErrors.Increment(1);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        public IOperationObserver CreateOperationObserver()
        {
            return new MetricsOperationObserver(_nodeMetrics, _enabledNodeTimerMetrics);
        }

        private static void LogError(Exception ex)
        {
            Logger.Warning("An error occured while recording metrics for a connection. Exception: {0}", ex.ToString());
        }
    }
}
