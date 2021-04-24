// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Runtime.InteropServices;

namespace Scylla.Net.DataStax.Auth.Sspi.Contexts
{
    /// <summary>
    /// Stores the result of a context query for the context's buffer sizes.
    /// </summary>
    [StructLayout( LayoutKind.Sequential )]
    internal struct SecPkgContext_Sizes
    {
        public int MaxToken;
        public int MaxSignature;
        public int BlockSize;
        public int SecurityTrailer;
    }

    /// <summary>
    /// Stores the result of a context query for a string-valued context attribute.
    /// </summary>
    [StructLayout( LayoutKind.Sequential )]
    internal struct SecPkgContext_String
    {
        public IntPtr StringResult;
    }
}
