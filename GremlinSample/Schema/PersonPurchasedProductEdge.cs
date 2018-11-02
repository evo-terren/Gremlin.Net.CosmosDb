using Gremlin.Net.CosmosDb.Structure;

namespace GremlinSample.Schema
{
    [Label("purchased")]
    public sealed class PersonPurchasedProductEdge : ManyToManyEdge<PersonVertex, ProductVertex>
    {
        public string Id { get; set; }
    }
}