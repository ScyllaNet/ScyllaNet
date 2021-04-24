// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Scylla.Net.DataStax.Cloud
{
    /// <inheritdoc />
    internal class SecureConnectionBundleParser : ISecureConnectionBundleParser
    {
        private const string CaName = "ca.crt";
        private const string ConfigName = "config.json";
        private const string CertName = "cert.pfx";

        private static readonly Logger Logger = new Logger(typeof(SecureConnectionBundleParser));

        private readonly CloudConfigurationParser _configParser;

        public SecureConnectionBundleParser()
        {
            _configParser = new CloudConfigurationParser();
        }

        /// <inheritdoc />
        public SecureConnectionBundle ParseBundle(string path)
        {
            var archive = ZipFile.OpenRead(path);

            var caCert = LoadCertificateAuthority(archive);
            var config = ParseConfig(archive);
            var clientCert = LoadClientCertificate(archive, config.CertificatePassword);

            return new SecureConnectionBundle(caCert, clientCert, config);
        }

        private CloudConfiguration ParseConfig(ZipArchive archive)
        {
            var config = archive.Entries.FirstOrDefault(entry => entry.Name.Equals(SecureConnectionBundleParser.ConfigName));

            if (config == null)
            {
                throw new SecureConnectionBundleException(
                    $"Could not get {SecureConnectionBundleParser.ConfigName} from the secure connection bundle.");
            }

            return _configParser.ParseConfig(config.Open());
        }

        private X509Certificate2 LoadCertificateAuthority(ZipArchive archive)
        {
            var caEntry = archive.Entries.FirstOrDefault(entry => entry.Name.Equals(SecureConnectionBundleParser.CaName));
            if (caEntry == null)
            {
                throw new SecureConnectionBundleException(
                    $"Could not get {SecureConnectionBundleParser.CaName} from the secure connection bundle.");
            }

            X509Certificate2 caCert;
            using (var memoryStream = new MemoryStream())
            {
                using (var caStream = caEntry.Open())
                {
                    caStream.CopyTo(memoryStream);
                }

                caCert = new X509Certificate2(memoryStream.ToArray());
            }

            return caCert;
        }

        private X509Certificate2 LoadClientCertificate(ZipArchive archive, string password)
        {
            var clientCertEntry = archive.Entries.FirstOrDefault(entry => entry.Name.Equals(SecureConnectionBundleParser.CertName));
            if (clientCertEntry == null)
            {
                SecureConnectionBundleParser.Logger.Warning(
                    $"Could not get {SecureConnectionBundleParser.CertName} from the secure connection bundle. " +
                    "The driver will attempt to connect without client authentication.");
                return null;
            }

            if (password == null)
            {
                SecureConnectionBundleParser.Logger.Warning(
                    "The certificate password that was obtained from the bundle is null. " +
                    "The driver will assume that the certificate is not password protected.");
            }

            X509Certificate2 clientCert;
            using (var memoryStream = new MemoryStream())
            {
                using (var clientCertStream = clientCertEntry.Open())
                {
                    clientCertStream.CopyTo(memoryStream);
                }

                clientCert = new X509Certificate2(memoryStream.ToArray(), password);
            }
            return clientCert;
        }
    }
}
