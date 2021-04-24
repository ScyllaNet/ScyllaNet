// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.IO;
using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Cloud
{
    /// <inheritdoc />
    internal class CloudConfigurationParser : ICloudConfigurationParser
    {
        private static readonly Logger Logger = new Logger(typeof(CloudConfigurationParser));

        /// <inheritdoc />
        public CloudConfiguration ParseConfig(Stream stream)
        {
            CloudConfiguration cloudConfiguration;
            using (var configStream = new StreamReader(stream))
            {
                var json = configStream.ReadToEnd();
                cloudConfiguration = JsonConvert.DeserializeObject<CloudConfiguration>(json);
            }

            ValidateConfiguration(cloudConfiguration);

            return cloudConfiguration;
        }
        
        private void ValidateConfiguration(CloudConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentException("Config file is empty.");
            }

            if (config.Port == 0)
            {
                throw new ArgumentException("Could not parse the \"port\" property from the config file.");
            }

            if (string.IsNullOrWhiteSpace(config.Host))
            {
                throw new ArgumentException("Could not parse the \"host\" property from the config file.");
            }
            
            if (string.IsNullOrEmpty(config.CertificatePassword))
            {
                CloudConfigurationParser.Logger.Warning("Could not parse the \"pfxCertPassword\" property from the config file.");
            }
        }
    }
}
