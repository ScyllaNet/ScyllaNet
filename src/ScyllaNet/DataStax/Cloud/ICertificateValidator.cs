// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Scylla.Net.DataStax.Cloud
{
    /// <summary>
    /// Validates a certificate (and its chain). Used in server certificate callbacks with SslStream, HttpClient, etc.
    /// </summary>
    internal interface ICertificateValidator
    {
        bool Validate(X509Certificate cert, X509Chain chain, SslPolicyErrors errors);
    }
}
