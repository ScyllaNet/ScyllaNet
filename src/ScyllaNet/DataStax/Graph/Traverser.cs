// Copyright (c) 2014-2020 DataStax Inc.
// Copyright (c) 2020, Rafael Almeida (ralmsdevelper)
// Licensed under the Apache License, Version 2.0. See LICENCE in the project root for license information.

namespace Scylla.Net.DataStax.Graph
{
    /// <summary>
    ///     A traverser represents the current state of an object flowing through a traversal.
    /// </summary>
    public class Traverser
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Traverser" /> class.
        /// </summary>
        /// <param name="obj">The object of the traverser.</param>
        /// <param name="bulk">The number of traversers represented in this traverser.</param>
        public Traverser(GraphNode obj, long bulk = 1)
        {
            Object = obj;
            Bulk = bulk;
        }

        /// <summary>
        ///     Gets the object of this traverser.
        /// </summary>
        public GraphNode Object { get; }

        /// <summary>
        ///     Gets the number of traversers represented in this traverser.
        /// </summary>
        public long Bulk { get; internal set; }

        /// <inheritdoc />
        public bool Equals(Traverser other)
        {
            if (object.ReferenceEquals(null, other)) return false;
            if (object.ReferenceEquals(this, other)) return true;
            return object.Equals(Object, other.Object);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj)) return false;
            if (object.ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Traverser) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Object != null ? Object.GetHashCode() : 0;
        }
    }
}
