// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Runtime.InteropServices;

namespace Scylla.Net.DataStax.Auth.Sspi.Credentials
{
    /// <summary>
    /// Stores the result from a query of a credential's principle name.
    /// </summary>
    [StructLayout( LayoutKind.Sequential )]
    internal struct QueryNameAttribCarrier
    {
        /// <summary>
        /// A pointer to a null-terminated ascii-encoded containing the principle name 
        /// associated with a credential
        /// </summary>
        public IntPtr Name;
    }
}
