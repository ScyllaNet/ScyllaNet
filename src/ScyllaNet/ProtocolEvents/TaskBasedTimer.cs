// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scylla.Net.ProtocolEvents
{
    internal class TaskBasedTimer : ITimer
    {
        private static readonly Logger Logger = new Logger(typeof(TaskBasedTimer));

        private volatile Task _task;
        private volatile CancellationTokenSource _cts;

        private readonly TaskFactory _taskFactory;
        private volatile bool _disposed = false;

        public TaskBasedTimer(TaskScheduler scheduler)
        {
            _taskFactory = new TaskFactory(
                CancellationToken.None,
                TaskCreationOptions.DenyChildAttach,
                TaskContinuationOptions.DenyChildAttach,
                scheduler);
        }

        public void Dispose()
        {
            // must NOT be running within the exclusive scheduler

            var task = _taskFactory.StartNew(() =>
            {
                _disposed = true;
                Cancel();
                return _task;
            }).GetAwaiter().GetResult(); // wait for Cancel

            task?.GetAwaiter().GetResult(); // wait for _task
        }

        public void Cancel()
        {
            // must be running within the exclusive scheduler

            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        public void Change(Action action, TimeSpan due)
        {
            // must be running within the exclusive scheduler

            if (_disposed)
            {
                return;
            }

            Cancel();

            _cts = new CancellationTokenSource();
            var cts = _cts;

            // ReSharper disable once MethodSupportsCancellation
            _task = Task.Run(async () =>
            {
                // not running within exclusive scheduler
                try
                {
                    if (cts.IsCancellationRequested)
                    {
                        return;
                    }

                    await Task.Delay(due, cts.Token).ConfigureAwait(false);

                    // ReSharper disable once MethodSupportsCancellation
                    var t = _taskFactory.StartNew(() =>
                    {
                        // running within exclusive scheduler
                        if (!cts.IsCancellationRequested)
                        {
                            action();
                        }
                    });

                    await t.ConfigureAwait(false);
                }
                catch (TaskCanceledException) { }
                catch (ObjectDisposedException) { }
                catch (Exception ex)
                {
                    TaskBasedTimer.Logger.Error("Exception thrown in TaskBasedTimer.", ex);
                }
            });
        }
    }
}
