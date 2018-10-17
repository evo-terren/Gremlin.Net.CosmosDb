using Newtonsoft.Json;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// An node with a vertex and all it's edges in tree representation.
    /// </summary>
    public class TreeVertexNode
    {
        /// <summary>
        /// The vertex.
        /// </summary>
        [JsonProperty("key")]
        public Vertex Vertex { get; set; }

        /// <summary>
        /// The edges. Can be a mixture of outbound and inbound edges depending on the traversal.
        /// </summary>
        [JsonProperty("value")]
        public TreeEdgeNode[] EdgeNodes { get; set; }

        public override string ToString() => Vertex.ToString() + " Edges: " + EdgeNodes.Length;
    }
}
