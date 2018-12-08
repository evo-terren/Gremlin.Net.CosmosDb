using Gremlin.Net.CosmosDb.Structure;

namespace GremlinSample.Schema
{
    [Label("product")]
    public sealed class ProductVertex : IVertex
    {
        public string Id { get; set; }
        public PersonPurchasedProductEdge People { get; }
        public StorePurchasedProductEdge Store { get; }
    }
}