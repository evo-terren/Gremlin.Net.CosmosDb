using Gremlin.Net.CosmosDb.Structure;

namespace GremlinSample.Schema
{
    [Label("store")]
    public class StoreVertex : IVertex
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public StorePurchasedProductEdge Purchases { get; }
    }
}