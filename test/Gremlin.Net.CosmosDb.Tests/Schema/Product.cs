using Gremlin.Net.CosmosDb.Structure;

namespace Gremlin.Net.CosmosDb.Schema
{
    public class Product : IVertex
    {
        public string Name { get; set; }
        public int Price { get; set; }

        [Label("purchased")]
        public ManyToManyEdge<Person, Product> PurchasedBy { get; set; }
    }
}
