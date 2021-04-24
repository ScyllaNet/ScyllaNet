﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Text;

namespace Scylla.Net.Serialization.Primitive
{
    internal class StringSerializer : TypeSerializer<string>
    {
        private readonly Encoding _encoding;

        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Text; }
        }

        public StringSerializer(Encoding encoding)
        {
            _encoding = encoding;
        }

        public override string Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            return _encoding.GetString(buffer, offset, length);
        }

        public override byte[] Serialize(ushort protocolVersion, string value)
        {
            return _encoding.GetBytes(value);
        }
    }
}
