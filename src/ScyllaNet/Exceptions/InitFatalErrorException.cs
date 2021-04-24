// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    public class InitFatalErrorException : Exception
    {
        private const string ExceptionMessage = 
            "An error occured during the initialization of the cluster instance. Further initialization attempts " +
            "for this cluster instance will never succeed and will return this exception instead. The InnerException property holds " +
            "a reference to the exception that originally caused the initialization error.";

        public InitFatalErrorException(Exception innerException) : base(ExceptionMessage, innerException)
        {
        }
    }
}
