// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;

namespace Scylla.Net.Data.Linq
{
    /// <summary>
    /// Represents nested states
    /// </summary>
    internal class VisitingParam<T>
    {
        private readonly Stack<T> _clauses = new Stack<T>();
        private readonly T _defaultValue;

        public VisitingParam(T defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public VisitingParam() : this(default(T))
        {
            
        }

        public IDisposable Set(T val)
        {
            return new ClauseLock(_clauses, val);
        }

        public T Get()
        {
            return _clauses.Count == 0 ? _defaultValue : _clauses.Peek();
        }

        private class ClauseLock : IDisposable
        {
            private readonly Stack<T> _stack;

            public ClauseLock(Stack<T> stack, T val)
            {
                _stack = stack;
                _stack.Push(val);
            }

            void IDisposable.Dispose()
            {
                _stack.Pop();
            }
        }
    }
}
