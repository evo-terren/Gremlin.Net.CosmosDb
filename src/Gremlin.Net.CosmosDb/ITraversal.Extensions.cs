using Gremlin.Net.CosmosDb.Serialization;
using Gremlin.Net.CosmosDb.Structure;
using Gremlin.Net.Process.Traversal;
using System.IO;
using System.Text;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// <see cref="Gremlin.Net.Process.Traversal.ITraversal"/> extension methods
    /// </summary>
    public static class ITraversalExtensions
    {
        /// <summary>
        /// Adds the "addE()" step to the traversal, creating a new edge in the graph.
        /// </summary>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<object, TEdge> AddE<TEdge>(this ITraversal traversal)
            where TEdge : EdgeBase
        {
            var label = LabelNameResolver.GetLabelName(typeof(TEdge));

            return traversal.ToGraphTraversal<object, object>().AddE(label).AsSchemaBound<object, TEdge>();
        }

        /// <summary>
        /// Adds the "addV()" step to the traversal, creating a new vertex in the graph.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<object, TVertex> AddV<TVertex>(this ITraversal traversal)
            where TVertex : VertexBase
        {
            var label = LabelNameResolver.GetLabelName(typeof(TVertex));

            return traversal.ToGraphTraversal<object, object>().AddV(label).AsSchemaBound<object, TVertex>();
        }

        /// <summary>
        /// Returns the schema-bound equivalent traversal for a edge.
        /// </summary>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the traversal</returns>
        public static ISchemaBoundTraversal<object, TEdge> AsEdge<TEdge>(this ITraversal traversal)
            where TEdge : EdgeBase
        {
            return traversal.AsSchemaBound<object, TEdge>();
        }

        /// <summary>
        /// Returns the schema-bound equivalent traversal for a vertex.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the traversal</returns>
        public static ISchemaBoundTraversal<object, TVertex> AsVertex<TVertex>(this ITraversal traversal)
            where TVertex : VertexBase
        {
            return traversal.AsSchemaBound<object, TVertex>();
        }

        /// <summary>
        /// Returns the string-equivalent of the given traversal
        /// </summary>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the string query</returns>
        public static string ToGremlinQuery(this ITraversal traversal)
        {
            var sb = new StringBuilder();
            using (var serializer = new GremlinQuerySerializer(new StringWriter(sb)))
            {
                serializer.Serialize(traversal);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts the given traversal to a schema-bound traversal equivalent.
        /// </summary>
        /// <typeparam name="S">The source type of the traversal</typeparam>
        /// <typeparam name="E">The element type of the current node</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the traversal</returns>
        internal static ISchemaBoundTraversal<S, E> AsSchemaBound<S, E>(this ITraversal traversal)
        {
            return new SchemaBoundTraversal<S, E>(traversal.Bytecode);
        }

        /// <summary>
        /// Converts the given traversal to a schema-bound traversal equivalent.
        /// </summary>
        /// <typeparam name="S">The source type of the traversal</typeparam>
        /// <typeparam name="E">The element type of the current node</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the traversal</returns>
        internal static ISchemaBoundTraversal<S, E> AsSchemaBound<S, E>(this ITraversal<S, E> traversal)
        {
            return new SchemaBoundTraversal<S, E>(traversal.Bytecode);
        }

        /// <summary>
        /// Casts the schema-bound traversal to a gremlin graph traversal.
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="E">The type of the current element</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the traversal</returns>
        internal static GraphTraversal<S, E> ToGraphTraversal<S, E>(this ITraversal traversal)
        {
            return new GraphTraversal<S, E>(new ITraversalStrategy[0], traversal.Bytecode);
        }
    }
}