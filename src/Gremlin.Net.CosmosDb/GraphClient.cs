using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gremlin.Net.CosmosDb.Serialization;
using Gremlin.Net.CosmosDb.Structure;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
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
        private readonly GraphSONReader _graphSONReader;

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

            _graphSONReader = new GraphSONJTokenReader();
            _gremlinClient = new GremlinClient(server, _graphSONReader, mimeType: GremlinClient.GraphSON2MimeType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphClient"/> class.
        /// </summary>
        /// <param name="gremlinClient">The gremlin client.</param>
        /// <exception cref="ArgumentNullException">gremlinClient</exception>
        internal GraphClient(IGremlinClient gremlinClient)
        {
            _graphSONReader = new GraphSONJTokenReader();
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

            var rawResultSet = await _gremlinClient.SubmitAsync<JToken>(gremlinQuery);

            // Because SubmitAsync() above handles JToken differently from all other types, we
            // have to do the normal processing (that it would do for any other type) here.
            var resultList = new List<JToken>();
            foreach (var rawJToken in rawResultSet)
            {
                var processedJToken = (JToken)this._graphSONReader.ToObject(rawJToken);
                foreach (var d in processedJToken)
                {
                    resultList.Add(d);
                }
            }
            var newResultSet = new ResultSet<JToken>(resultList, rawResultSet.StatusAttributes);

            return new GraphResult(newResultSet);
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