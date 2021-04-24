// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Scylla.Net.DataStax.Auth.Sspi
{
    internal static class NativeMethods
    {
        [ReliabilityContract( Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport( "Secur32.dll", EntryPoint = "FreeContextBuffer", CharSet = CharSet.Unicode )]
        internal static extern SecurityStatus FreeContextBuffer( IntPtr buffer );


        [ReliabilityContract( Consistency.WillNotCorruptState, Cer.Success )]
        [DllImport( "Secur32.dll", EntryPoint = "QuerySecurityPackageInfo", CharSet = CharSet.Unicode )]
        internal static extern SecurityStatus QuerySecurityPackageInfo( string packageName, ref IntPtr pkgInfo );

        [ReliabilityContract( Consistency.WillNotCorruptState, Cer.Success )]
        [DllImport( "Secur32.dll", EntryPoint = "EnumerateSecurityPackages", CharSet = CharSet.Unicode )]
        internal static extern SecurityStatus EnumerateSecurityPackages( ref int numPackages, ref IntPtr pkgInfoArry );

    }
}
