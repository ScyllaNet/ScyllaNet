// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Runtime.ConstrainedExecution;

namespace Scylla.Net.DataStax.Auth.Sspi.Contexts
{
    /// <summary>
    /// Captures an unmanaged security context handle.
    /// </summary>
    internal class SafeContextHandle : SafeSspiHandle
    {
        [ReliabilityContract( Consistency.WillNotCorruptState, Cer.Success )]
        protected override bool ReleaseHandle()
        {
            SecurityStatus status = ContextNativeMethods.DeleteSecurityContext(
                ref rawHandle
            );

            base.ReleaseHandle();

            return status == SecurityStatus.OK;
        }
    }
}
