using Gremlin.Net.CosmosDb.Structure;
using System.Reflection;

namespace Gremlin.Net.CosmosDb.Serialization
{
    /// <summary>
    /// A connector for <see cref="IEdge"/>. Can connect a <see cref="IEdge"/> to a <see cref="IVertex"/>.
    /// </summary>
    public interface IEdgeConnector : ITreeConnector
    {
        /// <summary>
        /// Connects the <paramref name="edge"/> to the <paramref name="vertex"/> using the specified <paramref name="property"/>.
        /// </summary>
        /// <param name="vertex">The vertex.</param>
        /// <param name="edge">The edge to connect to the vertex.</param>
        /// <param name="property">The vertex property that holds the edge.</param>
        /// <returns></returns>
        bool ConnectEdge(IVertex vertex, IEdge edge, PropertyInfo property);
    }
}