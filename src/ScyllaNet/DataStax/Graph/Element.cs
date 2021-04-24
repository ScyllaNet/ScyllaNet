// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// Base class for vertices and edges
    /// </summary>
    public abstract class Element : IElement
    {
        public bool Equals(IElement other)
        {
            return object.Equals(Id, other?.Id);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj)) return false;
            if (object.ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((IElement) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        /// <summary>
        /// Creates a new instance of a Graph <see cref="Element"/>.
        /// </summary>
        protected Element(GraphNode id, string label, IDictionary<string, GraphNode> properties)
        {
            Id = id;
            Label = label;
            Properties = properties;
        }

        /// <summary>
        /// Gets the identifier
        /// </summary>
        public GraphNode Id { get; }

        /// <summary>
        /// Gets the identifier
        /// </summary>
        IGraphNode IElement.Id => Id;

        /// <summary>
        /// Gets the label of the element
        /// </summary>
        public string Label { get; }


        /// <summary>
        /// Gets the properties
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Naming", 
            "CA1721:Property names should not match get methods", 
            Justification = "Public API")]
        public IDictionary<string, GraphNode> Properties { get; }

        /// <summary>
        /// Gets a property by name.
        /// </summary>
        public IProperty GetProperty(string name)
        {
            if (!Properties.TryGetValue(name, out var result))
            {
                return null;
            }

            return CastOrCreateProperty(name, result);
        }

        /// <summary>
        /// Gets all properties of an element.
        /// </summary>
        public IEnumerable<IProperty> GetProperties()
        {
            return Properties.Select(item => CastOrCreateProperty(item.Key, item.Value));
        }

        private IProperty CastOrCreateProperty(string name, GraphNode value)
        {
            if (value.GetGraphSONType() == "g:Property")
            {
                return value.To<IProperty>();
            }

            return new Property(name, value, null);
        }
    }
}
