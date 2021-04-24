// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Snappy
{
    internal class SnappyInternalUtils
    {
        private static readonly IMemory _memory = new SlowMemory();

        private SnappyInternalUtils()
        {
        }

        private static bool Equals(byte[] left, int leftIndex, byte[] right, int rightIndex, int length)
        {
            CheckPositionIndexes(leftIndex, leftIndex + length, left.Length);
            CheckPositionIndexes(rightIndex, rightIndex + length, right.Length);

            for (int i = 0; i < length; i++)
            {
                if (left[leftIndex + i] != right[rightIndex + i])
                {
                    return false;
                }
            }
            return true;
        }

        public static int LookupShort(short[] data, int index)
        {
            return _memory.LookupShort(data, index);
        }

        public static int LoadByte(byte[] data, int index)
        {
            return _memory.LoadByte(data, index);
        }

        public static int LoadInt(byte[] data, int index)
        {
            return _memory.LoadInt(data, index);
        }

        public static void CopyLong(byte[] src, int srcIndex, byte[] dest, int destIndex)
        {
            _memory.CopyLong(src, srcIndex, dest, destIndex);
        }

        public static long LoadLong(byte[] data, int index)
        {
            return _memory.LoadLong(data, index);
        }

        public static void CopyMemory(byte[] input, int inputIndex, byte[] output, int outputIndex, int length)
        {
            _memory.CopyMemory(input, inputIndex, output, outputIndex, length);
        }

        //
        // Copied from Guava Preconditions
        //static <T> T checkNotNull(T reference, string errorMessageTemplate, Object... errorMessageArgs)
        //{
        //    if (reference == null) {
        //        // If either of these parameters is null, the right thing happens anyway
        //        throw new NullPointerException(string.format(errorMessageTemplate, errorMessageArgs));
        //    }
        //    return reference;
        //}

        public static void CheckArgument(bool expression, string errorMessageTemplate, object errorMessageArg0, object errorMessageArg1)
        {
            if (!expression)
            {
                throw new ArgumentException(string.Format(errorMessageTemplate, errorMessageArg0, errorMessageArg1));
            }
        }

        private static void CheckPositionIndexes(int start, int end, int size)
        {
            // Carefully optimized for execution by hotspot (explanatory comment above)
            if (start < 0 || end < start || end > size)
            {
                throw new IndexOutOfRangeException(BadPositionIndexes(start, end, size));
            }
        }

        public static string BadPositionIndexes(int start, int end, int size)
        {
            if (start < 0 || start > size)
            {
                return BadPositionIndex(start, size, "start index");
            }
            if (end < 0 || end > size)
            {
                return BadPositionIndex(end, size, "end index");
            }
            // end < start
            return string.Format("end index ({0}) must not be less than start index ({1})", end, start);
        }

        private static string BadPositionIndex(int index, int size, string desc)
        {
            if (index < 0)
            {
                return string.Format("{0} ({1}) must not be negative", desc, index);
            }
            if (size < 0)
            {
                throw new ArgumentException("negative size: " + size);
            }
            // index > size
            return string.Format("{0} ({1}) must not be greater than size ({2})", desc, index, size);
        }
    }
}
