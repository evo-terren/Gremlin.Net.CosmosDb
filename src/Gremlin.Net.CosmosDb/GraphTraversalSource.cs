using Gremlin.Net.Process.Traversal;
using Gremlin.Net.Structure;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// A graph traversal source (g)
    /// </summary>
    /// <seealso cref="Gremlin.Net.CosmosDb.IGraphTraversalSource"/>
    public sealed class GraphTraversalSource : IGraphTraversalSource
    {
        private readonly Process.Traversal.GraphTraversalSource _graphTraversalSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphTraversalSource"/> class.
        /// </summary>
        public GraphTraversalSource()
        {
            _graphTraversalSource = new Graph().Traversal();
        }

        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and
        /// adds the addE step to that traversal.
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public GraphTraversal<Gremlin.Net.Structure.Edge, Gremlin.Net.Structure.Edge> AddE(string label)
        {
            return _graphTraversalSource.AddE(label);
        }

        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and
        /// adds the addE step to that traversal.
        /// </summary>
        /// <param name="edgeLabelTraversal"></param>
        /// <returns></returns>
        public GraphTraversal<Gremlin.Net.Structure.Edge, Gremlin.Net.Structure.Edge> AddE(ITraversal edgeLabelTraversal)
        {
            return _graphTraversalSource.AddE(edgeLabelTraversal);
        }

        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and
        /// adds the addV step to that traversal.
        /// </summary>
        /// <returns></returns>
        public GraphTraversal<Gremlin.Net.Structure.Vertex, Gremlin.Net.Structure.Vertex> AddV()
        {
            return _graphTraversalSource.AddV();
        }

        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and
        /// adds the addV step to that traversal.
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public GraphTraversal<Gremlin.Net.Structure.Vertex, Gremlin.Net.Structure.Vertex> AddV(string label)
        {
            return _graphTraversalSource.AddV(label);
        }

        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and
        /// adds the addV step to that traversal.
        /// </summary>
        /// <param name="vertexLabelTraversal"></param>
        /// <returns></returns>
        public GraphTraversal<Gremlin.Net.Structure.Vertex, Gremlin.Net.Structure.Vertex> AddV(ITraversal vertexLabelTraversal)
        {
            return _graphTraversalSource.AddV(vertexLabelTraversal);
        }

        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and
        /// adds the E step to that traversal.
        /// </summary>
        /// <param name="edgesIds"></param>
        /// <returns></returns>
        public GraphTraversal<Gremlin.Net.Structure.Edge, Gremlin.Net.Structure.Edge> E(params object[] edgesIds)
        {
            return _graphTraversalSource.E(edgesIds);
        }

        /// <summary>
        /// Spawns a <see cref="GraphTraversal{SType, EType}"/> off this graph traversal source and
        /// adds the V step to that traversal.
        /// </summary>
        /// <param name="vertexIds"></param>
        /// <returns></returns>
        public GraphTraversal<Gremlin.Net.Structure.Vertex, Gremlin.Net.Structure.Vertex> V(params object[] vertexIds)
        {
            return _graphTraversalSource.V(vertexIds);
        }
    }
}