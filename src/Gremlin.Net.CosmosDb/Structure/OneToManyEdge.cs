using System.Collections.Generic;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// A simple implementation of a one-to-many edge between vertices.
    /// </summary>
    /// <typeparam name="TOutV">The Out vertex type</typeparam>
    /// <typeparam name="TInV">The In vertex type</typeparam>
    public class OneToManyEdge<TOutV, TInV> : IEdge<TOutV, TInV>
        where TOutV : IVertex
        where TInV : IVertex
    {
        /// <summary>
        /// The Out vertex.
        /// </summary>
        public TOutV OutV { get; set; }

        /// <summary>
        /// The list of In vertices.
        /// </summary>
        public List<TInV> InV { get; set; }
    }
}
