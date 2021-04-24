// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.IO;

namespace Scylla.Net.DataStax.Cloud
{
    /// <summary>
    /// Parses the configuration data from the secure connection bundle.
    /// </summary>
    internal interface ICloudConfigurationParser
    {
        /// <summary>
        /// Parses config.json file from the secure connection bundle.
        /// </summary>
        /// <param name="stream">Stream that contains the config.json data in text format</param>
        /// <returns>Configuration object mapped from config.json</returns>
        CloudConfiguration ParseConfig(Stream stream);
    }
}
