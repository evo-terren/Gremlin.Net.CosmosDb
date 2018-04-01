using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.CosmosDb
{
    public static class GraphTraversalSourceExtensions
    {
        public static GraphTraversal<Gremlin.Net.Structure.Vertex, TVertex> V<TVertex>(this IGraphTraversalSource graphTraversalSource, params object[] vertexIds)
        {
            var traversal = graphTraversalSource.V(vertexIds);

            return new GraphTraversal<Net.Structure.Vertex, TVertex>(new ITraversalStrategy[0], traversal.Bytecode);
        }
    }
}