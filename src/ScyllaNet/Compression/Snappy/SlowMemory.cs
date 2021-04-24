// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Snappy
{
    internal class SlowMemory : IMemory
    {
        public bool FastAccessSupported()
        {
            return false;
        }


        public int LookupShort(short[] data, int index)
        {
            return data[index] & 0xFFFF;
        }

        public int LoadByte(byte[] data, int index)
        {
            return data[index] & 0xFF;
        }

        public int LoadInt(byte[] data, int index)
        {
            return (data[index] & 0xff) |
                   (data[index + 1] & 0xff) << 8 |
                   (data[index + 2] & 0xff) << 16 |
                   (data[index + 3] & 0xff) << 24;
        }

        public void CopyLong(byte[] src, int srcIndex, byte[] dest, int destIndex)
        {
            for (var i = 0; i < 8; i++)
            {
                dest[destIndex + i] = src[srcIndex + i];
            }
        }

        public long LoadLong(byte[] data, int index)
        {
            return (data[index] & 0xffL) |
                   (data[index + 1] & 0xffL) << 8 |
                   (data[index + 2] & 0xffL) << 16 |
                   (data[index + 3] & 0xffL) << 24 |
                   (data[index + 4] & 0xffL) << 32 |
                   (data[index + 5] & 0xffL) << 40 |
                   (data[index + 6] & 0xffL) << 48 |
                   (data[index + 7] & 0xffL) << 56;
        }

        public void CopyMemory(byte[] input, int inputIndex, byte[] output, int outputIndex, int length)
        {
            Buffer.BlockCopy(input, inputIndex, output, outputIndex, length);
        }
    }
} // end namespace
