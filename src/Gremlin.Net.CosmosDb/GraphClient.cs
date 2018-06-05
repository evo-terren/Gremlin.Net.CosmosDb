﻿using Gremlin.Net.CosmosDb.Serialization;
using Gremlin.Net.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// <param name="port">The port.</param>
        /// <param name="databaseName">Name of the database (case-sensitive).</param>
        /// <param name="graphName">Name of the graph.</param>
        /// <param name="accessKey">The access key.</param>
        public GraphClient(string gremlinHostname, int port, string databaseName, string graphName, string accessKey)
        {
            var server = new GremlinServer(gremlinHostname, port, true, $"/dbs/{databaseName}/colls/{graphName}", accessKey);

            _gremlinClient = new GremlinClient(server, new GraphSONJTokenReader(), mimeType: GremlinClient.GraphSON2MimeType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphClient"/> class.
        /// </summary>
        /// <param name="gremlinHostname">The hostname.</param>
        /// <param name="databaseName">Name of the database (case-sensitive).</param>
        /// <param name="graphName">Name of the graph.</param>
        /// <param name="accessKey">The access key.</param>
        public GraphClient(string gremlinHostname, string databaseName, string graphName, string accessKey) : this(gremlinHostname, 443, databaseName, graphName, accessKey) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphClient"/> class.
        /// </summary>
        /// <param name="gremlinClient">The gremlin client.</param>
        /// <exception cref="ArgumentNullException">gremlinClient</exception>
        internal GraphClient(IGremlinClient gremlinClient)
        {
            if (gremlinClient == null)
                throw new ArgumentNullException(nameof(gremlinClient));

            _gremlinClient = gremlinClient;
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
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Submits the given gremlin query to the Cosmos Db instance.
        /// </summary>
        /// <param name="gremlinQuery">The gremlin query.</param>
        /// <returns>Returns the results</returns>
        /// <exception cref="ArgumentNullException">gremlinQuery</exception>
        public Task<IReadOnlyCollection<JToken>> SubmitAsync(string gremlinQuery)
        {
            if (gremlinQuery == null)
                throw new ArgumentNullException(nameof(gremlinQuery));

            return _gremlinClient.SubmitAsync<JToken>(gremlinQuery);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        /// unmanaged resources.
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