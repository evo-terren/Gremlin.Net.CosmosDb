using Gremlin.Net.CosmosDb.Structure;
using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// A Cosmos Db graph traversal source (g)
    /// </summary>
    public interface IGraphTraversalSource
    {
        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and adds the addV step to
        /// that traversal.
        /// </summary>
        GraphTraversal<Gremlin.Net.Structure.Vertex, Gremlin.Net.Structure.Vertex> AddV();

        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and adds the addV step to
        /// that traversal.
        /// </summary>
        GraphTraversal<Gremlin.Net.Structure.Vertex, Gremlin.Net.Structure.Vertex> AddV(string label);

        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and adds the addV step to
        /// that traversal.
        /// </summary>
        GraphTraversal<Gremlin.Net.Structure.Vertex, Gremlin.Net.Structure.Vertex> AddV(ITraversal vertexLabelTraversal);

        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and adds the E step to
        /// that traversal.
        /// </summary>
        GraphTraversal<Gremlin.Net.Structure.Edge, Gremlin.Net.Structure.Edge> E(params object[] edgesIds);

        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and adds the V step to
        /// that traversal.
        /// </summary>
        GraphTraversal<Gremlin.Net.Structure.Vertex, Gremlin.Net.Structure.Vertex> V();

        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and adds the V step to
        /// that traversal.
        /// </summary>
        GraphTraversal<Gremlin.Net.Structure.Vertex, Gremlin.Net.Structure.Vertex> V(params object[] vertexIds);

        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and adds the V step to
        /// that traversal.
        /// </summary>
        GraphTraversal<Gremlin.Net.Structure.Vertex, Gremlin.Net.Structure.Vertex> V(params PartitionKeyIdPair[] vertexIds);
    }
}