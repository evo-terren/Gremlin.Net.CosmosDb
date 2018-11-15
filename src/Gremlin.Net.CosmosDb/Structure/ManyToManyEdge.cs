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
        /// The list of In vertices.
        /// </summary>
        public IList<TInV> InV
        {
            get { return _inV ?? new TInV[0]; }
            set { _inV = value; }
        }

        /// <summary>
        /// The list of Out vertices.
        /// </summary>
        public IList<TOutV> OutV
        {
            get { return _outV ?? new TOutV[0]; }
            set { _outV = value; }
        }

        private IList<TInV> _inV;
        private IList<TOutV> _outV;
    }
}