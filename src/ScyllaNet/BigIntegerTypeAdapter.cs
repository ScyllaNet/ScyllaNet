﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Numerics;
using Scylla.Net.Serialization;

namespace Scylla.Net
{
    public class BigIntegerTypeAdapter : ITypeAdapter
    {
        public Type GetDataType()
        {
            return typeof (BigInteger);
        }

        public object ConvertFrom(byte[] decimalBuf)
        {
            return new BigInteger(decimalBuf);
        }

        public byte[] ConvertTo(object value)
        {
            TypeSerializer.CheckArgument<BigInteger>(value);
            return ((BigInteger) value).ToByteArray();
        }
    }
}
