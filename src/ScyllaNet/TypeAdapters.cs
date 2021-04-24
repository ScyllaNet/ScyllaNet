// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net
{
    /// <summary>
    /// TypeAdapters are deprecated and will be removed in future versions. Use <see cref="Serialization.TypeSerializer{T}"/> instead.
    /// <para>
    /// Backwards compatibility only.
    /// </para>
    /// </summary>
    public static class TypeAdapters
    {
        public static ITypeAdapter DecimalTypeAdapter = new DecimalTypeAdapter();
        public static ITypeAdapter VarIntTypeAdapter = new BigIntegerTypeAdapter();
        public static ITypeAdapter CustomTypeAdapter = new DynamicCompositeTypeAdapter();
    }
}
