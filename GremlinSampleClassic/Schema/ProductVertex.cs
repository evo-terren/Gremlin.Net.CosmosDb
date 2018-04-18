using Gremlin.Net.CosmosDb.Structure;

namespace GremlinSampleClassic.Schema
{
    [Label("product")]
    public sealed class ProductVertex : VertexBase
    {
        public PersonPurchasedProductEdge People { get; }
    }
}