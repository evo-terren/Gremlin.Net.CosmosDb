using Gremlin.Net.CosmosDb.Structure;

namespace Gremlin.Net.CosmosDb.Serialization
{
    /// <summary>
    /// A connector for <see cref="IVertex"/>. Can connect a <see cref="IVertex"/> to an <see cref="IEdge"/>.
    /// </summary>
    public interface IVertexConnector : ITreeConnector
    {
        /// <summary>
        /// Connects the <paramref name="vertex"/> to the <paramref name="edge"/>
        /// </summary>
        /// <param name="edge">The edge.</param>
        /// <param name="vertex">The vertex to connect to the edge.</param>
        /// <returns>True if succesful.</returns>
        bool ConnectVertex(IEdge edge, IVertex vertex);
    }
}