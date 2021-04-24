// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net
{
    /// <summary>
    /// <para>
    /// This exception is thrown when the driver attempts to re-prepare a statement
    /// and the returned prepared statement's ID is different than the one on the existing
    /// <see cref="PreparedStatement"/>.
    /// </para>
    /// <para>
    /// When this exception is thrown, it means that the <see cref="PreparedStatement"/>
    /// object with the ID that matches <see cref="Id"/> should not be used anymore.
    /// </para>
    /// </summary>
    public class PreparedStatementIdMismatchException : DriverException
    {
        public PreparedStatementIdMismatchException(byte[] originalId, byte[] outputId) 
            : base("ID mismatch while trying to reprepare (expected " 
                   + $"{BitConverter.ToString(originalId).Replace("-", "")}, " 
                   + $"got {BitConverter.ToString(outputId).Replace("-", "")}). " 
                   + "This prepared statement won't work anymore. " 
                   + "This usually happens when you run a 'USE...' query after " 
                   + "the statement was prepared.")
        {
            Id = originalId;
        }

        /// <summary>
        /// ID of the prepared statement that should not be used anymore.
        /// </summary>
        public byte[] Id { get; }
    }
}
