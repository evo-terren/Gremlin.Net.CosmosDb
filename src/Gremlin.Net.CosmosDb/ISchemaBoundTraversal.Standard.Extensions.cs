using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;
using System.Collections.Generic;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Helper extension methods for <see cref="ISchemaBoundTraversal{S, E}"/> objects
    /// </summary>
    public static partial class ISchemaBoundTraversalExtensions
    {
        /// <summary>
        /// Adds the addE step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> AddE<S, E>(this ISchemaBoundTraversal<S, E> traversal, string edgeLabel)
        {
            return traversal.AsGraphTraversal().AddE(edgeLabel);
        }

        /// <summary>
        /// Adds the addE step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> AddE<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal edgeLabelTraversal)
        {
            return traversal.AsGraphTraversal().AddE(edgeLabelTraversal);
        }

        /// <summary>
        /// Adds the addV step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> AddV<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().AddV();
        }

        /// <summary>
        /// Adds the addV step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> AddV<S, E>(this ISchemaBoundTraversal<S, E> traversal, string vertexLabel)
        {
            return traversal.AsGraphTraversal().AddV(vertexLabel);
        }

        /// <summary>
        /// Adds the addV step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> AddV<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal vertexLabelTraversal)
        {
            return traversal.AsGraphTraversal().AddV(vertexLabelTraversal);
        }

        /// <summary>
        /// Adds the aggregate step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Aggregate<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.AsGraphTraversal().Aggregate(sideEffectKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the and step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> And<S, E>(this ISchemaBoundTraversal<S, E> traversal, params ITraversal[] andTraversals)
        {
            return traversal.AsGraphTraversal().And(andTraversals).AsSchemaBound();
        }

        /// <summary>
        /// Adds the as step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> As<S, E>(this ISchemaBoundTraversal<S, E> traversal, string stepLabel, params string[] stepLabels)
        {
            return traversal.AsGraphTraversal().As(stepLabel, stepLabels).AsSchemaBound();
        }

        /// <summary>
        /// Adds the barrier step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Barrier<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().Barrier().AsSchemaBound();
        }

        /// <summary>
        /// Adds the barrier step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Barrier<S, E>(this ISchemaBoundTraversal<S, E> traversal, IConsumer barrierConsumer)
        {
            return traversal.AsGraphTraversal().Barrier(barrierConsumer).AsSchemaBound();
        }

        /// <summary>
        /// Adds the barrier step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Barrier<S, E>(this ISchemaBoundTraversal<S, E> traversal, int maxBarrierSize)
        {
            return traversal.AsGraphTraversal().Barrier(maxBarrierSize).AsSchemaBound();
        }

        /// <summary>
        /// Adds the both step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> Both<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] edgeLabels)
        {
            return traversal.AsGraphTraversal().Both(edgeLabels);
        }

        /// <summary>
        /// Adds the bothE step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> BothE<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] edgeLabels)
        {
            return traversal.AsGraphTraversal().BothE(edgeLabels);
        }

        /// <summary>
        /// Adds the bothV step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> BothV<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().BothV();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().By().AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, IComparator comparator)
        {
            return traversal.AsGraphTraversal().By(comparator).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, IFunction function, IComparator comparator)
        {
            return traversal.AsGraphTraversal().By(function, comparator).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, Order order)
        {
            return traversal.AsGraphTraversal().By(order).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, string key)
        {
            return traversal.AsGraphTraversal().By(key).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, string key, IComparator comparator)
        {
            return traversal.AsGraphTraversal().By(key, comparator).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, T token)
        {
            return traversal.AsGraphTraversal().By(token).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal byTraversal)
        {
            return traversal.AsGraphTraversal().By(byTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the by step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> By<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal byTraversal, IComparator comparator)
        {
            return traversal.AsGraphTraversal().By(byTraversal, comparator).AsSchemaBound();
        }

        /// <summary>
        /// Adds the coin step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Coin<S, E>(this ISchemaBoundTraversal<S, E> traversal, double probability)
        {
            return traversal.AsGraphTraversal().Coin(probability).AsSchemaBound();
        }

        /// <summary>
        /// Adds the count step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, long> Count<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().Count();
        }

        /// <summary>
        /// Adds the count step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, long> Count<S, E>(this ISchemaBoundTraversal<S, E> traversal, Scope scope)
        {
            return traversal.AsGraphTraversal().Count(scope);
        }

        /// <summary>
        /// Adds the cyclicPath step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> CyclicPath<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().CyclicPath().AsSchemaBound();
        }

        /// <summary>
        /// Adds the dedup step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Dedup<S, E>(this ISchemaBoundTraversal<S, E> traversal, Scope scope, params string[] dedupLabels)
        {
            return traversal.AsGraphTraversal().Dedup(scope, dedupLabels).AsSchemaBound();
        }

        /// <summary>
        /// Adds the dedup step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Dedup<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] dedupLabels)
        {
            return traversal.AsGraphTraversal().Dedup(dedupLabels).AsSchemaBound();
        }

        /// <summary>
        /// Adds the drop step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Drop<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().Drop().AsSchemaBound();
        }

        /// <summary>
        /// Adds the emit step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Emit<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().Emit().AsSchemaBound();
        }

        /// <summary>
        /// Adds the emit step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Emit<S, E>(this ISchemaBoundTraversal<S, E> traversal, P emitPredicate)
        {
            return traversal.AsGraphTraversal().Emit(emitPredicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the emit step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Emit<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal emitTraversal)
        {
            return traversal.AsGraphTraversal().Emit(emitTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the filter step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Filter<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.AsGraphTraversal().Filter(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the filter step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Filter<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal filterTraversal)
        {
            return traversal.AsGraphTraversal().Filter(filterTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the fold step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, IList<E>> Fold<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().Fold();
        }

        /// <summary>
        /// Adds the from step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> From<S, E>(this ISchemaBoundTraversal<S, E> traversal, string fromStepLabel)
        {
            return traversal.AsGraphTraversal().From(fromStepLabel).AsSchemaBound();
        }

        /// <summary>
        /// Adds the from step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> From<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal fromVertex)
        {
            return traversal.AsGraphTraversal().From(fromVertex).AsSchemaBound();
        }

        /// <summary>
        /// Adds the from step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> From<S, E>(this ISchemaBoundTraversal<S, E> traversal, Vertex fromVertex)
        {
            return traversal.AsGraphTraversal().From(fromVertex).AsSchemaBound();
        }

        /// <summary>
        /// Adds the group step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Group<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.AsGraphTraversal().Group(sideEffectKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the groupCount step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> GroupCount<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.AsGraphTraversal().GroupCount(sideEffectKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, string propertyKey)
        {
            return traversal.AsGraphTraversal().Has(propertyKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, string propertyKey, object value)
        {
            return traversal.AsGraphTraversal().Has(propertyKey, value).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, string propertyKey, P predicate)
        {
            return traversal.AsGraphTraversal().Has(propertyKey, predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, string label, string propertyKey, object value)
        {
            return traversal.AsGraphTraversal().Has(label, propertyKey, value).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, string label, string propertyKey, P predicate)
        {
            return traversal.AsGraphTraversal().Has(label, propertyKey, predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, string propertyKey, ITraversal propertyTraversal)
        {
            return traversal.AsGraphTraversal().Has(propertyKey, propertyTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, T accessor, object value)
        {
            return traversal.AsGraphTraversal().Has(accessor, value).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, T accessor, P predicate)
        {
            return traversal.AsGraphTraversal().Has(accessor, predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the has step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Has<S, E>(this ISchemaBoundTraversal<S, E> traversal, T accessor, ITraversal propertyTraversal)
        {
            return traversal.AsGraphTraversal().Has(accessor, propertyTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasId step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasId<S, E>(this ISchemaBoundTraversal<S, E> traversal, object id, params object[] otherIds)
        {
            return traversal.AsGraphTraversal().HasId(id, otherIds).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasId step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasId<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.AsGraphTraversal().HasId(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasKey step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasKey<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.AsGraphTraversal().HasKey(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasKey step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasKey<S, E>(this ISchemaBoundTraversal<S, E> traversal, string label, params string[] otherLabels)
        {
            return traversal.AsGraphTraversal().HasLabel(label, otherLabels).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasLabel step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasLabel<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.AsGraphTraversal().HasLabel(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasLabel step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasLabel<S, E>(this ISchemaBoundTraversal<S, E> traversal, string label, params string[] otherLabels)
        {
            return traversal.AsGraphTraversal().HasLabel(label, otherLabels).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasNot step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasNot<S, E>(this ISchemaBoundTraversal<S, E> traversal, string propertyKey)
        {
            return traversal.AsGraphTraversal().HasNot(propertyKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasValue step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasValue<S, E>(this ISchemaBoundTraversal<S, E> traversal, object value, params object[] otherValues)
        {
            return traversal.AsGraphTraversal().HasValue(value, otherValues).AsSchemaBound();
        }

        /// <summary>
        /// Adds the hasValue step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> HasValue<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.AsGraphTraversal().HasValue(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the id step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, object> Id<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().Id();
        }

        /// <summary>
        /// Adds the identity step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Identity<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().Identity().AsSchemaBound();
        }

        /// <summary>
        /// Adds the in step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> In<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] edgeLabels)
            where E : Structure.IVertex
        {
            return traversal.AsGraphTraversal().In(edgeLabels);
        }

        /// <summary>
        /// Adds the inE step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> InE<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] edgeLabels)
            where E : Structure.IVertex
        {
            return traversal.AsGraphTraversal().InE(edgeLabels);
        }

        /// <summary>
        /// Adds the inject step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Inject<S, E>(this ISchemaBoundTraversal<S, E> traversal, params E[] injections)
        {
            return traversal.AsGraphTraversal().Inject(injections).AsSchemaBound();
        }

        /// <summary>
        /// Adds the is step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Is<S, E>(this ISchemaBoundTraversal<S, E> traversal, object value)
        {
            return traversal.AsGraphTraversal().Is(value).AsSchemaBound();
        }

        /// <summary>
        /// Adds the is step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Is<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.AsGraphTraversal().Is(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the key step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, string> Key<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().Key();
        }

        /// <summary>
        /// Adds the label step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, string> Label<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().Label();
        }

        /// <summary>
        /// Adds the limit step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Limit<S, E>(this ISchemaBoundTraversal<S, E> traversal, Scope scope, long limit)
        {
            return traversal.AsGraphTraversal().Limit<E>(scope, limit).AsSchemaBound();
        }

        /// <summary>
        /// Adds the limit step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Limit<S, E>(this ISchemaBoundTraversal<S, E> traversal, long limit)
        {
            return traversal.AsGraphTraversal().Limit<E>(limit).AsSchemaBound();
        }

        /// <summary>
        /// Adds the loops step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, int> Loops<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().Loops();
        }

        /// <summary>
        /// Adds the math step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, double> Math<S, E>(this ISchemaBoundTraversal<S, E> traversal, string expression)
        {
            return traversal.AsGraphTraversal().Math(expression);
        }

        /// <summary>
        /// Adds the not step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Not<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal notTraversal)
        {
            return traversal.AsGraphTraversal().Not(notTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the option step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Option<S, E>(this ISchemaBoundTraversal<S, E> traversal, object pickToken, ITraversal traversalOption)
        {
            return traversal.AsGraphTraversal().Option(pickToken, traversalOption).AsSchemaBound();
        }

        /// <summary>
        /// Adds the option step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Option<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal traversalOption)
        {
            return traversal.AsGraphTraversal().Option(traversalOption).AsSchemaBound();
        }

        /// <summary>
        /// Adds the or step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Or<S, E>(this ISchemaBoundTraversal<S, E> traversal, params ITraversal[] orTraversals)
        {
            return traversal.AsGraphTraversal().Or(orTraversals).AsSchemaBound();
        }

        /// <summary>
        /// Adds the order step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Order<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().Order().AsSchemaBound();
        }

        /// <summary>
        /// Adds the order step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Order<S, E>(this ISchemaBoundTraversal<S, E> traversal, Scope scope)
        {
            return traversal.AsGraphTraversal().Order(scope).AsSchemaBound();
        }

        /// <summary>
        /// Adds the otherV step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> OtherV<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().OtherV();
        }

        /// <summary>
        /// Adds the out step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> Out<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] edgeLabels)
            where E : Structure.IEdge
        {
            return traversal.AsGraphTraversal().Out(edgeLabels);
        }

        /// <summary>
        /// Adds the outE step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> OutE<S, E>(this ISchemaBoundTraversal<S, E> traversal, params string[] edgeLabels)
            where E : Structure.IVertex
        {
            return traversal.AsGraphTraversal().OutE(edgeLabels);
        }

        /// <summary>
        /// Adds the pageRank step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> PageRank<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().PageRank().AsSchemaBound();
        }

        /// <summary>
        /// Adds the pageRank step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> PageRank<S, E>(this ISchemaBoundTraversal<S, E> traversal, double alpha)
        {
            return traversal.AsGraphTraversal().PageRank(alpha).AsSchemaBound();
        }

        /// <summary>
        /// Adds the path step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Path> Path<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().Path();
        }

        /// <summary>
        /// Adds the peerPressure step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> PeerPressure<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().PeerPressure().AsSchemaBound();
        }

        /// <summary>
        /// Adds the profile step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Profile<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.AsGraphTraversal().Profile(sideEffectKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the program step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Program<S, E>(this ISchemaBoundTraversal<S, E> traversal, object vertexProgram)
        {
            return traversal.AsGraphTraversal().Program(vertexProgram).AsSchemaBound();
        }

        /// <summary>
        /// Adds the property step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Property<S, E>(this ISchemaBoundTraversal<S, E> traversal, Cardinality cardinality, object key, object value, params object[] keyValues)
        {
            return traversal.AsGraphTraversal().Property(cardinality, key, value, keyValues).AsSchemaBound();
        }

        /// <summary>
        /// Adds the property step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Property<S, E>(this ISchemaBoundTraversal<S, E> traversal, object key, object value, params object[] keyValues)
        {
            return traversal.AsGraphTraversal().Property(key, value, keyValues).AsSchemaBound();
        }

        /// <summary>
        /// Adds the range step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Range<S, E>(this ISchemaBoundTraversal<S, E> traversal, Scope scope, long low, long high)
        {
            return traversal.AsGraphTraversal().Range<E>(scope, low, high).AsSchemaBound();
        }

        /// <summary>
        /// Adds the range step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Range<S, E>(this ISchemaBoundTraversal<S, E> traversal, long low, long high)
        {
            return traversal.AsGraphTraversal().Range<E>(low, high).AsSchemaBound();
        }

        /// <summary>
        /// Adds the repeat step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Repeat<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal repeatTraversal)
        {
            return traversal.AsGraphTraversal().Repeat(repeatTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the sack step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Sack<S, E>(this ISchemaBoundTraversal<S, E> traversal, IBiFunction sackOperator)
        {
            return traversal.AsGraphTraversal().Sack(sackOperator).AsSchemaBound();
        }

        /// <summary>
        /// Adds the sample step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Sample<S, E>(this ISchemaBoundTraversal<S, E> traversal, Scope scope, int amountToSample)
        {
            return traversal.AsGraphTraversal().Sample(scope, amountToSample).AsSchemaBound();
        }

        /// <summary>
        /// Adds the sample step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Sample<S, E>(this ISchemaBoundTraversal<S, E> traversal, int amountToSample)
        {
            return traversal.AsGraphTraversal().Sample(amountToSample).AsSchemaBound();
        }

        /// <summary>
        /// Adds the sideEffect step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> SideEffect<S, E>(this ISchemaBoundTraversal<S, E> traversal, IConsumer consumer)
        {
            return traversal.AsGraphTraversal().SideEffect(consumer).AsSchemaBound();
        }

        /// <summary>
        /// Adds the sideEffect step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> SideEffect<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal sideEffectTraversal)
        {
            return traversal.AsGraphTraversal().SideEffect(sideEffectTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the simplePath step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> SimplePath<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().SimplePath().AsSchemaBound();
        }

        /// <summary>
        /// Adds the skip step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Skip<S, E>(this ISchemaBoundTraversal<S, E> traversal, Scope scope, long skip)
        {
            return traversal.AsGraphTraversal().Skip<E>(scope, skip).AsSchemaBound();
        }

        /// <summary>
        /// Adds the skip step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Skip<S, E>(this ISchemaBoundTraversal<S, E> traversal, long skip)
        {
            return traversal.AsGraphTraversal().Skip<E>(skip).AsSchemaBound();
        }

        /// <summary>
        /// Adds the store step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Store<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.AsGraphTraversal().Store(sideEffectKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the subgraph step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> Subgraph<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.AsGraphTraversal().Subgraph(sideEffectKey);
        }

        /// <summary>
        /// Adds the timeLimit step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> TimeLimit<S, E>(this ISchemaBoundTraversal<S, E> traversal, long timeLimit)
        {
            return traversal.AsGraphTraversal().TimeLimit(timeLimit).AsSchemaBound();
        }

        /// <summary>
        /// Adds the times step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Times<S, E>(this ISchemaBoundTraversal<S, E> traversal, int maxLoops)
        {
            return traversal.AsGraphTraversal().Times(maxLoops).AsSchemaBound();
        }

        /// <summary>
        /// Adds the to step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> To<S, E>(this ISchemaBoundTraversal<S, E> traversal, Direction direction, params string[] edgeLabels)
        {
            return traversal.AsGraphTraversal().To(direction, edgeLabels);
        }

        /// <summary>
        /// Adds the to step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> To<S, E>(this ISchemaBoundTraversal<S, E> traversal, string toStepLabel)
        {
            return traversal.AsGraphTraversal().To(toStepLabel).AsSchemaBound();
        }

        /// <summary>
        /// Adds the to step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> To<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal toVertex)
        {
            return traversal.AsGraphTraversal().To(toVertex).AsSchemaBound();
        }

        /// <summary>
        /// Adds the to step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> To<S, E>(this ISchemaBoundTraversal<S, E> traversal, Vertex toVertex)
        {
            return traversal.AsGraphTraversal().To(toVertex).AsSchemaBound();
        }

        /// <summary>
        /// Adds the toE step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Edge> ToE<S, E>(this ISchemaBoundTraversal<S, E> traversal, Direction direction, params string[] edgeLabels)
        {
            return traversal.AsGraphTraversal().ToE(direction, edgeLabels);
        }

        /// <summary>
        /// Returns the string-equivalent of the given traversal
        /// </summary>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the string query</returns>
        public static string ToGremlinQuery<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return traversal.AsGraphTraversal().ToGremlinQuery();
        }

        /// <summary>
        /// Adds the toV step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static GraphTraversal<S, Vertex> ToV<S, E>(this ISchemaBoundTraversal<S, E> traversal, Direction direction)
        {
            return traversal.AsGraphTraversal().ToV(direction);
        }

        /// <summary>
        /// Adds the tree step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Tree<S, E>(this ISchemaBoundTraversal<S, E> traversal, string sideEffectKey)
        {
            return traversal.AsGraphTraversal().Tree(sideEffectKey).AsSchemaBound();
        }

        /// <summary>
        /// Adds the until step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Until<S, E>(this ISchemaBoundTraversal<S, E> traversal, P untilPredicate)
        {
            return traversal.AsGraphTraversal().Until(untilPredicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the until step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Until<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal untilTraversal)
        {
            return traversal.AsGraphTraversal().Until(untilTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Adds the where step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Where<S, E>(this ISchemaBoundTraversal<S, E> traversal, P predicate)
        {
            return traversal.AsGraphTraversal().Where(predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the where step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Where<S, E>(this ISchemaBoundTraversal<S, E> traversal, string startKey, P predicate)
        {
            return traversal.AsGraphTraversal().Where(startKey, predicate).AsSchemaBound();
        }

        /// <summary>
        /// Adds the where step to this <see cref="GraphTraversal{SType, EType}"/>.
        /// </summary>
        public static ISchemaBoundTraversal<S, E> Where<S, E>(this ISchemaBoundTraversal<S, E> traversal, ITraversal whereTraversal)
        {
            return traversal.AsGraphTraversal().Where(whereTraversal).AsSchemaBound();
        }

        /// <summary>
        /// Casts the schema-bound traversal to a gremlin graph traversal.
        /// </summary>
        /// <typeparam name="S">The source of the traversal</typeparam>
        /// <typeparam name="E">The type of the current element</typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the traversal</returns>
        internal static GraphTraversal<S, E> AsGraphTraversal<S, E>(this ISchemaBoundTraversal<S, E> traversal)
        {
            return new GraphTraversal<S, E>(new ITraversalStrategy[0], traversal.Bytecode);
        }
    }
}