// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// Internal default implementation of a property.
    /// </summary>
    internal class Property : IPropertyWithElement
    {
        public string Name { get; }

        public IGraphNode Value { get; }

        public IGraphNode Element { get; }

        internal Property(string name, IGraphNode value, IGraphNode element)
        {
            Name = name;
            Value = value;
            Element = element;
        }

        protected bool Equals(Property other)
        {
            return string.Equals(Name, other.Name) 
                   && object.Equals(Value, other.Value) 
                   && object.Equals(Element, other.Element);
        }

        public bool Equals(IProperty other)
        {
            return Equals((object) other);
        }
        
        public bool Equals(IPropertyWithElement other)
        {
            return Equals((object) other);
        }

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
            return Equals((Property) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Element != null ? Element.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
