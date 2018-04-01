using Gremlin.Net.CosmosDb.Structure;

namespace GremlinSample.Schema
{
    [Label("product")]
    public sealed class ProductVertex
    {
        public PersonPurchasedProductEdge PurchasedBy { get; }
    }
}