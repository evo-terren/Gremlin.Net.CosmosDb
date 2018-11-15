using Gremlin.Net.CosmosDb;
using GremlinSample.Schema;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace GremlinSample
{
    internal class Program
    {
        internal static async Task Main()
        {
            using (var graphClient = new GraphClient("your-gremlin-host-name", "your-db-name", "your-graph-name", "your-access-key"))
            {
                var g = graphClient.CreateTraversalSource();

                //add vertices/edges using strongly-typed objects
                var personV = new PersonVertex
                {
                    Ages = new[] { 4, 6, 23 },
                    Id = "person-12345",
                    Name = "my name"
                };
                var purchasedE = new PersonPurchasedProductEdge
                {
                    Id = "person-12345_purchased_product-12345"
                };
                var productV = new ProductVertex
                {
                    Id = "product-12345"
                };
                var test = g.AddV(personV).As("person")
                            .AddV(productV).As("product")
                            .AddE(purchasedE).From("person").To("product");

                Console.WriteLine(test.ToGremlinQuery());

                //traverse vertices/edges with strongly-typed objects
                var query = g.V("1").Cast<PersonVertex>()
                             .Out(s => s.Purchases)
                             .InE(s => s.People)
                             .OutV()
                             .Property(v => v.Name, "test")
                             .Property(v => v.Ages, new[] { 5, 6 })
                             .Property(v => v.Ages, 7);
                Console.WriteLine(query.ToGremlinQuery());
                var response = await graphClient.QueryAsync(query);

                Console.WriteLine();
                Console.WriteLine("Response status:");

                Console.WriteLine($"Code: {response.StatusCode}");
                Console.WriteLine($"RU Cost: {response.TotalRequestCharge}");

                Console.WriteLine();
                Console.WriteLine("Response:");
                foreach (var result in response)
                {
                    var json = JsonConvert.SerializeObject(result, Formatting.Indented);

                    Console.WriteLine(json);
                }
            }

            Console.WriteLine();
            Console.WriteLine("All done...");
            Console.ReadKey();
        }
    }
}