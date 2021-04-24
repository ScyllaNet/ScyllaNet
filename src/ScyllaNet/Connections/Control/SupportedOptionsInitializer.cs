// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scylla.Net.Requests;
using Scylla.Net.Responses;

namespace Scylla.Net.Connections.Control
{
    internal class SupportedOptionsInitializer : ISupportedOptionsInitializer
    {
        private const string SupportedProductTypeKey = "PRODUCT_TYPE";
        private const string SupportedDbaas = "DATASTAX_APOLLO";

        private readonly Metadata _metadata;

        public SupportedOptionsInitializer(Metadata metadata)
        {
            _metadata = metadata;
        }

        public async Task ApplySupportedOptionsAsync(IConnection connection, Host host)
        {
            var request = new OptionsRequest();
            var response = await connection.Send(request).ConfigureAwait(false);

            if (response == null)
            {
                throw new NullReferenceException("Response can not be null");
            }

            if (!(response is SupportedResponse supportedResponse))
            {
                throw new DriverInternalError("Expected SupportedResponse, obtained " + response.GetType().FullName);
            }

            ParseShardingAware(supportedResponse.Output.Options, connection, host);
            ApplyProductTypeOption(supportedResponse.Output.Options);
        }

        private void ParseShardingAware(IDictionary<string, string[]> options,IConnection connection, Host host)
        {
            var sharding = Sharding.ConnectionShardingInfo.ParseShardingInfo(options);
            host.SetShardingInfo(sharding.GetShardingInfo());
        }

        private void ApplyProductTypeOption(IDictionary<string, string[]> options)
        {
            if (!options.TryGetValue(SupportedOptionsInitializer.SupportedProductTypeKey, out var productTypeOptions))
            {
                return;
            }

            if (productTypeOptions.Length <= 0)
            {
                return;
            }

            if (string.Compare(productTypeOptions[0], SupportedOptionsInitializer.SupportedDbaas, StringComparison.OrdinalIgnoreCase) == 0)
            {
                _metadata.SetProductTypeAsDbaas();
            }
        }
    }
}
