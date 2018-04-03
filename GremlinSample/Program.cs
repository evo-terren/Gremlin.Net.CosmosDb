using Gremlin.Net.CosmosDb;
using GremlinSample.Schema;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace GremlinSample
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            using (var graphClient = new GraphClient("graph-test-bplehal.gremlin.cosmosdb.azure.com", "evo", "CMS", "6agjPRz3wZlwdErfasKon2kbNug0z75fVdfL53uuaG7OTZxOxqoyvId3irOCDJgfkgxUwlZ3qV5aNLLBSGWy1w=="))
            {
                var g = graphClient.CreateTraversalSource();

                var query = g.V<PersonVertex>("1").Out(v => v.Purchased);
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