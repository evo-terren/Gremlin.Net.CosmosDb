using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;
using System.Collections.Generic;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Helper extension methods for <see cref="Gremlin.Net.CosmosDb.ISchemaBoundTraversal{S, E}"/> objects
    /// </summary>
    static partial class ISchemaBoundTraversalExtensions
    {
        /// <summary>
        /// Adds the addE step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> AddE<S, E>(this ISchemaBoundTraversal<S, E> traversal, string edgeLabel)
        {
            return traversal.ToGraphTraversal().AddE(edgeLabel);
        }

        /// <summary>
        /// Adds the addE step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> AddE<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal edgeLabelTraversal)
        {
            return traversal.ToGraphTraversal().AddE(edgeLabelTraversal);
        }

        /// <summary>
        /// Adds the addV step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> AddV<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().AddV();
        }

        /// <summary>
        /// Adds the addV step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> AddV<S, E>(this ISchemaBoundTraversal<S, E> traversal, string vertexLabel)
        {
            return traversal.ToGraphTraversal().AddV(vertexLabel);
        }

        /// <summary>
        /// Adds the addV step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> AddV<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal vertexLabelTraversal)
        {
            return traversal.ToGraphTraversal().AddV(vertexLabelTraversal);
        }

        /// <summary>
        /// Adds the aggregate step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Aggregate<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.ToGraphTraversal().Aggregate(sideEffectKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the and step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> And<S, E>(this ISchemaBoundTraversal<S, E> traversal, params ITraversal[] andTraversals)
        {
            return traversal.ToGraphTraversal().And(andTraversals).AsSchemaBound();
        }

        /// <summary>
        /// Adds the as step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> As<S, E>(this ISchemaBoundTraversal<S, E> traversal, string stepLabel, params string[] stepLabels)
        {
            return traversal.ToGraphTraversal().As(stepLabel, stepLabels).AsSchemaBound();
        }

        /// <summary>
        /// Adds the barrier step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Barrier<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().Barrier().AsSchemaBound();
        }

        /// <summary>
        /// Adds the barrier step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Barrier<S, E>(this ISchemaBoundTraversal<S, E> traversal, IConsumer barrierConsumer)
        {
            return traversal.ToGraphTraversal().Barrier(barrierConsumer).AsSchemaBound();
        }

        /// <summary>
        /// Adds the barrier step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Barrier<S, E>(this ISchemaBoundTraversal<S, E> traversal, int maxBarrierSize)
        {
            return traversal.ToGraphTraversal().Barrier(maxBarrierSize).AsSchemaBound();
        }

        /// <summary>
        /// Adds the both step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> Both<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] edgeLabels)
        {
            return traversal.ToGraphTraversal().Both(edgeLabels);
        }

        /// <summary>
        /// Adds the bothE step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> BothE<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] edgeLabels)
        {
            return traversal.ToGraphTraversal().BothE(edgeLabels);
        }

        /// <summary>
        /// Adds the bothV step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> BothV<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().BothV();
        }

        /// <summary>
        /// Adds the branch step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Branch<E2>(this ISchemaBoundTraversal traversal, IFunction function)
        {
            return traversal.ToGraphTraversal<object, E2>().Branch<E2>(function);
        }

        /// <summary>
        /// Adds the branch step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Branch<E2>(this ISchemaBoundTraversal traversal, ITraversal branchTraversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Branch<E2>(branchTraversal);
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().By().AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, IComparator comparator)
        {
            return traversal.ToGraphTraversal().By(comparator).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, IFunction function, IComparator comparator)
        {
            return traversal.ToGraphTraversal().By(function, comparator).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, Order order)
        {
            return traversal.ToGraphTraversal().By(order).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, string key)
        {
            return traversal.ToGraphTraversal().By(key).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, string key, IComparator comparator)
        {
            return traversal.ToGraphTraversal().By(key, comparator).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, T token)
        {
            return traversal.ToGraphTraversal().By(token).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal byTraversal)
        {
            return traversal.ToGraphTraversal().By(byTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal byTraversal, IComparator comparator)
        {
            return traversal.ToGraphTraversal().By(byTraversal, comparator).AsSchemaBound();
        }

        /// <summary>
        /// Adds the cap step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Cap<E2>(this ISchemaBoundTraversal traversal, string sideEffectKey, params string[] sideEffectKeys)
        {
            return traversal.ToGraphTraversal<object, E2>().Cap<E2>(sideEffectKey, sideEffectKeys);
        }

        /// <summary>
        /// Adds the choose step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Choose<E2>(this ISchemaBoundTraversal traversal, IFunction choiceFunction)
        {
            return traversal.ToGraphTraversal<object, E2>().Choose<E2>(choiceFunction);
        }

        /// <summary>
        /// Adds the choose step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Choose<E2>(this ISchemaBoundTraversal traversal, P choosePredicate, ITraversal trueChoice)
        {
            return traversal.ToGraphTraversal<object, E2>().Choose<E2>(choosePredicate, trueChoice);
        }

        /// <summary>
        /// Adds the choose step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Choose<E2>(this ISchemaBoundTraversal traversal, P choosePredicate, ITraversal trueChoice, ITraversal falseChoice)
        {
            return traversal.ToGraphTraversal<object, E2>().Choose<E2>(choosePredicate, trueChoice, falseChoice);
        }

        /// <summary>
        /// Adds the choose step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Choose<E2>(this ISchemaBoundTraversal traversal, ITraversal choiceTraversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Choose<E2>(choiceTraversal);
        }

        /// <summary>
        /// Adds the choose step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Choose<E2>(this ISchemaBoundTraversal traversal, ITraversal traversalPredicate, ITraversal trueChoice)
        {
            return traversal.ToGraphTraversal<object, E2>().Choose<E2>(traversalPredicate, trueChoice);
        }

        /// <summary>
        /// Adds the choose step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Choose<E2>(this ISchemaBoundTraversal traversal, ITraversal traversalPredicate, ITraversal trueChoice, ITraversal falseChoice)
        {
            return traversal.ToGraphTraversal<object, E2>().Choose<E2>(traversalPredicate, trueChoice, falseChoice);
        }

        /// <summary>
        /// Adds the coalesce step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Coalesce<E2>(this ISchemaBoundTraversal traversal, params ITraversal[] coalesceTraversals)
        {
            return traversal.ToGraphTraversal<object, E2>().Coalesce<E2>(coalesceTraversals);
        }

        /// <summary>
        /// Adds the coin step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Coin<S, E>(this ISchemaBoundTraversal<S, E> traversal, double probability)
        {
            return traversal.ToGraphTraversal().Coin(probability).AsSchemaBound();
        }

        /// <summary>
        /// Adds the constant step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Constant<E2>(this ISchemaBoundTraversal traversal, E2 e)
        {
            return traversal.ToGraphTraversal<object, E2>().Constant<E2>(e);
        }

        /// <summary>
        /// Adds the count step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, long> Count<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().Count();
        }

        /// <summary>
        /// Adds the count step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, long> Count<S, E>(this ISchemaBoundTraversal<S, E> traversal, Scope scope)
        {
            return traversal.ToGraphTraversal().Count(scope);
        }

        /// <summary>
        /// Adds the cyclicPath step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> CyclicPath<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().CyclicPath().AsSchemaBound();
        }

        /// <summary>
        /// Adds the dedup step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Dedup<S, E>(this ISchemaBoundTraversal<S, E> traversal, Scope scope, params string[] dedupLabels)
        {
            return traversal.ToGraphTraversal().Dedup(scope, dedupLabels).AsSchemaBound();
        }

        /// <summary>
        /// Adds the dedup step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Dedup<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] dedupLabels)
        {
            return traversal.ToGraphTraversal().Dedup(dedupLabels).AsSchemaBound();
        }

        /// <summary>
        /// Adds the drop step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Drop<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().Drop().AsSchemaBound();
        }

        /// <summary>
        /// Adds the emit step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Emit<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().Emit().AsSchemaBound();
        }

        /// <summary>
        /// Adds the emit step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Emit<S, E>(this ISchemaBoundTraversal<S, E> traversal, P emitPredicate)
        {
            return traversal.ToGraphTraversal().Emit(emitPredicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the emit step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Emit<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal emitTraversal)
        {
            return traversal.ToGraphTraversal().Emit(emitTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the filter step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Filter<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.ToGraphTraversal().Filter(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the filter step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Filter<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal filterTraversal)
        {
            return traversal.ToGraphTraversal().Filter(filterTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the flatMap step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> FlatMap<E2>(this ISchemaBoundTraversal traversal, IFunction function)
        {
            return traversal.ToGraphTraversal<object, E2>().FlatMap<E2>(function);
        }

        /// <summary>
        /// Adds the flatMap step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> FlatMap<E2>(this ISchemaBoundTraversal traversal, ITraversal flatMapTraversal)
        {
            return traversal.ToGraphTraversal<object, E2>().FlatMap<E2>(flatMapTraversal);
        }

        /// <summary>
        /// Adds the fold step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, IList<E>> Fold<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().Fold();
        }

        /// <summary>
        /// Adds the fold step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Fold<E2>(this ISchemaBoundTraversal traversal, E2 seed, IBiFunction foldFunction)
        {
            return traversal.ToGraphTraversal<object, E2>().Fold(seed, foldFunction);
        }

        /// <summary>
        /// Adds the from step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> From<S, E>(this ISchemaBoundTraversal<S, E> traversal, string fromStepLabel)
        {
            return traversal.ToGraphTraversal().From(fromStepLabel).AsSchemaBound();
        }

        /// <summary>
        /// Adds the from step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> From<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal fromVertex)
        {
            return traversal.ToGraphTraversal().From(fromVertex).AsSchemaBound();
        }

        /// <summary>
        /// Adds the from step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> From<S, E>(this ISchemaBoundTraversal<S, E> traversal, Vertex fromVertex)
        {
            return traversal.ToGraphTraversal().From(fromVertex).AsSchemaBound();
        }

        /// <summary>
        /// Adds the group step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<K, V>> Group<K, V>(this ISchemaBoundTraversal traversal)
        {
            return traversal.ToGraphTraversal<object, IDictionary<K, V>>().Group<K, V>();
        }

        /// <summary>
        /// Adds the group step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Group<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.ToGraphTraversal().Group(sideEffectKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the groupCount step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<K, long>> GroupCount<K>(this ISchemaBoundTraversal traversal)
        {
            return traversal.ToGraphTraversal<object, IDictionary<K, long>>().GroupCount<K>();
        }

        /// <summary>
        /// Adds the groupCount step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> GroupCount<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.ToGraphTraversal().GroupCount(sideEffectKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, string propertyKey)
        {
            return traversal.ToGraphTraversal().Has(propertyKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, string propertyKey, object value)
        {
            return traversal.ToGraphTraversal().Has(propertyKey, value).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, string propertyKey, P predicate)
        {
            return traversal.ToGraphTraversal().Has(propertyKey, predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, string label, string propertyKey, object value)
        {
            return traversal.ToGraphTraversal().Has(label, propertyKey, value).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, string label, string propertyKey, P predicate)
        {
            return traversal.ToGraphTraversal().Has(label, propertyKey, predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, string propertyKey, ITraversal propertyTraversal)
        {
            return traversal.ToGraphTraversal().Has(propertyKey, propertyTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, T accessor, object value)
        {
            return traversal.ToGraphTraversal().Has(accessor, value).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, T accessor, P predicate)
        {
            return traversal.ToGraphTraversal().Has(accessor, predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, T accessor, ITraversal propertyTraversal)
        {
            return traversal.ToGraphTraversal().Has(accessor, propertyTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasId step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasId<S, E>(this ISchemaBoundTraversal<S, E> traversal, object id, params object[] otherIds)
        {
            return traversal.ToGraphTraversal().HasId(id, otherIds).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasId step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasId<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.ToGraphTraversal().HasId(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasKey step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasKey<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.ToGraphTraversal().HasKey(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasKey step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasKey<S, E>(this ISchemaBoundTraversal<S, E> traversal, string label, params string[] otherLabels)
        {
            return traversal.ToGraphTraversal().HasLabel(label, otherLabels).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasLabel step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasLabel<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.ToGraphTraversal().HasLabel(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasLabel step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasLabel<S, E>(this ISchemaBoundTraversal<S, E> traversal, string label, params string[] otherLabels)
        {
            return traversal.ToGraphTraversal().HasLabel(label, otherLabels).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasNot step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasNot<S, E>(this ISchemaBoundTraversal<S, E> traversal, string propertyKey)
        {
            return traversal.ToGraphTraversal().HasNot(propertyKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasValue step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasValue<S, E>(this ISchemaBoundTraversal<S, E> traversal, object value, params object[] otherValues)
        {
            return traversal.ToGraphTraversal().HasValue(value, otherValues).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasValue step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasValue<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.ToGraphTraversal().HasValue(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the id step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, object> Id<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().Id();
        }

        /// <summary>
        /// Adds the identity step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Identity<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().Identity().AsSchemaBound();
        }

        /// <summary>
        /// Adds the in step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> In<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] edgeLabels)
            where E : Gremlin.Net.CosmosDb.Structure.VertexBase
        {
            return traversal.ToGraphTraversal().In(edgeLabels);
        }

        /// <summary>
        /// Adds the inE step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> InE<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] edgeLabels)
            where E : Gremlin.Net.CosmosDb.Structure.VertexBase
        {
            return traversal.ToGraphTraversal().InE(edgeLabels);
        }

        /// <summary>
        /// Adds the inject step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Inject<S, E>(this ISchemaBoundTraversal<S, E> traversal, params E[] injections)
        {
            return traversal.ToGraphTraversal().Inject(injections).AsSchemaBound();
        }

        /// <summary>
        /// Adds the is step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Is<S, E>(this ISchemaBoundTraversal<S, E> traversal, object value)
        {
            return traversal.ToGraphTraversal().Is(value).AsSchemaBound();
        }

        /// <summary>
        /// Adds the is step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Is<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.ToGraphTraversal().Is(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the key step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, string> Key<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().Key();
        }

        /// <summary>
        /// Adds the label step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, string> Label<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().Label();
        }

        /// <summary>
        /// Adds the limit step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Limit<E2>(this ISchemaBoundTraversal traversal, Scope scope, long limit)
        {
            return traversal.ToGraphTraversal<object, E2>().Limit<E2>(scope, limit);
        }

        /// <summary>
        /// Adds the limit step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Limit<E2>(this ISchemaBoundTraversal traversal, long limit)
        {
            return traversal.ToGraphTraversal<object, E2>().Limit<E2>(limit);
        }

        /// <summary>
        /// Adds the local step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Local<E2>(this ISchemaBoundTraversal traversal, ITraversal localTraversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Local<E2>(localTraversal);
        }

        /// <summary>
        /// Adds the loops step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, int> Loops<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().Loops();
        }

        /// <summary>
        /// Adds the map step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Map<E2>(this ISchemaBoundTraversal traversal, IFunction function)
        {
            return traversal.ToGraphTraversal<object, E2>().Map<E2>(function);
        }

        /// <summary>
        /// Adds the map step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Map<E2>(this ISchemaBoundTraversal traversal, ITraversal mapTraversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Map<E2>(mapTraversal);
        }

        /// <summary>
        /// Adds the match step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<string, E2>> Match<E2>(this ISchemaBoundTraversal traversal, params ITraversal[] matchTraversals)
        {
            return traversal.ToGraphTraversal<object, E2>().Match<E2>(matchTraversals);
        }

        /// <summary>
        /// Adds the math step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, double> Math<S, E>(this ISchemaBoundTraversal<S, E> traversal, string expression)
        {
            return traversal.ToGraphTraversal().Math(expression);
        }

        /// <summary>
        /// Adds the max step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Max<E2>(this ISchemaBoundTraversal traversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Max<E2>();
        }

        /// <summary>
        /// Adds the max step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Max<E2>(this ISchemaBoundTraversal traversal, Scope scope)
        {
            return traversal.ToGraphTraversal<object, E2>().Max<E2>(scope);
        }

        /// <summary>
        /// Adds the mean step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Mean<E2>(this ISchemaBoundTraversal traversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Mean<E2>();
        }

        /// <summary>
        /// Adds the mean step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Mean<E2>(this ISchemaBoundTraversal traversal, Scope scope)
        {
            return traversal.ToGraphTraversal<object, E2>().Mean<E2>(scope);
        }

        /// <summary>
        /// Adds the min step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Min<E2>(this ISchemaBoundTraversal traversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Min<E2>();
        }

        /// <summary>
        /// Adds the min step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Min<E2>(this ISchemaBoundTraversal traversal, Scope scope)
        {
            return traversal.ToGraphTraversal<object, E2>().Min<E2>(scope);
        }

        /// <summary>
        /// Adds the not step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Not<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal notTraversal)
        {
            return traversal.ToGraphTraversal().Not(notTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the option step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Option<S, E>(this ISchemaBoundTraversal<S, E> traversal, object pickToken, ITraversal traversalOption)
        {
            return traversal.ToGraphTraversal().Option(pickToken, traversalOption).AsSchemaBound();
        }

        /// <summary>
        /// Adds the option step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Option<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal traversalOption)
        {
            return traversal.ToGraphTraversal().Option(traversalOption).AsSchemaBound();
        }

        /// <summary>
        /// Adds the optional step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Optional<E2>(this ISchemaBoundTraversal traversal, ITraversal optionalTraversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Optional<E2>(optionalTraversal);
        }

        /// <summary>
        /// Adds the or step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Or<S, E>(this ISchemaBoundTraversal<S, E> traversal, params ITraversal[] orTraversals)
        {
            return traversal.ToGraphTraversal().Or(orTraversals).AsSchemaBound();
        }

        /// <summary>
        /// Adds the order step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Order<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().Order().AsSchemaBound();
        }

        /// <summary>
        /// Adds the order step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Order<S, E>(this ISchemaBoundTraversal<S, E> traversal, Scope scope)
        {
            return traversal.ToGraphTraversal().Order(scope).AsSchemaBound();
        }

        /// <summary>
        /// Adds the otherV step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> OtherV<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().OtherV();
        }

        /// <summary>
        /// Adds the out step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> Out<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] edgeLabels)
            where E : Gremlin.Net.CosmosDb.Structure.VertexBase
        {
            return traversal.ToGraphTraversal().Out(edgeLabels);
        }

        /// <summary>
        /// Adds the outE step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> OutE<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] edgeLabels)
            where E : Gremlin.Net.CosmosDb.Structure.VertexBase
        {
            return traversal.ToGraphTraversal().OutE(edgeLabels);
        }

        /// <summary>
        /// Adds the pageRank step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> PageRank<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().PageRank().AsSchemaBound();
        }

        /// <summary>
        /// Adds the pageRank step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> PageRank<S, E>(this ISchemaBoundTraversal<S, E> traversal, double alpha)
        {
            return traversal.ToGraphTraversal().PageRank(alpha).AsSchemaBound();
        }

        /// <summary>
        /// Adds the path step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Path> Path<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().Path();
        }

        /// <summary>
        /// Adds the peerPressure step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> PeerPressure<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().PeerPressure().AsSchemaBound();
        }

        /// <summary>
        /// Adds the profile step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Profile<E2>(this ISchemaBoundTraversal traversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Profile<E2>();
        }

        /// <summary>
        /// Adds the profile step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Profile<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.ToGraphTraversal().Profile(sideEffectKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the program step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Program<S, E>(this ISchemaBoundTraversal<S, E> traversal, object vertexProgram)
        {
            return traversal.ToGraphTraversal().Program(vertexProgram).AsSchemaBound();
        }

        /// <summary>
        /// Adds the project step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<string, E2>> Project<E2>(this ISchemaBoundTraversal traversal, string projectKey, params string[] otherProjectKeys)
        {
            return traversal.ToGraphTraversal<object, E2>().Project<E2>(projectKey, otherProjectKeys);
        }

        /// <summary>
        /// Adds the properties step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Properties<E2>(this ISchemaBoundTraversal traversal, params string[] propertyKeys)
        {
            return traversal.ToGraphTraversal<object, E2>().Properties<E2>(propertyKeys);
        }

        /// <summary>
        /// Adds the property step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Property<S, E>(this ISchemaBoundTraversal<S, E> traversal, Cardinality cardinality, object key, object value, params object[] keyValues)
        {
            return traversal.ToGraphTraversal().Property(cardinality, key, value, keyValues).AsSchemaBound();
        }

        /// <summary>
        /// Adds the property step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Property<S, E>(this ISchemaBoundTraversal<S, E> traversal, object key, object value, params object[] keyValues)
        {
            return traversal.ToGraphTraversal().Property(key, value, keyValues).AsSchemaBound();
        }

        /// <summary>
        /// Adds the propertyMap step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<string, E2>> PropertyMap<E2>(this ISchemaBoundTraversal traversal, params string[] propertyKeys)
        {
            return traversal.ToGraphTraversal<object, E2>().PropertyMap<E2>(propertyKeys);
        }

        /// <summary>
        /// Adds the range step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Range<E2>(this ISchemaBoundTraversal traversal, Scope scope, long low, long high)
        {
            return traversal.ToGraphTraversal<object, E2>().Range<E2>(scope, low, high);
        }

        /// <summary>
        /// Adds the range step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Range<E2>(this ISchemaBoundTraversal traversal, long low, long high)
        {
            return traversal.ToGraphTraversal<object, E2>().Range<E2>(low, high);
        }

        /// <summary>
        /// Adds the repeat step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Repeat<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal repeatTraversal)
        {
            return traversal.ToGraphTraversal().Repeat(repeatTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the sack step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Sack<E2>(this ISchemaBoundTraversal traversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Sack<E2>();
        }

        /// <summary>
        /// Adds the sack step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Sack<S, E>(this ISchemaBoundTraversal<S, E> traversal, IBiFunction sackOperator)
        {
            return traversal.ToGraphTraversal().Sack(sackOperator).AsSchemaBound();
        }

        /// <summary>
        /// Adds the sample step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Sample<S, E>(this ISchemaBoundTraversal<S, E> traversal, Scope scope, int amountToSample)
        {
            return traversal.ToGraphTraversal().Sample(scope, amountToSample).AsSchemaBound();
        }

        /// <summary>
        /// Adds the sample step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Sample<S, E>(this ISchemaBoundTraversal<S, E> traversal, int amountToSample)
        {
            return traversal.ToGraphTraversal().Sample(amountToSample).AsSchemaBound();
        }

        /// <summary>
        /// Adds the select step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, ICollection<E2>> Select<E2>(this ISchemaBoundTraversal traversal, Column column)
        {
            return traversal.ToGraphTraversal<object, E2>().Select<E2>(column);
        }

        /// <summary>
        /// Adds the select step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Select<E2>(this ISchemaBoundTraversal traversal, Pop pop, string selectKey)
        {
            return traversal.ToGraphTraversal<object, E2>().Select<E2>(pop, selectKey);
        }

        /// <summary>
        /// Adds the select step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<string, E2>> Select<E2>(this ISchemaBoundTraversal traversal, Pop pop, string selectKey1, string selectKey2, params string[] otherSelectKeys)
        {
            return traversal.ToGraphTraversal<object, E2>().Select<E2>(pop, selectKey1, selectKey2, otherSelectKeys);
        }

        /// <summary>
        /// Adds the select step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Select<E2>(this ISchemaBoundTraversal traversal, string selectKey)
        {
            return traversal.ToGraphTraversal<object, E2>().Select<E2>(selectKey);
        }

        /// <summary>
        /// Adds the select step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<string, E2>> Select<E2>(this ISchemaBoundTraversal traversal, string selectKey1, string selectKey2, params string[] otherSelectKeys)
        {
            return traversal.ToGraphTraversal<object, E2>().Select<E2>(selectKey1, selectKey2, otherSelectKeys);
        }

        /// <summary>
        /// Adds the sideEffect step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> SideEffect<S, E>(this ISchemaBoundTraversal<S, E> traversal, IConsumer consumer)
        {
            return traversal.ToGraphTraversal().SideEffect(consumer).AsSchemaBound();
        }

        /// <summary>
        /// Adds the sideEffect step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> SideEffect<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal sideEffectTraversal)
        {
            return traversal.ToGraphTraversal().SideEffect(sideEffectTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the simplePath step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> SimplePath<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().SimplePath().AsSchemaBound();
        }

        /// <summary>
        /// Adds the skip step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Skip<E2>(this ISchemaBoundTraversal traversal, Scope scope, long skip)
        {
            return traversal.ToGraphTraversal<object, E2>().Skip<E2>(scope, skip);
        }

        /// <summary>
        /// Adds the skip step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Skip<E2>(this ISchemaBoundTraversal traversal, long skip)
        {
            return traversal.ToGraphTraversal<object, E2>().Skip<E2>(skip);
        }

        /// <summary>
        /// Adds the store step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Store<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.ToGraphTraversal().Store(sideEffectKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the subgraph step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> Subgraph<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.ToGraphTraversal().Subgraph(sideEffectKey);
        }

        /// <summary>
        /// Adds the sum step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Sum<E2>(this ISchemaBoundTraversal traversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Sum<E2>();
        }

        /// <summary>
        /// Adds the sum step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Sum<E2>(this ISchemaBoundTraversal traversal, Scope scope)
        {
            return traversal.ToGraphTraversal<object, E2>().Sum<E2>(scope);
        }

        /// <summary>
        /// Adds the tail step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Tail<E2>(this ISchemaBoundTraversal traversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Tail<E2>();
        }

        /// <summary>
        /// Adds the tail step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Tail<E2>(this ISchemaBoundTraversal traversal, Scope scope)
        {
            return traversal.ToGraphTraversal<object, E2>().Tail<E2>(scope);
        }

        /// <summary>
        /// Adds the tail step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Tail<E2>(this ISchemaBoundTraversal traversal, Scope scope, long limit)
        {
            return traversal.ToGraphTraversal<object, E2>().Tail<E2>(scope, limit);
        }

        /// <summary>
        /// Adds the tail step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Tail<E2>(this ISchemaBoundTraversal traversal, long limit)
        {
            return traversal.ToGraphTraversal<object, E2>().Tail<E2>(limit);
        }

        /// <summary>
        /// Adds the timeLimit step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> TimeLimit<S, E>(this ISchemaBoundTraversal<S, E> traversal, long timeLimit)
        {
            return traversal.ToGraphTraversal().TimeLimit(timeLimit).AsSchemaBound();
        }

        /// <summary>
        /// Adds the times step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Times<S, E>(this ISchemaBoundTraversal<S, E> traversal, int maxLoops)
        {
            return traversal.ToGraphTraversal().Times(maxLoops).AsSchemaBound();
        }

        /// <summary>
        /// Adds the to step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> To<S, E>(this ISchemaBoundTraversal<S, E> traversal, Direction direction, params string[] edgeLabels)
        {
            return traversal.ToGraphTraversal().To(direction, edgeLabels);
        }

        /// <summary>
        /// Adds the to step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> To<S, E>(this ISchemaBoundTraversal<S, E> traversal, string toStepLabel)
        {
            return traversal.ToGraphTraversal().To(toStepLabel).AsSchemaBound();
        }

        /// <summary>
        /// Adds the to step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> To<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal toVertex)
        {
            return traversal.ToGraphTraversal().To(toVertex).AsSchemaBound();
        }

        /// <summary>
        /// Adds the to step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> To<S, E>(this ISchemaBoundTraversal<S, E> traversal, Vertex toVertex)
        {
            return traversal.ToGraphTraversal().To(toVertex).AsSchemaBound();
        }

        /// <summary>
        /// Adds the toE step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> ToE<S, E>(this ISchemaBoundTraversal<S, E> traversal, Direction direction, params string[] edgeLabels)
        {
            return traversal.ToGraphTraversal().ToE(direction, edgeLabels);
        }

        /// <summary>
        /// Returns the string-equivalent of the given traversal
        /// </summary>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the string query</returns>
        public static string ToGremlinQuery<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.ToGraphTraversal().ToGremlinQuery();
        }

        /// <summary>
        /// Adds the toV step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> ToV<S, E>(this ISchemaBoundTraversal<S, E> traversal, Direction direction)
        {
            return traversal.ToGraphTraversal().ToV(direction);
        }

        /// <summary>
        /// Adds the tree step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Tree<E2>(this ISchemaBoundTraversal traversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Tree<E2>();
        }

        /// <summary>
        /// Adds the tree step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Tree<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.ToGraphTraversal().Tree(sideEffectKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the unfold step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Unfold<E2>(this ISchemaBoundTraversal traversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Unfold<E2>();
        }

        /// <summary>
        /// Adds the union step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Union<E2>(this ISchemaBoundTraversal traversal, params ITraversal[] unionTraversals)
        {
            return traversal.ToGraphTraversal<object, E2>().Union<E2>(unionTraversals);
        }

        /// <summary>
        /// Adds the until step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Until<S, E>(this ISchemaBoundTraversal<S, E> traversal, P untilPredicate)
        {
            return traversal.ToGraphTraversal().Until(untilPredicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the until step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Until<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal untilTraversal)
        {
            return traversal.ToGraphTraversal().Until(untilTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the value step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Value<E2>(this ISchemaBoundTraversal traversal)
        {
            return traversal.ToGraphTraversal<object, E2>().Value<E2>();
        }

        /// <summary>
        /// Adds the valueMap step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<TKey, TValue>> ValueMap<TKey, TValue>(this ISchemaBoundTraversal traversal, params string[] propertyKeys)
        {
            return traversal.ToGraphTraversal<object, object>().ValueMap<TKey, TValue>(propertyKeys);
        }

        /// <summary>
        /// Adds the valueMap step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, IDictionary<TKey, TValue>> ValueMap<TKey, TValue>(this ISchemaBoundTraversal traversal, bool includeTokens, params string[] propertyKeys)
        {
            return traversal.ToGraphTraversal<object, object>().ValueMap<TKey, TValue>(includeTokens, propertyKeys);
        }

        /// <summary>
        /// Adds the values step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<object, E2> Values<E2>(this ISchemaBoundTraversal traversal, params string[] propertyKeys)
        {
            return traversal.ToGraphTraversal<object, E2>().Values<E2>(propertyKeys);
        }

        /// <summary>
        /// Adds the where step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Where<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.ToGraphTraversal().Where(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the where step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Where<S, E>(this ISchemaBoundTraversal<S, E> traversal, string startKey, P predicate)
        {
            return traversal.ToGraphTraversal().Where(startKey, predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the where step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Where<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal whereTraversal)
        {
            return traversal.ToGraphTraversal().Where(whereTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Casts the schema-bound traversal to a gremlin graph traversal.
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="E">The type of the current element</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the traversal</returns>
        internal static GraphTraversal<S, E> ToGraphTraversal<S, E>(this ISchemaBoundTraversal traversal)
        {
            return new GraphTraversal<S, E>(new ITraversalStrategy[0], traversal.Bytecode);
        }

        /// <summary>
        /// Casts the schema-bound traversal to a gremlin graph traversal.
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="E">The type of the current element</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the traversal</returns>
        internal static GraphTraversal<S, E> ToGraphTraversal<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return new GraphTraversal<S, E>(new ITraversalStrategy[0], traversal.Bytecode);
        }
    }
}