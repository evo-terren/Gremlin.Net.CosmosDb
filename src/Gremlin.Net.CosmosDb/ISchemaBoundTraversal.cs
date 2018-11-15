using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Traversal that is bound to a specific schema
    /// </summary>
    /// <typeparam name="S">The source of the traversal</typeparam>
    /// <typeparam name="E">The type of the current element</typeparam>
    /// <remarks>
    /// This is only necessary to have a covariant type I can bind to in order to support inherited vertex/edge types.
    /// <see cref="Gremlin.Net.Process.Traversal.GraphTraversal{S, E}"/> is not covariant, yet defines all the traversal
    /// methods (unfortunately for me)...
    /// </remarks>
    public interface ISchemaBoundTraversal<S, out E> : ITraversal
    {
    }
}