namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// A simple implementation of a one-to-one edge between vertices.
    /// </summary>
    /// <typeparam name="TOutV">The Out vertex type</typeparam>
    /// <typeparam name="TInV">The In vertex type</typeparam>
    public class OneToOneEdge<TOutV, TInV> : IEdge<TOutV, TInV>
        where TOutV : IVertex
        where TInV : IVertex
    {
        /// <summary>
        /// The In vertex.
        /// </summary>
        public TInV InV { get; set; }

        /// <summary>
        /// The Out vertex.
        /// </summary>
        public TOutV OutV { get; set; }
    }
}