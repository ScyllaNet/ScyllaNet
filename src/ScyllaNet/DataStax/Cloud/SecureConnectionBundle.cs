// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Security.Cryptography.X509Certificates;

namespace Scylla.Net.DataStax.Cloud
{
    internal class SecureConnectionBundle
    {
        public SecureConnectionBundle(X509Certificate2 caCert, X509Certificate2 clientCert, CloudConfiguration config)
        {
            CaCert = caCert;
            ClientCert = clientCert;
            Config = config;
        }

        public X509Certificate2 CaCert { get; }

        public X509Certificate2 ClientCert { get; }

        public CloudConfiguration Config { get; }
    }
}
