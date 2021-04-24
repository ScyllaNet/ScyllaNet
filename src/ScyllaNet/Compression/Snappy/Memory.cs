// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Snappy
{
    internal interface IMemory
    {
        bool FastAccessSupported();

        int LookupShort(short[] data, int index);

        int LoadByte(byte[] data, int index);

        int LoadInt(byte[] data, int index);

        void CopyLong(byte[] src, int srcIndex, byte[] dest, int destIndex);

        long LoadLong(byte[] data, int index);

        void CopyMemory(byte[] input, int inputIndex, byte[] output, int outputIndex, int length);
    }
} // end namespace
