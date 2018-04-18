using Gremlin.Net.CosmosDb.Structure;
using Gremlin.Net.Process.Traversal;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Helper extension methods for <see cref="Gremlin.Net.CosmosDb.ISchemaBoundTraversal{S, E}"/> objects
    /// </summary>
    public static partial class ISchemaBoundTraversalExtensions
    {
        private static readonly Type TYPE_OF_ENUMERABLE = typeof(IEnumerable);

        /// <summary>
        /// Adds the "addE()" step to the traversal, creating a new edge in the graph.
        /// </summary>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<object, TEdge> AddE<TEdge>(this ISchemaBoundTraversal traversal)
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
        public static ISchemaBoundTraversal<object, TVertex> AddV<TVertex>(this ISchemaBoundTraversal traversal)
            where TVertex : VertexBase
        {
            var label = LabelNameResolver.GetLabelName(typeof(TVertex));

            return traversal.ToGraphTraversal<object, object>().AddV(label).AsSchemaBound<object, TVertex>();
        }

        /// <summary>
        /// Adds the "has()" step to the traversal, removing traversers that do not have a value
        /// defined for the given property
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, TElement> Has<S, TElement, TProperty>(this ISchemaBoundTraversal<S, TElement> traversal, Expression<Func<TElement, TProperty>> propertySelector)
        {
            var propName = GetPropertyName(typeof(TElement), propertySelector);

            return traversal.ToGraphTraversal().Has(propName).AsSchemaBound();
        }

        /// <summary>
        /// Adds the "has()" step to the traversal, removing traversers that do not have a specific
        /// value defined for the given property
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="value">The value to compare.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, TElement> Has<S, TElement, TProperty>(this ISchemaBoundTraversal<S, TElement> traversal, Expression<Func<TElement, TProperty>> propertySelector, TProperty value)
        {
            var propName = GetPropertyName(typeof(TElement), propertySelector);

            return traversal.ToGraphTraversal().Has(propName, value).AsSchemaBound();
        }

        /// <summary>
        /// Adds the "has()" step to the traversal, removing traversers that do not satisfy the given
        /// traversal for the given property
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="propertyTraversal">The value to compare.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, TElement> Has<S, TElement, TProperty>(this ISchemaBoundTraversal<S, TElement> traversal, Expression<Func<TElement, TProperty>> propertySelector, ITraversal propertyTraversal)
        {
            var propName = GetPropertyName(typeof(TElement), propertySelector);

            return traversal.ToGraphTraversal().Has(propName, propertyTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the "has()" step to the traversal, removing traversers that do not satisfy the given
        /// predicate for the property selected
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="predicate">The value predicate to test.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, TElement> Has<S, TElement, TProperty>(this ISchemaBoundTraversal<S, TElement> traversal, Expression<Func<TElement, TProperty>> propertySelector, P predicate)
        {
            var propName = GetPropertyName(typeof(TElement), propertySelector);

            return traversal.ToGraphTraversal().Has(propName, predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the "hasNot()" step to the traversal, removing traversers that define a value for
        /// the selected property
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, TElement> HasNot<S, TElement, TProperty>(this ISchemaBoundTraversal<S, TElement> traversal, Expression<Func<TElement, TProperty>> propertySelector)
        {
            var propName = GetPropertyName(typeof(TElement), propertySelector);

            return traversal.ToGraphTraversal().HasNot(propName).AsSchemaBound();
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
        public static ISchemaBoundTraversal<S, ToutVertex> In<S, TVertex, ToutVertex>(this ISchemaBoundTraversal<S, TVertex> traversal, Expression<Func<TVertex, EdgeBase<ToutVertex, TVertex>>> edgeSelector)
            where TVertex : VertexBase
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return traversal.ToGraphTraversal().In(labelName).AsSchemaBound<S, ToutVertex>();
        }

        /// <summary>
        /// Adds the "in()" step to the traversal, returning all inbound adjacent vertices with the
        /// given label
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelectors">The edge selectors.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> In<S, TVertex>(this ISchemaBoundTraversal<S, TVertex> traversal, params Expression<Func<TVertex, IHasInVertex<TVertex>>>[] edgeSelectors)
            where TVertex : VertexBase
        {
            if (edgeSelectors == null)
                throw new ArgumentNullException(nameof(edgeSelectors));

            var vertexType = typeof(TVertex);
            var edgeLabels = edgeSelectors.Select(es => GetLabelName(vertexType, es)).ToArray();

            return traversal.ToGraphTraversal().In(edgeLabels);
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
        public static ISchemaBoundTraversal<S, TEdge> InE<S, TVertex, TEdge>(this ISchemaBoundTraversal<S, TVertex> traversal, Expression<Func<TVertex, TEdge>> edgeSelector)
            where TVertex : VertexBase
            where TEdge : IHasInVertex<TVertex>
        {
            var labelName = LabelNameResolver.GetLabelName(typeof(TEdge));

            return traversal.ToGraphTraversal().InE(labelName).AsSchemaBound<S, TEdge>();
        }

        /// <summary>
        /// Adds the "inE()" step to the traversal, returning all inbound adjacent edges with the
        /// given labels
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelectors">The edge selectors.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Edge> InE<S, TVertex>(this ISchemaBoundTraversal<S, TVertex> traversal, params Expression<Func<TVertex, IHasInVertex<TVertex>>>[] edgeSelectors)
            where TVertex : VertexBase
        {
            if (edgeSelectors == null)
                throw new ArgumentNullException(nameof(edgeSelectors));

            var vertexType = typeof(TVertex);
            var edgeLabels = edgeSelectors.Select(es => GetLabelName(vertexType, es)).ToArray();

            return traversal.ToGraphTraversal().InE(edgeLabels);
        }

        /// <summary>
        /// Adds the "inV()" step to the traversal, returning all inbound adjacent vertices
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TinVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, TinVertex> InV<S, TinVertex>(this ISchemaBoundTraversal<S, IHasInVertex<TinVertex>> traversal)
        {
            return traversal.ToGraphTraversal().InV().AsSchemaBound<S, TinVertex>();
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
        public static ISchemaBoundTraversal<S, TinVertex> Out<S, TVertex, TinVertex>(this ISchemaBoundTraversal<S, TVertex> traversal, Expression<Func<TVertex, EdgeBase<TVertex, TinVertex>>> edgeSelector)
            where TVertex : VertexBase
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return traversal.ToGraphTraversal().Out(labelName).AsSchemaBound<S, TinVertex>();
        }

        /// <summary>
        /// Adds the "out()" step to the traversal, returning all adjacent vertices connected via
        /// outbount edges
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelectors">The edge selectors.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> Out<S, TVertex>(this ISchemaBoundTraversal<S, TVertex> traversal, params Expression<Func<TVertex, IHasOutVertex<TVertex>>>[] edgeSelectors)
            where TVertex : VertexBase
        {
            if (edgeSelectors == null)
                throw new ArgumentNullException(nameof(edgeSelectors));

            var vertexType = typeof(TVertex);
            var edgeLabels = edgeSelectors.Select(es => GetLabelName(vertexType, es)).ToArray();

            return traversal.ToGraphTraversal().In(edgeLabels);
        }

        /// <summary>
        /// Adds the "outE()" step to the traversal, returning all adjacent outbound edges
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, TEdge> OutE<S, TVertex, TEdge>(this ISchemaBoundTraversal<S, TVertex> traversal, Expression<Func<TVertex, TEdge>> edgeSelector)
            where TVertex : VertexBase
            where TEdge : IHasOutVertex<TVertex>
        {
            var labelName = LabelNameResolver.GetLabelName(typeof(TEdge));

            return traversal.ToGraphTraversal().OutE(labelName).AsSchemaBound<S, TEdge>();
        }

        /// <summary>
        /// Adds the "outE()" step to the traversal, returning all adjacent outbound edges
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelectors">The edge selectors.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Edge> OutE<S, TVertex>(this ISchemaBoundTraversal<S, TVertex> traversal, params Expression<Func<TVertex, IHasOutVertex<TVertex>>>[] edgeSelectors)
            where TVertex : VertexBase
        {
            if (edgeSelectors == null)
                throw new ArgumentNullException(nameof(edgeSelectors));

            var vertexType = typeof(TVertex);
            var edgeLabels = edgeSelectors.Select(es => GetLabelName(vertexType, es)).ToArray();

            return traversal.ToGraphTraversal().OutE(edgeLabels);
        }

        /// <summary>
        /// Adds the "outV()" step to the traversal, returning all outbound adjacent vertices
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="ToutVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, ToutVertex> OutV<S, ToutVertex>(this ISchemaBoundTraversal<S, IHasOutVertex<ToutVertex>> traversal)
        {
            return traversal.ToGraphTraversal().InV().AsSchemaBound<S, ToutVertex>();
        }

        /// <summary>
        /// Adds the "property()" step to the traversal, updating a property on an element
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, TElement> Property<S, TElement, TProperty>(this ISchemaBoundTraversal<S, TElement> traversal, Expression<Func<TElement, TProperty>> propertySelector, TProperty value)
        {
            var propName = GetPropertyName(typeof(TElement), propertySelector);

            //if the property is an enumerable, we need to specify the cardinality to be list
            if (TYPE_OF_ENUMERABLE.IsAssignableFrom(typeof(TProperty)) && typeof(TProperty) != typeof(string))
            {
                var enumerator = ((IEnumerable)value).GetEnumerator();
                var result = traversal;
                while (enumerator.MoveNext())
                    result = result.ToGraphTraversal().Property(Cardinality.List, propName, enumerator.Current).AsSchemaBound();
                return result;
            }
            else
            {
                return traversal.ToGraphTraversal().Property(propName, value).AsSchemaBound();
            }
        }

        /// <summary>
        /// Gets the name of the label of the property's type.
        /// </summary>
        /// <param name="sourceType">Type of the source object from which the lambda started.</param>
        /// <param name="lambda">The lambda.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">lambda</exception>
        /// <exception cref="ArgumentException"></exception>
        private static string GetLabelName(Type sourceType, LambdaExpression lambda)
        {
            var propInfo = GetPropertyInfo(lambda);

            if (sourceType != propInfo.ReflectedType && !sourceType.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException($"Expression '{lambda}' refers to a property that is not from type {sourceType}.");

            return LabelNameResolver.GetLabelName(propInfo.PropertyType);
        }

        /// <summary>
        /// Gets the property information from the given lambda expression.
        /// </summary>
        /// <param name="lambda">The lambda.</param>
        /// <returns>Returns the property info being selected</returns>
        /// <exception cref="ArgumentNullException">lambda</exception>
        /// <exception cref="ArgumentException"></exception>
        private static PropertyInfo GetPropertyInfo(LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException(nameof(lambda));

            var member = lambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{lambda}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Expression '{lambda}' refers to a field, not a property.");

            return propInfo;
        }

        /// <summary>
        /// Gets the name of the property the lambda expression is selecting.
        /// </summary>
        /// <param name="elementType">Type of the source.</param>
        /// <param name="lambda">The lambda.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">lambda</exception>
        /// <exception cref="ArgumentException"></exception>
        private static string GetPropertyName(Type elementType, LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException(nameof(lambda));

            var propInfo = GetPropertyInfo(lambda);

            if (elementType != propInfo.ReflectedType && !elementType.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException($"Expression '{lambda}' refers to a property that is not from type {elementType}.");

            return propInfo.Name;
        }
    }
}