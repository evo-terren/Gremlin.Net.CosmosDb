using System;
using System.Threading.Tasks;
using Gremlin.Net.CosmosDb.Serialization;
using Gremlin.Net.CosmosDb.Structure;
using Gremlin.Net.Driver;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Cosmos Db graph client that can be used for querying
    /// </summary>
    /// <seealso cref="Gremlin.Net.CosmosDb.IGraphClient"/>
    /// <seealso cref="System.IDisposable"/>
    public class GraphClient : IGraphClient, IDisposable
    {
        private readonly IGremlinClient _gremlinClient;

        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphClient"/> class.
        /// </summary>
        /// <param name="gremlinHostname">The hostname.</param>
        /// <param name="databaseName">Name of the database (case-sensitive).</param>
        /// <param name="graphName">Name of the graph.</param>
        /// <param name="accessKey">The access key.</param>
        public GraphClient(string gremlinHostname, string databaseName, string graphName, string accessKey,
            int port = 443, bool useSSL = true)
        {
            var server = new GremlinServer(gremlinHostname, port, useSSL, $"/dbs/{databaseName}/colls/{graphName}", accessKey);

            _gremlinClient = new GremlinClient(server, new GraphSONJTokenReader(), mimeType: GremlinClient.GraphSON2MimeType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphClient"/> class.
        /// </summary>
        /// <param name="gremlinClient">The gremlin client.</param>
        /// <exception cref="ArgumentNullException">gremlinClient</exception>
        public GraphClient(IGremlinClient gremlinClient)
        {
            _gremlinClient = gremlinClient ?? throw new ArgumentNullException(nameof(gremlinClient));
        }

        /// <summary>
        /// Creates the traversal source (g).
        /// </summary>
        /// <returns>Returns the traversal source</returns>
        public IGraphTraversalSource CreateTraversalSource()
        {
            return new GraphTraversalSource();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Submits the given gremlin query to the Cosmos Db instance and returns the results.
        /// </summary>
        /// <param name="gremlinQuery">The gremlin query.</param>
        /// <returns>Returns the results</returns>
        /// <exception cref="ArgumentNullException">gremlinQuery</exception>
        public async Task<GraphResult> QueryAsync(string gremlinQuery)
        {
            if (gremlinQuery == null)
                throw new ArgumentNullException(nameof(gremlinQuery));

            var resultSet = await _gremlinClient.SubmitAsync<JToken>(gremlinQuery);

            return new GraphResult(resultSet);
        }

        /// <summary>
        /// Submits the given gremlin query to the Cosmos Db instance.
        /// </summary>
        /// <param name="gremlinQuery">The gremlin query.</param>
        /// <returns>Returns the results</returns>
        /// <exception cref="ArgumentNullException">gremlinQuery</exception>
        [Obsolete("Renamed to QueryAsync")]
        public Task<GraphResult> SubmitAsync(string gremlinQuery)
        {
            return QueryAsync(gremlinQuery);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    _gremlinClient.Dispose();

                disposedValue = true;
            }
        }
    }
}