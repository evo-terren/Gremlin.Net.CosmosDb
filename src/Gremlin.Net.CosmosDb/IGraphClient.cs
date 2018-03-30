using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Cosmos Db graph client that can be used for querying and mutation of the graph
    /// </summary>
    public interface IGraphClient
    {
        /// <summary>
        /// Creates the traversal source (g).
        /// </summary>
        /// <returns>Returns the traversal source</returns>
        IGraphTraversalSource CreateTraversalSource();

        /// <summary>
        /// Submits the given gremlin query to the Cosmos Db instance.
        /// </summary>
        /// <param name="gremlinQuery">The gremlin query.</param>
        /// <returns>Returns the results</returns>
        Task<IReadOnlyCollection<JToken>> SubmitAsync(string gremlinQuery);
    }
}