// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    ///     Represents an enum.
    /// </summary>
    public abstract class EnumWrapper : IEquatable<EnumWrapper>
    {
        /// <summary>
        ///     Gets the name of the enum.
        /// </summary>
        public string EnumName { get; }

        /// <summary>
        ///     Gets the value of the enum.
        /// </summary>
        public string EnumValue { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="EnumWrapper" /> class.
        /// </summary>
        /// <param name="enumName">The name of the enum.</param>
        /// <param name="enumValue">The value of the enum.</param>
        protected EnumWrapper(string enumName, string enumValue)
        {
            EnumName = enumName;
            EnumValue = enumValue;
        }

        /// <inheritdoc />
        public bool Equals(EnumWrapper other)
        {
            if (object.ReferenceEquals(null, other))
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(EnumName, other.EnumName) && string.Equals(EnumValue, other.EnumValue);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((EnumWrapper) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((EnumName != null ? EnumName.GetHashCode() : 0) * 397) ^
                       (EnumValue != null ? EnumValue.GetHashCode() : 0);
            }
        }
    }
}
