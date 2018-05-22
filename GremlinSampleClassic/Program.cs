using Gremlin.Net.CosmosDb;
using GremlinSampleClassic.Schema;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace GremlinSampleClassic
{
    internal class Program
    {
        private static void Main()
        {
            using (var graphClient = new GraphClient("your-gremlin-host-name", "your-db-name", "your-graph-name", "your-access-key"))
            {
                var g = graphClient.CreateTraversalSource();

                var query = g.V<PersonVertex>("1")
                             .Out(s => s.Purchases)
                             .InE(s => s.People)
                             .OutV()
                             .Property(v => v.Name, "test").Property(v => v.Ages, new[] { 5, 6 });
                Console.WriteLine(query.ToGremlinQuery());
                var response = graphClient.SubmitAsync(query);
                response.Wait();
                Console.WriteLine();
                Console.WriteLine("Response:");
                foreach (var result in response.Result)
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