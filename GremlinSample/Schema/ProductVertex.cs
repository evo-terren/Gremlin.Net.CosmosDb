using Gremlin.Net.CosmosDb.Structure;

namespace GremlinSample.Schema
{
    [Label("product")]
    public sealed class ProductVertex : Vertex
    {
        public PersonPurchasedProductEdge PurchasedBy { get; }
    }
}