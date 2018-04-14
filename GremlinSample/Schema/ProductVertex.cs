using Gremlin.Net.CosmosDb.Structure;

namespace GremlinSample.Schema
{
    [Label("product")]
    public sealed class ProductVertex : VertexBase
    {
        public PersonPurchasedProductEdge People { get; }
    }
}