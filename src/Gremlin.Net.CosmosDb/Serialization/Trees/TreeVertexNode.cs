using Gremlin.Net.CosmosDb.Structure;
using Newtonsoft.Json;

namespace Gremlin.Net.CosmosDb.Serialization.Trees
{
    public class TreeVertexNode
    {
        [JsonProperty("key")]
        public Vertex Vertex { get; set; }

        [JsonProperty("value")]
        public TreeEdgeNode[] EdgeNodes { get; set; }

        public override string ToString() => Vertex.ToString() + " Edges: " + EdgeNodes.Length;
    }
}
