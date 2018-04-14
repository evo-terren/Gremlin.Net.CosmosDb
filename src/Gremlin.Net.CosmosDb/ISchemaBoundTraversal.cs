using Gremlin.Net.Process.Traversal;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Traversal that is bound to a specific schema
    /// </summary>
    /// <typeparam name="S">The source of the traversal</typeparam>
    /// <typeparam name="E">The type of the current element</typeparam>
    public interface ISchemaBoundTraversal<S, out E> : ISchemaBoundTraversal
    {
    }

    /// <summary>
    /// Traversal that is bound to a specific schema
    /// </summary>
    public interface ISchemaBoundTraversal
    {
        /// <summary>
        /// Gets the <see cref="Gremlin.Net.Process.Traversal.Bytecode"/> representation of this traversal.
        /// </summary>
        Bytecode Bytecode { get; }
    }
}