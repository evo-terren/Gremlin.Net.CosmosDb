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

                var query = g.V("1").AsVertex<PersonVertex>()
                             .Out(s => s.Purchases)
                             .InE(s => s.People)
                             .OutV()
                             .Property(v => v.Name, "test").Property(v => v.Ages, new[] { 5, 6 });
                Console.WriteLine(query.ToGremlinQuery());
                var response = await graphClient.SubmitAsync(query);

                Console.WriteLine();
                Console.WriteLine("Response:");
                foreach (var result in response)
                {
                    var json = JsonConvert.SerializeObject(result);
                    Console.WriteLine(json);
                }
            }

            Console.WriteLine();
            Console.WriteLine("All done...");
            Console.ReadKey();
        }
    }
}