using Gremlin.Net.CosmosDb.Structure;

namespace GremlinSample.Schema
{
    [Label("person")]
    public sealed class PersonVertex : Vertex
    {
        public PersonPurchasedProductEdge Purchased { get; private set; }
    }
}