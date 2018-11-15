using Gremlin.Net.CosmosDb.Serialization;
using Gremlin.Net.CosmosDb.Structure;
using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            where TEdge : IEdge
        {
            var label = LabelNameResolver.GetLabelName(typeof(TEdge));

            return traversal.AsGraphTraversal().AddE(label).AsSchemaBound<object, TEdge>();
        }

        /// <summary>
        /// Adds the "addV()" step to the traversal, creating a new vertex in the graph.
        /// </summary>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="traversal">The graph traversal.</param>
        /// <param name="edge">The edge to add.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<object, TEdge> AddE<TEdge>(this ITraversal traversal, TEdge edge)
            where TEdge : IEdge
        {
            return AddE(traversal, edge, new JsonSerializerSettings { ContractResolver = new ElementContractResolver() });
        }

        /// <summary>
        /// Adds the "addV()" step to the traversal, creating a new vertex in the graph.
        /// </summary>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="traversal">The graph traversal.</param>
        /// <param name="edge">The edge to add.</param>
        /// <param name="serializationSettings">The serialization settings.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<object, TEdge> AddE<TEdge>(this ITraversal traversal, TEdge edge, JsonSerializerSettings serializationSettings)
            where TEdge : IEdge
        {
            var label = LabelNameResolver.GetLabelName(typeof(TEdge));
            var t = traversal.AsGraphTraversal().AddE(label);

            t = TraversalHelper.AddObjectProperties(t, edge, serializationSettings);

            return t.AsSchemaBound<object, TEdge>();
        }

        /// <summary>
        /// Adds the "addV()" step to the traversal, creating a new vertex in the graph.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<object, TVertex> AddV<TVertex>(this ITraversal traversal)
            where TVertex : IVertex
        {
            var label = LabelNameResolver.GetLabelName(typeof(TVertex));

            return traversal.AsGraphTraversal().AddV(label).AsSchemaBound<object, TVertex>();
        }

        /// <summary>
        /// Adds the "addV()" step to the traversal, creating a new vertex in the graph.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The graph traversal.</param>
        /// <param name="vertex">The vertex to add.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<object, TVertex> AddV<TVertex>(this ITraversal traversal, TVertex vertex)
            where TVertex : IVertex
        {
            return AddV(traversal, vertex, new JsonSerializerSettings { ContractResolver = new ElementContractResolver() });
        }

        /// <summary>
        /// Adds the "addV()" step to the traversal, creating a new vertex in the graph.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The graph traversal.</param>
        /// <param name="vertex">The vertex to add.</param>
        /// <param name="serializationSettings">The serialization settings.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static ISchemaBoundTraversal<object, TVertex> AddV<TVertex>(this ITraversal traversal, TVertex vertex, JsonSerializerSettings serializationSettings)
            where TVertex : IVertex
        {
            var label = LabelNameResolver.GetLabelName(typeof(TVertex));
            var t = traversal.AsGraphTraversal().AddV(label);

            t = TraversalHelper.AddObjectProperties(t, vertex, serializationSettings);

            return t.AsSchemaBound<object, TVertex>();
        }

        /// <summary>
        /// Returns the schema-bound equivalent traversal for a edge.
        /// </summary>
        /// <typeparam name="TEdge">The type of the edge.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the traversal</returns>
        [Obsolete("Use Cast<T>() instead")]
        public static ISchemaBoundTraversal<object, TEdge> AsEdge<TEdge>(this ITraversal traversal)
            where TEdge : IEdge
        {
            return traversal.AsSchemaBound<object, TEdge>();
        }

        /// <summary>
        /// Returns the schema-bound equivalent traversal for a vertex.
        /// </summary>
        /// <typeparam name="TVertex">The type of the vertex.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the traversal</returns>
        [Obsolete("Use Cast<T>() instead")]
        public static ISchemaBoundTraversal<object, TVertex> AsVertex<TVertex>(this ITraversal traversal)
            where TVertex : IVertex
        {
            return traversal.AsSchemaBound<object, TVertex>();
        }

        /// <summary>
        /// Adds the branch step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Branch<E2>(this ITraversal traversal, IFunction function)
        {
            return traversal.AsGraphTraversal<object, E2>().Branch<E2>(function);
        }

        /// <summary>
        /// Adds the branch step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Branch<E2>(this ITraversal traversal, ITraversal branchTraversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Branch<E2>(branchTraversal);
        }

        /// <summary>
        /// Adds the cap step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Cap<E2>(this ITraversal traversal, string sideEffectKey, params string[] sideEffectKeys)
        {
            return traversal.AsGraphTraversal<object, E2>().Cap<E2>(sideEffectKey, sideEffectKeys);
        }

        /// <summary>
        /// Returns the schema-bound equivalent traversal for a specific type.
        /// </summary>
        /// <typeparam name="T">The type to cast the traversal to.</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the traversal</returns>
        public static ISchemaBoundTraversal<object, T> Cast<T>(this ITraversal traversal)
        {
            return traversal.AsSchemaBound<object, T>();
        }

        /// <summary>
        /// Adds the choose step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Choose<E2>(this ITraversal traversal, IFunction choiceFunction)
        {
            return traversal.AsGraphTraversal<object, E2>().Choose<E2>(choiceFunction);
        }

        /// <summary>
        /// Adds the choose step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Choose<E2>(this ITraversal traversal, P choosePredicate, ITraversal trueChoice)
        {
            return traversal.AsGraphTraversal<object, E2>().Choose<E2>(choosePredicate, trueChoice);
        }

        /// <summary>
        /// Adds the choose step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Choose<E2>(this ITraversal traversal, P choosePredicate, ITraversal trueChoice, ITraversal falseChoice)
        {
            return traversal.AsGraphTraversal<object, E2>().Choose<E2>(choosePredicate, trueChoice, falseChoice);
        }

        /// <summary>
        /// Adds the choose step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Choose<E2>(this ITraversal traversal, ITraversal choiceTraversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Choose<E2>(choiceTraversal);
        }

        /// <summary>
        /// Adds the choose step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Choose<E2>(this ITraversal traversal, ITraversal traversalPredicate, ITraversal trueChoice)
        {
            return traversal.AsGraphTraversal<object, E2>().Choose<E2>(traversalPredicate, trueChoice);
        }

        /// <summary>
        /// Adds the choose step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Choose<E2>(this ITraversal traversal, ITraversal traversalPredicate, ITraversal trueChoice, ITraversal falseChoice)
        {
            return traversal.AsGraphTraversal<object, E2>().Choose<E2>(traversalPredicate, trueChoice, falseChoice);
        }

        /// <summary>
        /// Adds the coalesce step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Coalesce<E2>(this ITraversal traversal, params ITraversal[] coalesceTraversals)
        {
            return traversal.AsGraphTraversal<object, E2>().Coalesce<E2>(coalesceTraversals);
        }

        /// <summary>
        /// Adds the constant step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Constant<E2>(this ITraversal traversal, E2 e)
        {
            return traversal.AsGraphTraversal<object, E2>().Constant<E2>(e);
        }

        /// <summary>
        /// Adds the flatMap step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> FlatMap<E2>(this ITraversal traversal, IFunction function)
        {
            return traversal.AsGraphTraversal<object, E2>().FlatMap<E2>(function);
        }

        /// <summary>
        /// Adds the flatMap step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> FlatMap<E2>(this ITraversal traversal, ITraversal flatMapTraversal)
        {
            return traversal.AsGraphTraversal<object, E2>().FlatMap<E2>(flatMapTraversal);
        }

        /// <summary>
        /// Adds the fold step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Fold<E2>(this ITraversal traversal, E2 seed, IBiFunction foldFunction)
        {
            return traversal.AsGraphTraversal<object, E2>().Fold(seed, foldFunction);
        }

        /// <summary>
        /// Adds the group step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<K, V>> Group<K, V>(this ITraversal traversal)
        {
            return traversal.AsGraphTraversal<object, IDictionary<K, V>>().Group<K, V>();
        }

        /// <summary>
        /// Adds the groupCount step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<K, long>> GroupCount<K>(this ITraversal traversal)
        {
            return traversal.AsGraphTraversal<object, IDictionary<K, long>>().GroupCount<K>();
        }

        /// <summary>
        /// Adds the local step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Local<E2>(this ITraversal traversal, ITraversal localTraversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Local<E2>(localTraversal);
        }

        /// <summary>
        /// Adds the map step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Map<E2>(this ITraversal traversal, IFunction function)
        {
            return traversal.AsGraphTraversal<object, E2>().Map<E2>(function);
        }

        /// <summary>
        /// Adds the map step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Map<E2>(this ITraversal traversal, ITraversal mapTraversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Map<E2>(mapTraversal);
        }

        /// <summary>
        /// Adds the match step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<string, E2>> Match<E2>(this ITraversal traversal, params ITraversal[] matchTraversals)
        {
            return traversal.AsGraphTraversal<object, E2>().Match<E2>(matchTraversals);
        }

        /// <summary>
        /// Adds the max step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Max<E2>(this ITraversal traversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Max<E2>();
        }

        /// <summary>
        /// Adds the max step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Max<E2>(this ITraversal traversal, Scope scope)
        {
            return traversal.AsGraphTraversal<object, E2>().Max<E2>(scope);
        }

        /// <summary>
        /// Adds the mean step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Mean<E2>(this ITraversal traversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Mean<E2>();
        }

        /// <summary>
        /// Adds the mean step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Mean<E2>(this ITraversal traversal, Scope scope)
        {
            return traversal.AsGraphTraversal<object, E2>().Mean<E2>(scope);
        }

        /// <summary>
        /// Adds the min step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Min<E2>(this ITraversal traversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Min<E2>();
        }

        /// <summary>
        /// Adds the min step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Min<E2>(this ITraversal traversal, Scope scope)
        {
            return traversal.AsGraphTraversal<object, E2>().Min<E2>(scope);
        }

        /// <summary>
        /// Adds the optional step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Optional<E2>(this ITraversal traversal, ITraversal optionalTraversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Optional<E2>(optionalTraversal);
        }

        /// <summary>
        /// Adds the profile step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Profile<E2>(this ITraversal traversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Profile<E2>();
        }

        /// <summary>
        /// Adds the project step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<string, E2>> Project<E2>(this ITraversal traversal, string projectKey, params string[] otherProjectKeys)
        {
            return traversal.AsGraphTraversal<object, E2>().Project<E2>(projectKey, otherProjectKeys);
        }

        /// <summary>
        /// Adds the properties step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Properties<E2>(this ITraversal traversal, params string[] propertyKeys)
        {
            return traversal.AsGraphTraversal<object, E2>().Properties<E2>(propertyKeys);
        }

        /// <summary>
        /// Adds the propertyMap step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<string, E2>> PropertyMap<E2>(this ITraversal traversal, params string[] propertyKeys)
        {
            return traversal.AsGraphTraversal<object, E2>().PropertyMap<E2>(propertyKeys);
        }

        /// <summary>
        /// Adds the sack step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Sack<E2>(this ITraversal traversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Sack<E2>();
        }

        /// <summary>
        /// Adds the select step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, ICollection<E2>> Select<E2>(this ITraversal traversal, Column column)
        {
            return traversal.AsGraphTraversal<object, E2>().Select<E2>(column);
        }

        /// <summary>
        /// Adds the select step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Select<E2>(this ITraversal traversal, Pop pop, string selectKey)
        {
            return traversal.AsGraphTraversal<object, E2>().Select<E2>(pop, selectKey);
        }

        /// <summary>
        /// Adds the select step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<string, E2>> Select<E2>(this ITraversal traversal, Pop pop, string selectKey1, string selectKey2, params string[] otherSelectKeys)
        {
            return traversal.AsGraphTraversal<object, E2>().Select<E2>(pop, selectKey1, selectKey2, otherSelectKeys);
        }

        /// <summary>
        /// Adds the select step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Select<E2>(this ITraversal traversal, string selectKey)
        {
            return traversal.AsGraphTraversal<object, E2>().Select<E2>(selectKey);
        }

        /// <summary>
        /// Adds the select step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<string, E2>> Select<E2>(this ITraversal traversal, string selectKey1, string selectKey2, params string[] otherSelectKeys)
        {
            return traversal.AsGraphTraversal<object, E2>().Select<E2>(selectKey1, selectKey2, otherSelectKeys);
        }

        /// <summary>
        /// Adds the sum step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Sum<E2>(this ITraversal traversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Sum<E2>();
        }

        /// <summary>
        /// Adds the sum step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Sum<E2>(this ITraversal traversal, Scope scope)
        {
            return traversal.AsGraphTraversal<object, E2>().Sum<E2>(scope);
        }

        /// <summary>
        /// Adds the tail step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Tail<E2>(this ITraversal traversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Tail<E2>();
        }

        /// <summary>
        /// Adds the tail step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Tail<E2>(this ITraversal traversal, Scope scope)
        {
            return traversal.AsGraphTraversal<object, E2>().Tail<E2>(scope);
        }

        /// <summary>
        /// Adds the tail step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Tail<E2>(this ITraversal traversal, Scope scope, long limit)
        {
            return traversal.AsGraphTraversal<object, E2>().Tail<E2>(scope, limit);
        }

        /// <summary>
        /// Adds the tail step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Tail<E2>(this ITraversal traversal, long limit)
        {
            return traversal.AsGraphTraversal<object, E2>().Tail<E2>(limit);
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
        /// Adds the tree step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Tree<E2>(this ITraversal traversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Tree<E2>();
        }

        /// <summary>
        /// Adds the unfold step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Unfold<E2>(this ITraversal traversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Unfold<E2>();
        }

        /// <summary>
        /// Adds the union step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Union<E2>(this ITraversal traversal, params ITraversal[] unionTraversals)
        {
            return traversal.AsGraphTraversal<object, E2>().Union<E2>(unionTraversals);
        }

        /// <summary>
        /// Adds the value step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Value<E2>(this ITraversal traversal)
        {
            return traversal.AsGraphTraversal<object, E2>().Value<E2>();
        }

        /// <summary>
        /// Adds the valueMap step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<TKey, TValue>> ValueMap<TKey, TValue>(this ITraversal traversal, params string[] propertyKeys)
        {
            return traversal.AsGraphTraversal<object, object>().ValueMap<TKey, TValue>(propertyKeys);
        }

        /// <summary>
        /// Adds the valueMap step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<TKey, TValue>> ValueMap<TKey, TValue>(this ITraversal traversal, bool includeTokens, params string[] propertyKeys)
        {
            return traversal.AsGraphTraversal<object, object>().ValueMap<TKey, TValue>(includeTokens, propertyKeys);
        }

        /// <summary>
        /// Adds the values step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Values<E2>(this ITraversal traversal, params string[] propertyKeys)
        {
            return traversal.AsGraphTraversal<object, E2>().Values<E2>(propertyKeys);
        }

        /// <summary>
        /// Casts the traversal to a gremlin graph traversal.
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="E">The type of the current element</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the traversal</returns>
        internal static GraphTraversal<S, E> AsGraphTraversal<S, E>(this ITraversal traversal)
        {
            return new GraphTraversal<S, E>(new ITraversalStrategy[0], traversal.Bytecode);
        }

        /// <summary>
        /// Casts the traversal to a gremlin graph traversal.
        /// </summary>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the traversal</returns>
        internal static GraphTraversal<object, object> AsGraphTraversal(this ITraversal traversal)
        {
            return new GraphTraversal<object, object>(new ITraversalStrategy[0], traversal.Bytecode);
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
    }
}