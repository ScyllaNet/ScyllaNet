﻿// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    /// Represents a vertex in DSE graph.
    /// </summary>
    public class Vertex : Element, IVertex
    {
        /// <summary>
        /// Creates a new <see cref="Vertex"/> instance.
        /// </summary>
        public Vertex(GraphNode id, string label, IDictionary<string, GraphNode> properties) 
            : base(id, label, properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }
        }

        /// <summary>
        /// Gets the first property of this element that has the given name, or null if the property 
        /// does not exist.
        /// <para>
        /// If more than one property of this element has the given name, it will return one of them 
        /// (unspecified order).
        /// </para>
        /// </summary>
        /// <param name="name">The name of the property</param>
        /// <returns>The property or null.</returns>
        public new IVertexProperty GetProperty(string name)
        {
            return GetProperties(name).FirstOrDefault();
        }
        
        IProperty IElement.GetProperty(string name)
        {
            return GetProperty(name);
        }
        
        /// <summary>
        /// Gets the properties of this element that has the given name.
        /// </summary>
        /// <param name="name">The name of the property</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Naming", 
            "CA1721:Property names should not match get methods", 
            Justification = "Public API")]
        public IEnumerable<IVertexProperty> GetProperties(string name)
        {
            if (!Properties.TryGetValue(name, out var result))
            {
                return Enumerable.Empty<IVertexProperty>();   
            }
            if (!result.IsArray)
            {
                throw new InvalidOperationException("Expected an array of properties");
            }
            return result.ToArray().Select(item => item.To<IVertexProperty>());
        }
        
        /// <summary>
        /// Gets the properties of this element.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Naming", 
            "CA1721:Property names should not match get methods", 
            Justification = "Public API")]
        public new IEnumerable<IVertexProperty> GetProperties()
        {
            return Properties.SelectMany(prop => GetProperties(prop.Key));
        }

        IEnumerable<IProperty> IElement.GetProperties()
        {
            return GetProperties();
        }
    }
}
