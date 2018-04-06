using Gremlin.Net.CosmosDb.Structure;

namespace GremlinSample.Schema
{
    [Label("person")]
    public class PersonVertex : Vertex<PersonModel>
    {
        public PersonPurchasedProductEdge Purchased { get; private set; }
    }
}