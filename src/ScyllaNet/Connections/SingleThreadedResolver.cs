// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Threading;
using System.Threading.Tasks;

using Scylla.Net.Tasks;

namespace Scylla.Net.Connections
{
    internal abstract class SingleThreadedResolver
    {
        private volatile Task _currentTask;

        protected SemaphoreSlim RefreshSemaphoreSlim { get; } = new SemaphoreSlim(1, 1);

        /// <summary>
        /// This method makes sure that there are no concurrent refresh operations.
        /// </summary>
        protected async Task SafeRefreshIfNeededAsync(Func<bool> refreshNeeded, Func<Task> refreshFunc)
        {
            if (!refreshNeeded())
            {
                return;
            }

            var task = _currentTask;
            if (task != null && !task.HasFinished())
            {
                await task.ConfigureAwait(false);
                return;
            }

            // Use a lock for avoiding concurrent calls to RefreshAsync()
            await RefreshSemaphoreSlim.WaitAsync().ConfigureAwait(false);
            try
            {
                if (!refreshNeeded())
                {
                    return;
                }

                var newTask = _currentTask;

                if ((newTask == null && task != null)
                    || (newTask != null && task == null)
                    || (newTask != null && task != null && !object.ReferenceEquals(newTask, task)))
                {
                    // another thread refreshed
                    task = newTask ?? TaskHelper.Completed;
                }
                else
                {
                    task = refreshFunc();
                    _currentTask = task;
                }
            }
            finally
            {
                RefreshSemaphoreSlim.Release();
            }

            await task.ConfigureAwait(false);
        }
    }
}
