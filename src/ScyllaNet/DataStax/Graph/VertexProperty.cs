// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// Internal default implementation of <see cref="IVertexProperty"/>.
    /// </summary>
    internal class VertexProperty : Element, IVertexProperty
    {
        internal VertexProperty(
            GraphNode id, 
            string name, 
            IGraphNode value, 
            IGraphNode vertex, 
            IDictionary<string, GraphNode> properties)
            : base(id, name, properties)
        {
            Name = name;
            Value = value;
            Vertex = vertex;
        }

        public string Name { get; }

        public IGraphNode Value { get; }

        public IGraphNode Vertex { get; }

        public bool Equals(IProperty other)
        {
            return Equals((object) other);
        }

        protected bool Equals(VertexProperty other)
        {
            return string.Equals(Name, other.Name) 
                   && object.Equals(Value, other.Value) 
                   && object.Equals(Vertex, other.Vertex)
                   && string.Equals(Label, other.Label);
        }

        public bool Equals(IVertexProperty other)
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
            return Equals((VertexProperty) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Label != null ? Label.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Vertex != null ? Vertex.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
