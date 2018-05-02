using Gremlin.Net.Process.Traversal;
using System.Collections.Generic;

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
        /// Gets the bytecode.
        /// </summary>
        public Bytecode Bytecode
        {
            get { return _traversal.Bytecode; }
        }

        /// <summary>
        /// Gets the current.
        /// </summary>
        public object Current => _traversal.Current;

        /// <summary>
        /// Gets or sets the <see cref="ITraversalSideEffects"/> of this traversal.
        /// </summary>
        public ITraversalSideEffects SideEffects
        {
            get { return _traversal.SideEffects; }
            set { _traversal.SideEffects = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="Traverser"/>'s of this traversal that hold the results of the traversal.
        /// </summary>
        public IEnumerable<Traverser> Traversers
        {
            get { return _traversal.Traversers; }
            set { _traversal.Traversers = value; }
        }

        private readonly GraphTraversal<S, E> _traversal;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaBoundTraversal{S, E}"/> class.
        /// </summary>
        /// <param name="bytecode">The bytecode.</param>
        public SchemaBoundTraversal(Bytecode bytecode)
        {
            _traversal = new GraphTraversal<S, E>(new ITraversalStrategy[0], bytecode);
        }

        /// <summary>
        /// Iterates this instance.
        /// </summary>
        /// <returns></returns>
        public ITraversal Iterate()
        {
            return _traversal.Iterate();
        }

        /// <summary>
        /// Moves the next.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            return _traversal.MoveNext();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            _traversal.Reset();
        }
    }
}