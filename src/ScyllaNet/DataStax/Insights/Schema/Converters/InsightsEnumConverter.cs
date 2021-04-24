﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Scylla.Net.DataStax.Insights.Schema.Converters
{
    /// <summary>
    /// This JsonConverter implementation serializes values to their string representation in Insights schema
    /// </summary>
    /// <typeparam name="TEnumType">Type of objects that this converter can convert.</typeparam>
    /// <typeparam name="TJsonType">Type of objects that will be created by this converter.</typeparam>
    internal abstract class InsightsEnumConverter<TEnumType, TJsonType> : JsonConverter
    {
        private static readonly string TypeString = typeof(TEnumType).ToString();
        private static readonly Logger Logger = new Logger(typeof(InsightsEnumConverter<TEnumType, TJsonType>));

        protected abstract IReadOnlyDictionary<TEnumType, TJsonType> EnumToJsonValueMap { get; }

        /// <summary>
        /// Converts the provided value (of the specified input type) to the specified output type.
        /// </summary>
        /// <param name="value">Input value</param>
        /// <param name="output">Output variable where the output value will be stored.</param>
        /// <returns><code>true</code> if conversion was successful; <code>false</code> otherwise.</returns>
        public bool TryConvert(TEnumType value, out TJsonType output)
        {
            if (!EnumToJsonValueMap.TryGetValue(value, out output))
            {
                InsightsEnumConverter<TEnumType, TJsonType>.Logger.Error(
                    $"Unrecognized value for type { InsightsEnumConverter<TEnumType, TJsonType>.TypeString }.");
                return false;
            }

            return true;
        }

        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var enumValue = (TEnumType)value;
            if (!TryConvert(enumValue, out var enumValueJsonValue))
            {
                InsightsEnumConverter<TEnumType, TJsonType>.Logger.Error($"Unrecognized value for type { InsightsEnumConverter<TEnumType, TJsonType>.TypeString }.");
                writer.WriteNull();
                return;
            }
            
            writer.WriteValue(enumValueJsonValue);
        }

        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var existingValueJson = (TJsonType)Convert.ChangeType(reader.Value, typeof(TJsonType));

            foreach (var kvp in EnumToJsonValueMap)
            {
                if (kvp.Value.Equals(existingValueJson))
                {
                    return kvp.Key;
                }
            }

            throw new ArgumentException($"could not convert {existingValueJson} to {InsightsEnumConverter<TEnumType, TJsonType>.TypeString}");
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TEnumType);
        }
    }
}
