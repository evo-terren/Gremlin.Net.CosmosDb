using Gremlin.Net.CosmosDb.Structure;

namespace Gremlin.Net.CosmosDb.Schema
{
    public class Person : IVertex
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Label("purchased")]
        public ManyToManyEdge<Person, Product> Purchased { get; set; }
    }
}
