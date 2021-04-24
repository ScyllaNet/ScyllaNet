// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.Serialization.Geometry
{
    internal abstract class GeometrySerializer<T> : TypeSerializer<T>
    {
        /// <summary>
        /// WKB geometry type codes
        /// </summary>
        internal enum GeometryType
        {
            Point2D = 1,
            LineString = 2,
            Polygon = 3,
            Circle = 101
        }

        /// <summary>
        /// Returns true if the buffer is little endian according to WKB.
        /// </summary>
        protected bool IsLittleEndian(byte[] buffer, int offset)
        {
            return (buffer[offset] == 1);
        }

        /// <summary>
        /// Returns true is the CPU is little-endian.
        /// </summary>
        protected virtual bool UseLittleEndianSerialization()
        {
            // instead of using CPU endianness, we hardcode it to LE.
            // see DSP-10092
            return true;
        }
    }
}
