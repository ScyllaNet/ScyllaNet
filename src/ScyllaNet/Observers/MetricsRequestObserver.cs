// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Diagnostics;
using System.Threading;
using Scylla.Net.Metrics.Abstractions;
using Scylla.Net.Metrics.Internal;
using Scylla.Net.Metrics.Registries;
using Scylla.Net.Observers.Abstractions;
using Scylla.Net.Requests;

namespace Scylla.Net.Observers
{
    internal class MetricsRequestObserver : IRequestObserver
    {
        private static readonly Logger Logger = new Logger(typeof(MetricsRequestObserver));
        private static readonly long Factor = 1000L * 1000L * 1000L / Stopwatch.Frequency;

        private readonly IMetricsManager _manager;
        private readonly IDriverTimer _requestTimer;
        private long _startTimestamp;

        public MetricsRequestObserver(IMetricsManager manager, IDriverTimer requestTimer)
        {
            _manager = manager;
            _requestTimer = requestTimer;
        }

        public void OnSpeculativeExecution(Host host, long delay)
        {
            _manager.GetOrCreateNodeMetrics(host).SpeculativeExecutions.Increment(1);
        }

        public void OnRequestError(Host host, RequestErrorType errorType, RetryDecision.RetryDecisionType decision)
        {
            var nodeMetrics = _manager.GetOrCreateNodeMetrics(host);
            OnRequestError(nodeMetrics.Errors, errorType);
            switch (decision)
            {
                case RetryDecision.RetryDecisionType.Retry:
                    OnRetryPolicyDecision(nodeMetrics.Retries, errorType);
                    break;

                case RetryDecision.RetryDecisionType.Ignore:
                    OnRetryPolicyDecision(nodeMetrics.Ignores, errorType);
                    break;
            }
        }

        private void OnRetryPolicyDecision(IRetryPolicyMetrics metricsRegistry, RequestErrorType reason)
        {
            metricsRegistry.Total.Increment();
            switch (reason)
            {
                case RequestErrorType.Unavailable:
                    metricsRegistry.Unavailable.Increment();
                    break;

                case RequestErrorType.ReadTimeOut:
                    metricsRegistry.ReadTimeout.Increment();
                    break;

                case RequestErrorType.WriteTimeOut:
                    metricsRegistry.WriteTimeout.Increment();
                    break;

                case RequestErrorType.Other:
                case RequestErrorType.Aborted:
                case RequestErrorType.Unsent:
                case RequestErrorType.ClientTimeout:
                    metricsRegistry.Other.Increment();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(reason), reason, null);
            }
        }

        private void OnRequestError(IRequestErrorMetrics metricsRegistry, RequestErrorType errorType)
        {
            switch (errorType)
            {
                case RequestErrorType.Unavailable:
                    metricsRegistry.Unavailable.Increment();
                    break;

                case RequestErrorType.ReadTimeOut:
                    metricsRegistry.ReadTimeout.Increment();
                    break;

                case RequestErrorType.WriteTimeOut:
                    metricsRegistry.WriteTimeout.Increment();
                    break;

                case RequestErrorType.Other:
                    metricsRegistry.Other.Increment();
                    break;

                case RequestErrorType.Aborted:
                    metricsRegistry.Aborted.Increment();
                    break;

                case RequestErrorType.Unsent:
                    metricsRegistry.Unsent.Increment();
                    break;

                case RequestErrorType.ClientTimeout:
                    metricsRegistry.ClientTimeout.Increment();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(errorType), errorType, null);
            }
        }

        public void OnRequestStart()
        {
            if (!_manager.AreSessionTimerMetricsEnabled)
            {
                return;
            }

            Volatile.Write(ref _startTimestamp, Stopwatch.GetTimestamp());
        }

        public void OnRequestFinish(Exception exception)
        {
            if (!_manager.AreSessionTimerMetricsEnabled)
            {
                return;
            }

            try
            {
                var startTimestamp = Volatile.Read(ref _startTimestamp);
                if (startTimestamp == 0)
                {
                    MetricsRequestObserver.Logger.Warning("Start timestamp wasn't recorded, discarding this measurement.");
                    return;
                }

                _requestTimer.Record((Stopwatch.GetTimestamp() - startTimestamp) * MetricsRequestObserver.Factor);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private static void LogError(Exception ex)
        {
            Logger.Warning("An error occured while recording metrics for a request. Exception = {0}", ex.ToString());
        }
    }
}
