// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    /// <summary>
    /// Exception that is thrown when the driver expected a type and other was provided
    /// </summary>
    public class InvalidTypeException : DriverException
    {
        public object ReceivedType { get; private set; }
        public object[] ExpectedType { get; private set; }
        public String ParamName { get; private set; }

        public InvalidTypeException(String msg)
            : base(msg)
        {
        }

        public InvalidTypeException(String msg, Exception cause)
            : base(msg, cause)
        {
        }

        public InvalidTypeException(String paramName, object receivedType, object[] expectedType)
            : base(string.Format("Received object of type: {0}, expected: {1} {2}. Parameter name that caused exception: {3}",
                                 receivedType, expectedType[0], expectedType.Length > 1 ? "or" + expectedType[1] : "", paramName))
        {
            ReceivedType = receivedType;
            ExpectedType = expectedType;
            ParamName = paramName;
        }
    }
}
