using Gremlin.Net.CosmosDb.Structure;
using Gremlin.Net.Process.Traversal;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Helper extension methods for <see cref="Gremlin.Net.Process.Traversal.GraphTraversal{S, E}"/> objects
    /// </summary>
    public static class GraphTraversalExtensions
    {
        private static readonly ConcurrentDictionary<Type, string> _labelLookup = new ConcurrentDictionary<Type, string>();

        /// <summary>
        /// Adds the "both()" step to the traversal, returning both adjacent vertices of the given edge
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="ToutVertex">The type of the out vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> Both<S, TVertex, ToutVertex>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<ToutVertex, TVertex>>> edgeSelector)
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return traversal.Both(labelName);
        }

        /// <summary>
        /// Adds the "both()" step to the traversal, returning both adjacent vertices of the given edge
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TinVertex">The type of the in vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> Both<S, TVertex, TinVertex>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<TVertex, TinVertex>>> edgeSelector)
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return traversal.Both(labelName);
        }

        /// <summary>
        /// Adds the "bothE()" step to the traversal, returning all adjacent edges of the given vertex
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Edge> BothE<S, TVertex>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, IEdgeOut<TVertex>>> edgeSelector)
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return traversal.BothE(labelName);
        }

        /// <summary>
        /// Adds the "bothE()" step to the traversal, returning all adjacent edges of the given vertex
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Edge> BothE<S, TVertex>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, IEdgeIn<TVertex>>> edgeSelector)
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return traversal.BothE(labelName);
        }

        /// <summary>
        /// Helper type-casting method that converts the given traversal to a new wrapped traversal type.
        /// </summary>
        /// <typeparam name="S">The source traversal type</typeparam>
        /// <typeparam name="E">The current traversal type</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the wrapped traversal</returns>
        public static GraphTraversal<S, E> Cast<S, E>(this ITraversal traversal)
        {
            return new GraphTraversal<S, E>(new ITraversalStrategy[0], traversal.Bytecode);
        }

        /// <summary>
        /// Adds the "in()" step to the traversal, returning all inbound adjacent vertices with the
        /// given label
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="ToutVertex">The type of the out vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, ToutVertex> In<S, TVertex, ToutVertex>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<ToutVertex, TVertex>>> edgeSelector)
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return traversal.In(labelName).Cast<S, ToutVertex>();
        }

        /// <summary>
        /// Adds the "in()" step to the traversal, returning all inbound adjacent vertices with the
        /// given label
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector1">The first edge selector.</param>
        /// <param name="edgeSelector2">The second edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> In<S, TVertex>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, IEdgeIn<TVertex>>> edgeSelector1, Expression<Func<TVertex, IEdgeIn<TVertex>>> edgeSelector2)
        {
            return traversal.In(GetLabelName(typeof(TVertex), edgeSelector1), GetLabelName(typeof(TVertex), edgeSelector2));
        }

        /// <summary>
        /// Adds the "in()" step to the traversal, returning all inbound adjacent vertices with the
        /// given label
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector1">The first edge selector.</param>
        /// <param name="edgeSelector2">The second edge selector.</param>
        /// <param name="edgeSelector3">The third edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> In<S, TVertex>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, IEdgeIn<TVertex>>> edgeSelector1, Expression<Func<TVertex, IEdgeIn<TVertex>>> edgeSelector2, Expression<Func<TVertex, IEdgeIn<TVertex>>> edgeSelector3)
        {
            return traversal.In(GetLabelName(typeof(TVertex), edgeSelector1), GetLabelName(typeof(TVertex), edgeSelector2), GetLabelName(typeof(TVertex), edgeSelector3));
        }

        /// <summary>
        /// Adds the "inE()" step to the traversal, returning all inbound adjacent edges with the
        /// given label
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, TEdge> InE<S, TVertex, TEdge>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, TEdge>> edgeSelector)
            where TEdge : IEdgeIn<TVertex>
        {
            var labelName = GetLabelName(typeof(TEdge));

            return traversal.InE(labelName).Cast<S, TEdge>();
        }

        /// <summary>
        /// Adds the "inE()" step to the traversal, returning all inbound adjacent edges with the
        /// given labels
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge1">The first type of edge.</typeparam>
        /// <typeparam name="TEdge2">The second type of edge.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector1">The first edge selector.</param>
        /// <param name="edgeSelector2">The second edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Edge> InE<S, TVertex, TEdge1, TEdge2>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, TEdge1>> edgeSelector1, Expression<Func<TVertex, TEdge2>> edgeSelector2)
            where TEdge1 : IEdgeIn<TVertex>
            where TEdge2 : IEdgeIn<TVertex>
        {
            return traversal.InE(GetLabelName(typeof(TEdge1)), GetLabelName(typeof(TEdge2)));
        }

        /// <summary>
        /// Adds the "inE()" step to the traversal, returning all inbound adjacent edges with the
        /// given labels
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge1">The first type of edge.</typeparam>
        /// <typeparam name="TEdge2">The second type of edge.</typeparam>
        /// <typeparam name="TEdge3">The third type of edge.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector1">The first edge selector.</param>
        /// <param name="edgeSelector2">The second edge selector.</param>
        /// <param name="edgeSelector3">The third edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Edge> InE<S, TVertex, TEdge1, TEdge2, TEdge3>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, TEdge1>> edgeSelector1, Expression<Func<TVertex, TEdge2>> edgeSelector2, Expression<Func<TVertex, TEdge3>> edgeSelector3)
            where TEdge1 : IEdgeIn<TVertex>
            where TEdge2 : IEdgeIn<TVertex>
            where TEdge3 : IEdgeIn<TVertex>
        {
            return traversal.InE(GetLabelName(typeof(TEdge1)), GetLabelName(typeof(TEdge2)), GetLabelName(typeof(TEdge3)));
        }

        /// <summary>
        /// Adds the "inV()" step to the traversal, returning all inbound adjacent vertices
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TinVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, TinVertex> InV<S, TinVertex>(this GraphTraversal<S, IEdgeIn<TinVertex>> traversal)
        {
            return traversal.InV().Cast<S, TinVertex>();
        }

        /// <summary>
        /// Adds the "out()" step to the traversal, returning all adjacent vertices connected via
        /// outbount edges
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TinVertex">The type of the "in"/destination vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, TinVertex> Out<S, TVertex, TinVertex>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<TVertex, TinVertex>>> edgeSelector)
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return traversal.Out(labelName).Cast<S, TinVertex>();
        }

        /// <summary>
        /// Adds the "out()" step to the traversal, returning all adjacent vertices connected via
        /// outbount edges
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TinVertex1">The first type of "in" vertex.</typeparam>
        /// <typeparam name="TinVertex2">The second type of "in" vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector1">The first edge selector.</param>
        /// <param name="edgeSelector2">The second edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> Out<S, TVertex, TinVertex1, TinVertex2>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<TVertex, TinVertex1>>> edgeSelector1, Expression<Func<TVertex, Edge<TVertex, TinVertex2>>> edgeSelector2)
        {
            return traversal.In(GetLabelName(typeof(TVertex), edgeSelector1), GetLabelName(typeof(TVertex), edgeSelector2));
        }

        /// <summary>
        /// Adds the "out()" step to the traversal, returning all adjacent vertices connected via
        /// outbount edges
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TinVertex1">The first type of "in" vertex.</typeparam>
        /// <typeparam name="TinVertex2">The second type of "in" vertex.</typeparam>
        /// <typeparam name="TinVertex3">The third type of "in" vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector1">The first edge selector.</param>
        /// <param name="edgeSelector2">The second edge selector.</param>
        /// <param name="edgeSelector3">The third edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> Out<S, TVertex, TinVertex1, TinVertex2, TinVertex3>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<TVertex, TinVertex1>>> edgeSelector1, Expression<Func<TVertex, Edge<TVertex, TinVertex2>>> edgeSelector2, Expression<Func<TVertex, Edge<TVertex, TinVertex3>>> edgeSelector3)
        {
            return traversal.In(GetLabelName(typeof(TVertex), edgeSelector1), GetLabelName(typeof(TVertex), edgeSelector2), GetLabelName(typeof(TVertex), edgeSelector3));
        }

        /// <summary>
        /// Adds the "outE()" step to the traversal, returning all adjacent outbound edges
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <typeparam name="TinVertex">The type of the "in"/destination vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, TEdge> OutE<S, TVertex, TEdge, TinVertex>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, TEdge>> edgeSelector)
            where TEdge : IEdgeOut<TVertex>
        {
            var labelName = GetLabelName(typeof(TEdge));

            return traversal.OutE(labelName).Cast<S, TEdge>();
        }

        /// <summary>
        /// Adds the "outE()" step to the traversal, returning all adjacent outbound edges
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge1">The first type of edge.</typeparam>
        /// <typeparam name="TEdge2">The second type of edge.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector1">The first edge selector.</param>
        /// <param name="edgeSelector2">The second edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Edge> OutE<S, TVertex, TEdge1, TEdge2>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, TEdge1>> edgeSelector1, Expression<Func<TVertex, TEdge2>> edgeSelector2)
            where TEdge1 : IEdgeIn<TVertex>
            where TEdge2 : IEdgeIn<TVertex>
        {
            return traversal.OutE(GetLabelName(typeof(TEdge1)), GetLabelName(typeof(TEdge2)));
        }

        /// <summary>
        /// Adds the "outE()" step to the traversal, returning all adjacent outbound edges
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge1">The first type of edge.</typeparam>
        /// <typeparam name="TEdge2">The second type of edge.</typeparam>
        /// <typeparam name="TEdge3">The third type of edge.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector1">The first edge selector.</param>
        /// <param name="edgeSelector2">The second edge selector.</param>
        /// <param name="edgeSelector3">The third edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Edge> OutE<S, TVertex, TEdge1, TEdge2, TEdge3>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, TEdge1>> edgeSelector1, Expression<Func<TVertex, TEdge2>> edgeSelector2, Expression<Func<TVertex, TEdge3>> edgeSelector3)
            where TEdge1 : IEdgeOut<TVertex>
            where TEdge2 : IEdgeOut<TVertex>
            where TEdge3 : IEdgeOut<TVertex>
        {
            return traversal.OutE(GetLabelName(typeof(TEdge1)), GetLabelName(typeof(TEdge2)), GetLabelName(typeof(TEdge3)));
        }

        /// <summary>
        /// Adds the "outV()" step to the traversal, returning all outbound adjacent vertices
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="ToutVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, ToutVertex> OutV<S, ToutVertex>(this GraphTraversal<S, IEdgeOut<ToutVertex>> traversal)
        {
            return traversal.InV().Cast<S, ToutVertex>();
        }

        /// <summary>
        /// Gets the name of the label.
        /// </summary>
        /// <param name="sourceType">Type of the source object from which the lambda started.</param>
        /// <param name="lambda">The lambda.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">lambda</exception>
        /// <exception cref="ArgumentException"></exception>
        private static string GetLabelName(Type sourceType, LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException(nameof(lambda));

            var member = lambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{lambda}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Expression '{lambda}' refers to a field, not a property.");

            if (sourceType != propInfo.ReflectedType && !sourceType.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException($"Expression '{lambda}' refers to a property that is not from type {sourceType}.");

            return GetLabelName(propInfo.PropertyType);
        }

        /// <summary>
        /// Gets the name of the label.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
        private static string GetLabelName(Type targetType)
        {
            return _labelLookup.GetOrAdd(targetType, t =>
            {
                var attr = targetType.GetCustomAttributes(typeof(LabelAttribute), false).OfType<LabelAttribute>().FirstOrDefault();

                return attr?.Name;
            });
        }
    }
}