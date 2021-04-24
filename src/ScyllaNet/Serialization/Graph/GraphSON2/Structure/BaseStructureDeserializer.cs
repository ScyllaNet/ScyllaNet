// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using Scylla.Net.DataStax.Graph;
using Newtonsoft.Json.Linq;

namespace Scylla.Net.Serialization.Graph.GraphSON2.Structure
{
    internal abstract class BaseStructureDeserializer
    {
        protected GraphNode ToGraphNode(Func<JToken, GraphNode> factory, JToken token, string propName, bool required = false)
        {
            var prop = !(token is JObject jobj) ? null : jobj[propName];
            if (prop == null)
            {
                if (!required)
                {
                    return null;
                }
                throw new InvalidOperationException($"Required property {propName} not found: {token}");
            }

            return factory.Invoke(prop);
        }

        protected GraphNode ToGraphNode(Func<JToken, GraphNode> factory, JToken token)
        {
            return factory.Invoke(token);
        }

        protected string ToString(JToken token, string propName, bool required = false)
        {
            var prop = !(token is JObject jobj) ? null : jobj[propName];
            if (prop == null)
            {
                if (!required)
                {
                    return null;
                }
                throw new InvalidOperationException($"Required property {propName} not found: {token}");
            }
            return prop.ToString();
        }
    }
}
