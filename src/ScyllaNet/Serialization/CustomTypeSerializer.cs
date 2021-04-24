// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Serialization
{
    /// <summary>
    /// Base serializer for custom types.
    /// </summary>
    public abstract class CustomTypeSerializer<T> : TypeSerializer<T>
    {
        private readonly IColumnInfo _typeInfo;

        public override ColumnTypeCode CqlType
        {
            get { return ColumnTypeCode.Custom; }
        }

        public override IColumnInfo TypeInfo
        {
            get { return _typeInfo; }
        }

        /// <summary>
        /// Creates a new instance of the serializer for custom types.
        /// </summary>
        /// <param name="name">Fully qualified name of the custom type</param>
        protected CustomTypeSerializer(string name)
        {
            _typeInfo = new CustomColumnInfo(name);
        }
    }
}
