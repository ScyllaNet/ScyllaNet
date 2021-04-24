// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.Serialization;

namespace Scylla.Net
{
    public class DynamicCompositeTypeAdapter : ITypeAdapter
    {
        public Type GetDataType()
        {
            return typeof (byte[]);
        }

        public object ConvertFrom(byte[] decimalBuf)
        {
            return decimalBuf;
        }

        public byte[] ConvertTo(object value)
        {
            TypeSerializer.CheckArgument<byte[]>(value);
            return (byte[]) value;
        }
    }
}
