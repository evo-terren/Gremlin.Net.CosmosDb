using Gremlin.Net.CosmosDb.Structure;
using Gremlin.Net.Process.Traversal;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Gremlin.Net.CosmosDb
{
    public static class GraphTraversalExtensions
    {
        private static readonly ConcurrentDictionary<Type, string> _labelLookup = new ConcurrentDictionary<Type, string>();

        public static GraphTraversal<TVertex, TinVertex> In<S, TVertex, TinVertex>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<TinVertex, TVertex>>> edgeSelector)
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return WrapTraversal<TVertex, TinVertex>(traversal.In(labelName));
        }

        public static GraphTraversal<TVertex, ToutVertex> Out<S, TVertex, ToutVertex>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<TVertex, ToutVertex>>> edgeSelector)
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return WrapTraversal<TVertex, ToutVertex>(traversal.Out(labelName));
        }

        public static GraphTraversal<TVertex, TEdge> OutE<S, TVertex, TEdge>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, TEdge>> edgeSelector)
            where TEdge : IInVEdge<TVertex>
        {
            var labelName = GetLabelName(typeof(TEdge));

            return WrapTraversal<TVertex, TEdge>(traversal.OutE(labelName));
        }

        /// <summary>
        /// Gets the name of the label.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <returns></returns>
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

        private static GraphTraversal<S2, E2> WrapTraversal<S2, E2>(ITraversal traversal)
        {
            return new GraphTraversal<S2, E2>(new ITraversalStrategy[0], traversal.Bytecode);
        }
    }
}