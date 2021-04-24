// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Net;

namespace Scylla.Net
{
    /// <summary>
    /// The default <c>AddressTranslater</c> used by the driver that performs no translation, returning the same IPEndPoint as the one provided.
    /// </summary>
    internal class DefaultAddressTranslator : IAddressTranslator
    {
        /// <inheritdoc />
        public IPEndPoint Translate(IPEndPoint address)
        {
            return address;
        }
    }
}
