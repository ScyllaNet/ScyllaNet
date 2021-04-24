// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    /// Parses a FunctionFailureException from a function failure error
    /// </summary>
    internal class OutputFunctionFailure : OutputError
    {
        private FunctionFailureException _exception;

        public override DriverException CreateException()
        {
            return _exception;
        }

        protected override void Load(FrameReader reader)
        {
            _exception = new FunctionFailureException(Message)
            {
                Keyspace = reader.ReadString(),
                Name = reader.ReadString(),
                ArgumentTypes = reader.ReadStringList()
            };
        }
    }
}
