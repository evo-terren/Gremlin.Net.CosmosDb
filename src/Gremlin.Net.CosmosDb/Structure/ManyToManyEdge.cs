using System.Collections.Generic;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// A simple implementation of a many-to-many edge between vertices.
    /// </summary>
    /// <typeparam name="TOutV">The Out vertex type</typeparam>
    /// <typeparam name="TInV">The In vertex type</typeparam>
    public class ManyToManyEdge<TOutV, TInV> : IEdge<TOutV, TInV>
        where TOutV : IVertex
        where TInV : IVertex
    {
        /// <summary>
        /// The list of Out vertices.
        /// </summary>
        public IList<TOutV> OutV { get; set; }

        /// <summary>
        /// The list of In vertices.
        /// </summary>
        public IList<TInV> InV { get; set; }
    }
}
