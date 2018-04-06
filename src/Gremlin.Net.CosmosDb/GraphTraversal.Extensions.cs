using Gremlin.Net.CosmosDb.Structure;
using Gremlin.Net.Process.Traversal;
using System;
using System.Collections;
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
        private static readonly Type TYPE_OF_ELEMENT = typeof(Element<>);
        private static readonly Type TYPE_OF_ENUMERABLE = typeof(IEnumerable);

        /// <summary>
        /// Adds the "both()" step to the traversal, returning both adjacent vertices of the given edge
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="ToutVertex">The type of the out vertex.</typeparam>
        /// <typeparam name="TPropertiesModel">The type of the properties model.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> Both<S, TVertex, ToutVertex, TPropertiesModel>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<ToutVertex, TVertex, TPropertiesModel>>> edgeSelector)
            where TPropertiesModel : class
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
        /// <typeparam name="TPropertiesModel">The type of the properties model.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> Both<S, TVertex, TinVertex, TPropertiesModel>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<TVertex, TinVertex, TPropertiesModel>>> edgeSelector)
            where TPropertiesModel : class
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
        /// Adds the "has()" step to the traversal, removing traversers that do not have a value
        /// defined for the given property
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, TElement> Has<S, TElement, TProperty>(this GraphTraversal<S, TElement> traversal, Expression<Func<TElement, TProperty>> propertySelector)
        {
            var propName = GetPropertyName(typeof(TElement), propertySelector);

            return traversal.Has(propName);
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
        public static GraphTraversal<S, TElement> Has<S, TElement, TProperty>(this GraphTraversal<S, TElement> traversal, Expression<Func<TElement, TProperty>> propertySelector, TProperty value)
        {
            var propName = GetPropertyName(typeof(TElement), propertySelector);

            return traversal.Has(propName, value);
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
        public static GraphTraversal<S, TElement> Has<S, TElement, TProperty>(this GraphTraversal<S, TElement> traversal, Expression<Func<TElement, TProperty>> propertySelector, ITraversal propertyTraversal)
        {
            var propName = GetPropertyName(typeof(TElement), propertySelector);

            return traversal.Has(propName, propertyTraversal);
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
        public static GraphTraversal<S, TElement> Has<S, TElement, TProperty>(this GraphTraversal<S, TElement> traversal, Expression<Func<TElement, TProperty>> propertySelector, TraversalPredicate predicate)
        {
            var propName = GetPropertyName(typeof(TElement), propertySelector);

            return traversal.Has(propName, predicate);
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
        public static GraphTraversal<S, TElement> HasNot<S, TElement, TProperty>(this GraphTraversal<S, TElement> traversal, Expression<Func<TElement, TProperty>> propertySelector)
        {
            var propName = GetPropertyName(typeof(TElement), propertySelector);

            return traversal.HasNot(propName);
        }

        /// <summary>
        /// Adds the "in()" step to the traversal, returning all inbound adjacent vertices with the
        /// given label
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="ToutVertex">The type of the out vertex.</typeparam>
        /// <typeparam name="TPropertiesModel">The type of the properties model.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, ToutVertex> In<S, TVertex, ToutVertex, TPropertiesModel>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<ToutVertex, TVertex, TPropertiesModel>>> edgeSelector)
            where TPropertiesModel : class
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
        /// <typeparam name="TPropertiesModel">The type of the properties model.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, TinVertex> Out<S, TVertex, TinVertex, TPropertiesModel>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<TVertex, TinVertex, TPropertiesModel>>> edgeSelector)
            where TPropertiesModel : class
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
        /// <typeparam name="TPropertiesModel1">The type of the first properties model.</typeparam>
        /// <typeparam name="TinVertex2">The second type of "in" vertex.</typeparam>
        /// <typeparam name="TPropertiesModel2">The type of the second properties model.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector1">The first edge selector.</param>
        /// <param name="edgeSelector2">The second edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> Out<S, TVertex, TinVertex1, TPropertiesModel1, TinVertex2, TPropertiesModel2>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<TVertex, TinVertex1, TPropertiesModel1>>> edgeSelector1, Expression<Func<TVertex, Edge<TVertex, TinVertex2, TPropertiesModel2>>> edgeSelector2)
            where TPropertiesModel1 : class
            where TPropertiesModel2 : class
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
        /// <typeparam name="TPropertiesModel1">The type of the first properties model.</typeparam>
        /// <typeparam name="TinVertex2">The second type of "in" vertex.</typeparam>
        /// <typeparam name="TPropertiesModel2">The type of the second properties model.</typeparam>
        /// <typeparam name="TinVertex3">The third type of "in" vertex.</typeparam>
        /// <typeparam name="TPropertiesModel3">The type of the third properties model.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector1">The first edge selector.</param>
        /// <param name="edgeSelector2">The second edge selector.</param>
        /// <param name="edgeSelector3">The third edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> Out<S, TVertex, TinVertex1, TPropertiesModel1, TinVertex2, TPropertiesModel2, TinVertex3, TPropertiesModel3>(this GraphTraversal<S, TVertex> traversal, Expression<Func<TVertex, Edge<TVertex, TinVertex1, TPropertiesModel1>>> edgeSelector1, Expression<Func<TVertex, Edge<TVertex, TinVertex2, TPropertiesModel2>>> edgeSelector2, Expression<Func<TVertex, Edge<TVertex, TinVertex3, TPropertiesModel3>>> edgeSelector3)
            where TPropertiesModel1 : class
            where TPropertiesModel2 : class
            where TPropertiesModel3 : class
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
        /// Adds the "property()" step to the traversal, updating a property on an element
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, TElement> Property<S, TElement, TProperty>(this GraphTraversal<S, TElement> traversal, Expression<Func<TElement, TProperty>> propertySelector, TProperty value)
        {
            var propName = GetPropertyName(typeof(TElement), propertySelector);

            //if the property is an enumerable, we need to specify the cardinality to be list
            if (TYPE_OF_ENUMERABLE.IsAssignableFrom(typeof(TProperty)) && typeof(TProperty) != typeof(string))
            {
                var enumerator = ((IEnumerable)value).GetEnumerator();
                var result = traversal;
                while (enumerator.MoveNext())
                    result = result.Property(Cardinality.List, propName, enumerator.Current);
                return result;
            }
            else
            {
                return traversal.Property(propName, value);
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
            //first ensure that the element type is a true "Element<>"
            Type genericElementType = elementType;
            while (genericElementType != null)
            {
                genericElementType = genericElementType.BaseType;
                if (genericElementType.GetGenericTypeDefinition() == TYPE_OF_ELEMENT)
                    break;
            }
            if (genericElementType == null)
                throw new ArgumentException($"'{elementType}' does not inherit from '{TYPE_OF_ELEMENT}'.");

            var propInfo = GetPropertyInfo(lambda);

            var propertiesModelType = genericElementType.GetGenericArguments().Single();
            if (propertiesModelType != propInfo.ReflectedType && !propertiesModelType.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException($"Expression '{lambda}' refers to a property that is not from type {propertiesModelType}.");

            return propInfo.Name;
        }
    }
}