// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Threading.Tasks;

namespace Scylla.Net.DataStax.Cloud
{
    /// <summary>
    /// Client to interact with the Cloud Metadata Service.
    /// </summary>
    internal interface ICloudMetadataService
    {
        /// <summary>
        /// Retrieve the cloud cluster's metadata from the cloud metadata service.
        /// </summary>
        /// <param name="url">Metadata endpoint</param>
        /// <param name="socketOptions">Socket options to use for the HTTPS request (timeout).</param>
        /// <param name="sslOptions">SSL Options to use for the HTTPS request.</param>
        Task<CloudMetadataResult> GetClusterMetadataAsync(string url, SocketOptions socketOptions, SSLOptions sslOptions);
    }
}
