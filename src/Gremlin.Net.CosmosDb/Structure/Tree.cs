namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// A parsed traversal result in tree representation.
    /// E.g. "g.V().hasLabel('person').outE('purchased').inV().tree()"
    /// </summary>
    public class Tree
    {
        /// <summary>
        /// The root vertices of the tree traversal.
        /// </summary>
        public TreeVertexNode[] RootVertexNodes { get; set; }
    }
}
