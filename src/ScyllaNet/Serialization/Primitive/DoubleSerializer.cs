// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Serialization.Primitive
{
    internal class DoubleSerializer : TypeSerializer<double>
    {
        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Double; }
        }

        public override double Deserialize(ushort protocolVersion, byte[] buffer, int offset, int length, IColumnInfo typeInfo)
        {
            return BeConverter.ToDouble(buffer, offset);
        }

        public override byte[] Serialize(ushort protocolVersion, double value)
        {
            return BeConverter.GetBytes(value);
        }
    }
}
