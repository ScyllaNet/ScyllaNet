// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.IO;

namespace Scylla.Net.Compression
{
    /// <summary>
    /// A simple wrapper to a stream that allows to limit the length of the provided stream.
    /// Used to overcome the deficiencies in the Compression API (not providing a length) 
    /// </summary>
    internal class WrappedStream : Stream
    {
        private readonly Stream _stream;
        private readonly long _length;
        private long _position;

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { return _length; }
        }

        public override long Position
        {
            get { return _position; }
            set
            {
                if (value != 0)
                {
                    throw new NotSupportedException("Setting the position of the stream is not supported");
                }
                _position = value;
            }
        }

        internal WrappedStream(Stream stream, long length)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (stream.Position + length > stream.Length)
            {
                throw new ArgumentOutOfRangeException("length", "length + current position cannot be greater than stream.Length");
            }
            _stream = stream;
            _length = length;
        }

        public override void Flush()
        {
            throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException("offset", "offset cannot be negative");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count", "count cannot be negative");
            }
            if (offset >= buffer.Length)
            {
                throw new ArgumentOutOfRangeException("offset", "offset cannot be greater or equal the buffer length");
            }
            if (count + _position > _length)
            {
                //Do not read pass the length specified in this wrapper
                count = Convert.ToInt32(_length - _position);
            }
            if (count <= 0)
            {
                return 0;
            }
            var amountRead = _stream.Read(buffer, offset, count);
            _position += amountRead;
            return amountRead;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
