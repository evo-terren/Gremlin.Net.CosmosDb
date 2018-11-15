using System.Collections.Generic;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// A simple implementation of a many-to-one edge between vertices.
    /// </summary>
    /// <typeparam name="TOutV">The Out vertex type</typeparam>
    /// <typeparam name="TInV">The In vertex type</typeparam>
    public class ManyToOneEdge<TOutV, TInV> : IEdge<TOutV, TInV>
        where TOutV : IVertex
        where TInV : IVertex
    {
        /// <summary>
        /// The In vertex.
        /// </summary>
        public TInV InV { get; set; }

        /// <summary>
        /// The list of Out vertices.
        /// </summary>
        public IList<TOutV> OutV
        {
            get { return _outV ?? new TOutV[0]; }
            set { _outV = value; }
        }

        private IList<TOutV> _outV;
    }
}