using Gremlin.Net.CosmosDb;
using GremlinSample.Schema;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace GremlinSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Run().Wait();

            Console.WriteLine();
            Console.WriteLine("All done...");
            Console.ReadKey();
        }

        private static async Task Run()
        {
            using (var graphClient = new GraphClient("your-gremlin-host-name", "your-db-name", "your-graph-name", "your-access-key"))
            {
                var g = graphClient.CreateTraversalSource();

                var query = g.V<PersonVertex>("1").Property(v => v.Properties.Name, "test").Property(v => v.Properties.Ages, new[] { 5, 6 });
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