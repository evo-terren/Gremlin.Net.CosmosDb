using Gremlin.Net.Process.Traversal;
using System;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Traversal that is bound to a specific schema
    /// </summary>
    /// <typeparam name="S">The source of the traversal</typeparam>
    /// <typeparam name="E">The type of the current element</typeparam>
    internal sealed class SchemaBoundTraversal<S, E> : ISchemaBoundTraversal<S, E>
    {
        /// <summary>
        /// Gets the <see cref="T:Gremlin.Net.Process.Traversal.Bytecode"/> representation of this traversal.
        /// </summary>
        public Bytecode Bytecode { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaBoundTraversal{S, E}"/> class.
        /// </summary>
        /// <param name="bytecode">The bytecode.</param>
        /// <exception cref="ArgumentNullException">bytecode</exception>
        public SchemaBoundTraversal(Bytecode bytecode)
        {
            if (bytecode == null)
                throw new ArgumentNullException(nameof(bytecode));

            Bytecode = bytecode;
        }
    }
}