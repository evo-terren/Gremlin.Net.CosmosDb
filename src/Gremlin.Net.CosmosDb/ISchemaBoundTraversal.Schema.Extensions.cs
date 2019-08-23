using Gremlin.Net.CosmosDb.Structure;
using Gremlin.Net.Process.Traversal;
using System;
using System.Collections;
using System.Collections.Generic;
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
        /// <summary>
        /// Adds the "has()" step to the traversal, removing traversers that do not have a value defined for the given property
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

            return traversal.AsGraphTraversal().Has(propName).AsSchemaBound();
        }

        /// <summary>
        /// Adds the "has()" step to the traversal, removing traversers that do not have a specific value defined for the
        /// given property
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

            return traversal.AsGraphTraversal().Has(propName, value).AsSchemaBound();
        }

        /// <summary>
        /// Adds the "has()" step to the traversal, removing traversers that do not satisfy the given traversal for the
        /// given property
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

            return traversal.AsGraphTraversal().Has(propName, propertyTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the "has()" step to the traversal, removing traversers that do not satisfy the given predicate for the
        /// property selected
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

            return traversal.AsGraphTraversal().Has(propName, predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the "hasNot()" step to the traversal, removing traversers that define a value for the selected property
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

            return traversal.AsGraphTraversal().HasNot(propName).AsSchemaBound();
        }

        /// <summary>
        /// Adds the "in()" step to the traversal, returning all inbound adjacent vertices with the given label
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="ToutVertex">The type of the out vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, ToutVertex> In<S, TVertex, ToutVertex>(this ISchemaBoundTraversal<S, TVertex> traversal, Expression<Func<TVertex, IEdge<ToutVertex, TVertex>>> edgeSelector)
            where TVertex : IVertex
            where ToutVertex : IVertex
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return traversal.AsGraphTraversal().In(labelName).AsSchemaBound<S, ToutVertex>();
        }

        /// <summary>
        /// Adds the "in()" step to the traversal, returning all inbound adjacent vertices with the given label
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelectors">The edge selectors.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> In<S, TVertex>(this ISchemaBoundTraversal<S, TVertex> traversal, params Expression<Func<TVertex, IHasInV<TVertex>>>[] edgeSelectors)
            where TVertex : IVertex
        {
            if (edgeSelectors == null)
                throw new ArgumentNullException(nameof(edgeSelectors));

            var vertexType = typeof(TVertex);
            var edgeLabels = edgeSelectors.Select(es => GetLabelName(vertexType, es)).ToArray();

            return traversal.AsGraphTraversal().In(edgeLabels);
        }

        /// <summary>
        /// Adds the "inE()" step to the traversal, returning all inbound adjacent edges with the given label
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, TEdge> InE<S, TVertex, TEdge>(this ISchemaBoundTraversal<S, TVertex> traversal, Expression<Func<TVertex, TEdge>> edgeSelector)
            where TVertex : IVertex
            where TEdge : IHasInV<TVertex>
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return traversal.AsGraphTraversal().InE(labelName).AsSchemaBound<S, TEdge>();
        }

        /// <summary>
        /// Adds the "inE()" step to the traversal, returning all inbound adjacent edges with the given labels
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelectors">The edge selectors.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Edge> InE<S, TVertex>(this ISchemaBoundTraversal<S, TVertex> traversal, params Expression<Func<TVertex, IHasInV<TVertex>>>[] edgeSelectors)
            where TVertex : IVertex
        {
            if (edgeSelectors == null)
                throw new ArgumentNullException(nameof(edgeSelectors));

            var vertexType = typeof(TVertex);
            var edgeLabels = edgeSelectors.Select(es => GetLabelName(vertexType, es)).ToArray();

            return traversal.AsGraphTraversal().InE(edgeLabels);
        }

        /// <summary>
        /// Adds the "inV()" step to the traversal, returning all inbound adjacent vertices
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TinVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, TinVertex> InV<S, TinVertex>(this ISchemaBoundTraversal<S, IHasInV<TinVertex>> traversal)
        where TinVertex : IVertex
        {
            return traversal.AsGraphTraversal().InV().AsSchemaBound<S, TinVertex>();
        }

        /// <summary>
        /// Adds the "out()" step to the traversal, returning all adjacent vertices connected via outbount edges
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <typeparam name="TinVertex">The type of the "in"/destination vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelector">The edge selector.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, TinVertex> Out<S, TVertex, TinVertex>(this ISchemaBoundTraversal<S, TVertex> traversal, Expression<Func<TVertex, IEdge<TVertex, TinVertex>>> edgeSelector)
            where TVertex : IVertex
            where TinVertex : IVertex
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return traversal.AsGraphTraversal().Out(labelName).AsSchemaBound<S, TinVertex>();
        }

        /// <summary>
        /// Adds the "out()" step to the traversal, returning all adjacent vertices connected via outbount edges
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelectors">The edge selectors.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Vertex> Out<S, TVertex>(this ISchemaBoundTraversal<S, TVertex> traversal, params Expression<Func<TVertex, IHasOutV<TVertex>>>[] edgeSelectors)
            where TVertex : IVertex
        {
            if (edgeSelectors == null)
                throw new ArgumentNullException(nameof(edgeSelectors));

            var vertexType = typeof(TVertex);
            var edgeLabels = edgeSelectors.Select(es => GetLabelName(vertexType, es)).ToArray();

            return traversal.AsGraphTraversal().Out(edgeLabels);
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
            where TVertex : IVertex
            where TEdge : IHasOutV<TVertex>
        {
            var labelName = GetLabelName(typeof(TVertex), edgeSelector);

            return traversal.AsGraphTraversal().OutE(labelName).AsSchemaBound<S, TEdge>();
        }

        /// <summary>
        /// Adds the "outE()" step to the traversal, returning all adjacent outbound edges
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="edgeSelectors">The edge selectors.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, Gremlin.Net.Structure.Edge> OutE<S, TVertex>(this ISchemaBoundTraversal<S, TVertex> traversal, params Expression<Func<TVertex, IHasOutV<TVertex>>>[] edgeSelectors)
            where TVertex : IVertex
        {
            if (edgeSelectors == null)
                throw new ArgumentNullException(nameof(edgeSelectors));

            var vertexType = typeof(TVertex);
            var edgeLabels = edgeSelectors.Select(es => GetLabelName(vertexType, es)).ToArray();

            return traversal.AsGraphTraversal().OutE(edgeLabels);
        }

        /// <summary>
        /// Adds the "outV()" step to the traversal, returning all outbound adjacent vertices
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="ToutVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, ToutVertex> OutV<S, ToutVertex>(this ISchemaBoundTraversal<S, IHasOutV<ToutVertex>> traversal)
            where ToutVertex : IVertex
        {
            return traversal.AsGraphTraversal().OutV().AsSchemaBound<S, ToutVertex>();
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
            var propType = typeof(TProperty);
            var graphTraversal = traversal.AsGraphTraversal();

            //special case for null value - drop the property via side effect
            if (value == null)
            {
                graphTraversal = graphTraversal.SideEffect(__.Properties<TElement>(propName).Drop());
            }
            //if the property is an enumerable, use sideEffect() to drop existing values before adding the new ones
            //also, strings need to be special cased since most people don't think of strings as an enumerable of chars
            else if (!TypeHelper.IsScalar(propType))
            {
                var enumerator = ((IEnumerable)value).GetEnumerator();
                graphTraversal = graphTraversal.SideEffect(__.Properties<TElement>(propName).Drop());
                while (enumerator.MoveNext())
                    graphTraversal = graphTraversal.Property(Cardinality.List, propName, enumerator.Current);
            }
            else
            {
                graphTraversal = graphTraversal.Property(propName, value);
            }

            return graphTraversal.AsSchemaBound();
        }

        /// <summary>
        /// Adds the "property()" step to the traversal, updating an enumerable property on an element
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TValue">The type of the value being added</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, TElement> Property<S, TElement, TValue>(this ISchemaBoundTraversal<S, TElement> traversal, Expression<Func<TElement, IEnumerable<TValue>>> propertySelector, IEnumerable<TValue> value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var propName = GetPropertyName(typeof(TElement), propertySelector);
            var graphTraversal = traversal.AsGraphTraversal();

            //special case for null value - drop the property via side effect
            if (value == null)
            {
                graphTraversal = graphTraversal.SideEffect(__.Properties<TElement>(propName).Drop());
            }
            //special case for strings - most people don't think of strings as an array of chars
            //so, don't treat them as enumerable properties
            else if (value.GetType() == TypeCache.String)
            {
                graphTraversal = graphTraversal.Property(propName, value);
            }
            else
            {
                //use sideEffect() to drop any existing value before adding new ones
                graphTraversal = graphTraversal.SideEffect(__.Properties<TElement>(propName).Drop());
                foreach (var val in value)
                    graphTraversal = graphTraversal.Property(Cardinality.List, propName, val);
            }

            return graphTraversal.AsSchemaBound();
        }

        /// <summary>
        /// Adds the "property()" step to the traversal, adding a new value to an enumerable property list
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TValue">The type of the value being added</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<S, TElement> Property<S, TElement, TValue>(this ISchemaBoundTraversal<S, TElement> traversal, Expression<Func<TElement, IEnumerable<TValue>>> propertySelector, TValue value)
        {
            var propName = GetPropertyName(typeof(TElement), propertySelector);

            return traversal.AsGraphTraversal().Property(Cardinality.List, propName, value).AsSchemaBound();
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

            return LabelNameResolver.GetLabelName(propInfo);
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

            if (propInfo.GetCustomAttribute<Newtonsoft.Json.JsonPropertyAttribute>() is var jsonProperty)
            {
              if (!String.IsNullOrWhiteSpace(jsonProperty.PropertyName))
                return jsonProperty.PropertyName;
            }

            return propInfo.Name;
        }
    }
}
