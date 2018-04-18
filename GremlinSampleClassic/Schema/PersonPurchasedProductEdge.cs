using Gremlin.Net.CosmosDb.Structure;

namespace GremlinSampleClassic.Schema
{
    [Label("purchased")]
    public sealed class PersonPurchasedProductEdge : EdgeBase<PersonVertex, ProductVertex>
    {
    }
}