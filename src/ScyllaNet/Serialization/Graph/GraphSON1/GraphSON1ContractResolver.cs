// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Scylla.Net.Serialization.Graph.GraphSON1
{
    internal class GraphSON1ContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// A single instance of a JsonSerializerSettings that uses this ContractResolver.
        /// </summary>
        internal static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new GraphSON1ContractResolver(),
            DateParseHandling = DateParseHandling.None,
            Culture = CultureInfo.InvariantCulture
        };

        protected GraphSON1ContractResolver()
        {

        }

        protected override JsonContract CreateContract(Type objectType)
        {
            var contract = base.CreateContract(objectType);
            if (GraphSON1Converter.Instance.CanConvert(objectType))
            {
                contract.Converter = GraphSON1Converter.Instance;
            }
            return contract;
        }
    }
}
