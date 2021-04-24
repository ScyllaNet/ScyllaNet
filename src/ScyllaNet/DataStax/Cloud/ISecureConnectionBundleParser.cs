// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.DataStax.Cloud
{
    /// <summary>
    /// Loads and parses the secure connection bundle.
    /// </summary>
    internal interface ISecureConnectionBundleParser
    {
        /// <summary>
        /// Loads and parses the secure connection bundle (creds.zip)
        /// </summary>
        /// <param name="path">Path of the secure connection bundle file.</param>
        /// <returns>The configuration object built from the provided bundle.</returns>
        SecureConnectionBundle ParseBundle(string path);
    }
}
