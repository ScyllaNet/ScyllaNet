﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Linq;
using System.Reflection;
using Scylla.Net.Mapping;

namespace Scylla.Net.Data.Linq
{
    [Obsolete]
    internal class LinqAttributeBasedColumnDefinition : IColumnDefinition
    {
        public MemberInfo MemberInfo { get; private set; }
        public Type MemberInfoType { get; private set; }
        public string ColumnName { get; private set; }
        public Type ColumnType { get; private set; }
        public bool Ignore { get; private set; }
        public bool IsExplicitlyDefined { get; private set; }
        public bool SecondaryIndex { get; private set; }
        public bool IsCounter { get; private set; }
        public bool IsStatic { get; private set; }
        public bool IsFrozen { get; private set; }
        public bool HasFrozenKey { get; private set; }
        public bool HasFrozenValue { get; private set; }

        /// <summary>
        /// Creates a new column definition for the field specified using any attributes on the field to determine mapping configuration.
        /// </summary>
        public LinqAttributeBasedColumnDefinition(FieldInfo fieldInfo) 
            : this((MemberInfo) fieldInfo)
        {
            MemberInfoType = fieldInfo.FieldType;
        }

        /// <summary>
        /// Creates a new column definition for the property specified using any attributes on the property to determine mapping configuration.
        /// </summary>
        public LinqAttributeBasedColumnDefinition(PropertyInfo propertyInfo) 
            : this((MemberInfo) propertyInfo)
        {
            MemberInfoType = propertyInfo.PropertyType;
        }

        private LinqAttributeBasedColumnDefinition(MemberInfo memberInfo)
        {
            MemberInfo = memberInfo;

            var columnAttribute = (ColumnAttribute) memberInfo.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault();
            if (columnAttribute != null)
            {
                IsExplicitlyDefined = true;

                if (columnAttribute.Name != null)
                {
                    ColumnName = columnAttribute.Name;
                }
            }
            SecondaryIndex = HasAttribute(memberInfo, typeof (SecondaryIndexAttribute));
            IsCounter = HasAttribute(memberInfo, typeof(CounterAttribute));
            IsStatic = HasAttribute(memberInfo, typeof(StaticColumnAttribute));
            Ignore = HasAttribute(memberInfo, typeof(IgnoreAttribute));
        }

        /// <summary>
        /// Determines if the member has an attribute applied
        /// </summary>
        private static bool HasAttribute(MemberInfo memberInfo, Type attributeType)
        {
            return memberInfo.GetCustomAttributes(attributeType, true).FirstOrDefault() != null;
        }
    }
}
