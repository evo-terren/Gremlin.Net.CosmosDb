using Gremlin.Net.CosmosDb.Structure;

namespace GremlinSample.Schema
{
    [Label("purchased")]
    public sealed class StorePurchasedProductEdge : ManyToManyEdge<StoreVertex, ProductVertex>
    {
        public string Id { get; set; }
    }
}