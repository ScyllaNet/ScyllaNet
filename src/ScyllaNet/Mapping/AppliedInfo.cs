﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Linq;

namespace Scylla.Net.Mapping
{
    /// <summary>
    /// When using Lightweight transactions, it provides information whether the change was applied or not.
    /// </summary>
    public class AppliedInfo<T>
    {
        /// <summary>
        /// Determines if the change was applied.
        /// </summary>
        public bool Applied { get; set; }

        /// <summary>
        /// Gets or sets the existing data that prevented
        /// </summary>
        public T Existing { get; set; }

        /// <summary>
        /// Creates a new instance marking the change as applied 
        /// </summary>
        public AppliedInfo(bool applied)
        {
            Applied = applied;
        }

        /// <summary>
        /// Creates a new instance marking the change as not applied and provides information about the existing data.
        /// </summary>
        /// <param name="existing"></param>
        public AppliedInfo(T existing)
        {
            Applied = false;
            Existing = existing;
        }

        /// <summary>
        /// Adapts a LWT RowSet and returns a new AppliedInfo
        /// </summary>
        internal static AppliedInfo<T> FromRowSet(MapperFactory mapperFactory, string cql, RowSet rs)
        {
            var row = rs.FirstOrDefault();
            const string appliedColumn = "[applied]";
            if (row == null || row.GetColumn(appliedColumn) == null || row.GetValue<bool>(appliedColumn))
            {
                //The change was applied correctly
                return new AppliedInfo<T>(true);
            }
            if (rs.Columns.Length == 1)
            {
                //There isn't more information on why it was not applied
                return new AppliedInfo<T>(false);
            }
            //It was not applied, map the information returned
            var mapper = mapperFactory.GetMapper<T>(cql, rs);
            return new AppliedInfo<T>(mapper(row));
        }
    }
}
