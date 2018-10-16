using Gremlin.Net.CosmosDb.Structure;
using Newtonsoft.Json;

namespace Gremlin.Net.CosmosDb.Serialization.Trees
{
    public class TreeEdgeNode
    {
        [JsonProperty("key")]
        public Edge Edge { get; set; }

        [JsonProperty("value")]
        public TreeVertexNode VertexNode { get; set; }

        public override string ToString() => Edge.ToString();
    }
}
