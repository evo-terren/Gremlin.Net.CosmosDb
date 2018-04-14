using Gremlin.Net.CosmosDb.Structure;

namespace GremlinSample.Schema
{
    [Label("person")]
    public class PersonVertex : VertexBase
    {
        public int[] Ages { get; set; }

        public string Name { get; set; }

        public PersonPurchasedProductEdge Purchases { get; }
    }
}