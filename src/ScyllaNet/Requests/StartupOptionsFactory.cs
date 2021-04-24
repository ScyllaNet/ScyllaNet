// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using Scylla.Net.Helpers;

namespace Scylla.Net.Requests
{
    internal class StartupOptionsFactory : IStartupOptionsFactory
    {
        public const string CqlVersionOption = "CQL_VERSION";
        public const string CompressionOption = "COMPRESSION";
        public const string NoCompactOption = "NO_COMPACT";
        public const string DriverNameOption = "DRIVER_NAME";
        public const string DriverVersionOption = "DRIVER_VERSION";
        
        public const string ApplicationNameOption = "APPLICATION_NAME";
        public const string ApplicationVersionOption = "APPLICATION_VERSION";
        public const string ClientIdOption = "CLIENT_ID";

        public const string CqlVersion = "3.0.0";
        public const string SnappyCompression = "snappy";
        public const string Lz4Compression = "lz4";

        private readonly string _appName;
        private readonly string _appVersion;
        private readonly Guid _clusterId;

        public StartupOptionsFactory(Guid clusterId, string appVersion, string appName)
        {
            _appName = appName;
            _appVersion = appVersion;
            _clusterId = clusterId;
        }
        
        public IReadOnlyDictionary<string, string> CreateStartupOptions(ProtocolOptions options)
        {
            var startupOptions = new Dictionary<string, string>
            {
                { StartupOptionsFactory.CqlVersionOption, StartupOptionsFactory.CqlVersion }
            };

            string compressionName = null;
            switch (options.Compression)
            {
                case CompressionType.LZ4:
                    compressionName = StartupOptionsFactory.Lz4Compression;
                    break;
                case CompressionType.Snappy:
                    compressionName = StartupOptionsFactory.SnappyCompression;
                    break;
            }

            if (compressionName != null)
            {
                startupOptions.Add(StartupOptionsFactory.CompressionOption, compressionName);
            }

            if (options.NoCompact)
            {
                startupOptions.Add(StartupOptionsFactory.NoCompactOption, "true");
            }

            startupOptions.Add(StartupOptionsFactory.DriverNameOption, AssemblyHelpers.GetAssemblyTitle(typeof(StartupOptionsFactory)));
            startupOptions.Add(
                StartupOptionsFactory.DriverVersionOption, AssemblyHelpers.GetAssemblyInformationalVersion(typeof(StartupOptionsFactory)));
            
            if (_appName != null)
            {
                startupOptions[StartupOptionsFactory.ApplicationNameOption] = _appName;
            }

            if (_appVersion != null)
            {
                startupOptions[StartupOptionsFactory.ApplicationVersionOption] = _appVersion;
            }
            
            startupOptions[StartupOptionsFactory.ClientIdOption] = _clusterId.ToString();
            return startupOptions;
        }
    }
}
