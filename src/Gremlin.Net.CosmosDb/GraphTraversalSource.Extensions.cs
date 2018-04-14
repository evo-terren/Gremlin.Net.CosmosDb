using Gremlin.Net.CosmosDb.Structure;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Helper extension methods for <see cref="Gremlin.Net.Process.Traversal.GraphTraversalSource"/> objects
    /// </summary>
    public static class GraphTraversalSourceExtensions
    {
        /// <summary>
        /// Adds the "E()" step to the traversal.
        /// </summary>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="graphTraversalSource">The graph traversal source.</param>
        /// <param name="edgeIds">The edge id(s).</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<Edge, TEdge> E<TEdge>(this IGraphTraversalSource graphTraversalSource, params object[] edgeIds)
        {
            return graphTraversalSource.E(edgeIds).AsSchemaBound<Edge, TEdge>();
        }

        /// <summary>
        /// Adds the "V()" step to the traversal.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="graphTraversalSource">The graph traversal source.</param>
        /// <param name="vertexIds">The vertex id(s).</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<Vertex, TVertex> V<TVertex>(this IGraphTraversalSource graphTraversalSource, params object[] vertexIds)
        {
            return graphTraversalSource.V(vertexIds).AsSchemaBound<Vertex, TVertex>();
        }
    }
}