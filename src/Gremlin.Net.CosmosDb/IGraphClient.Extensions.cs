using Gremlin.Net.CosmosDb.Serialization;
using Gremlin.Net.CosmosDb.Structure;
using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Helper extension methods for the <see cref="Gremlin.Net.CosmosDb.IGraphClient"/> object
    /// </summary>
    public static class IGraphClientExensions
    {
        /// <summary>
        /// Submits the given query to the <see cref="Gremlin.Net.CosmosDb.IGraphClient"/>
        /// and returns the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graphClient">The graph client.</param>
        /// <param name="gremlinQuery">The traversal query.</param>
        /// <returns>Returns the result</returns>
        /// <exception cref="ArgumentNullException">traversal</exception>
        public static Task<IReadOnlyCollection<T>> SubmitAsync<T>(this IGraphClient graphClient, string gremlinQuery)
        {
            return graphClient.SubmitAsync<T>(gremlinQuery, BuildDefaultSerializerSettings());
        }

        /// <summary>
        /// Submits the given traversal query to the <see cref="Gremlin.Net.CosmosDb.IGraphClient"/>
        /// and returns the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graphClient">The graph client.</param>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the result</returns>
        /// <exception cref="ArgumentNullException">traversal</exception>
        public static Task<IReadOnlyCollection<T>> SubmitAsync<T>(this IGraphClient graphClient, ITraversal traversal)
        {
            return graphClient.SubmitAsync<T>(traversal, BuildDefaultSerializerSettings());
        }

        /// <summary>
        /// Submits the given traversal query to the <see cref="Gremlin.Net.CosmosDb.IGraphClient"/>
        /// and returns the result
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="graphClient">The graph client.</param>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the result</returns>
        /// <exception cref="ArgumentNullException">traversal</exception>
        public static Task<IReadOnlyCollection<E>> SubmitAsync<S, E>(this IGraphClient graphClient, ITraversal<S, E> traversal)
        {
            return graphClient.SubmitAsync(traversal, BuildDefaultSerializerSettings());
        }

        /// <summary>
        /// Submits the given traversal query to the <see cref="Gremlin.Net.CosmosDb.IGraphClient"/>
        /// and returns the result
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="graphClient">The graph client.</param>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the result</returns>
        /// <exception cref="ArgumentNullException">traversal</exception>
        public static Task<IReadOnlyCollection<E>> SubmitAsync<S, E>(this IGraphClient graphClient, ISchemaBoundTraversal<S, E> traversal)
        {
            return graphClient.SubmitAsync(traversal, BuildDefaultSerializerSettings());
        }

        /// <summary>
        /// Submits the given traversal query to the <see cref="Gremlin.Net.CosmosDb.IGraphClient"/>
        /// and returns the result
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="graphClient">The graph client.</param>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the result</returns>
        /// <exception cref="ArgumentNullException">traversal</exception>
        public static Task<IReadOnlyCollection<Vertex>> SubmitAsync<S>(this IGraphClient graphClient, ITraversal<S, Gremlin.Net.Structure.Vertex> traversal)
        {
            return graphClient.SubmitAsync<Vertex>(traversal);
        }

        /// <summary>
        /// Submits the given traversal query to the <see cref="Gremlin.Net.CosmosDb.IGraphClient"/>
        /// and returns the result
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="graphClient">The graph client.</param>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the result</returns>
        /// <exception cref="ArgumentNullException">traversal</exception>
        public static Task<IReadOnlyCollection<Edge>> SubmitAsync<S>(this IGraphClient graphClient, ITraversal<S, Gremlin.Net.Structure.Edge> traversal)
        {
            return graphClient.SubmitAsync<Edge>(traversal);
        }

        /// <summary>
        /// Submits the given traversal query to the <see cref="Gremlin.Net.CosmosDb.IGraphClient"/>
        /// and returns the result
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="graphClient">The graph client.</param>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the result</returns>
        /// <exception cref="ArgumentNullException">traversal</exception>
        public static Task<IReadOnlyCollection<Property>> SubmitAsync<S>(this IGraphClient graphClient, ITraversal<S, Gremlin.Net.Structure.Property> traversal)
        {
            return graphClient.SubmitAsync<Property>(traversal);
        }

        /// <summary>
        /// Submits the given traversal query to the <see cref="Gremlin.Net.CosmosDb.IGraphClient"/>
        /// and returns the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graphClient">The graph client.</param>
        /// <param name="gremlinQuery">The traversal query.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <returns>Returns the result</returns>
        /// <exception cref="ArgumentNullException">traversal</exception>
        public static async Task<IReadOnlyCollection<T>> SubmitAsync<T>(this IGraphClient graphClient, string gremlinQuery, JsonSerializerSettings serializerSettings)
        {
            var result = await graphClient.SubmitAsync(gremlinQuery);
            var serializer = JsonSerializer.Create(serializerSettings);

            return result.Select(token => token.ToObject<T>(serializer)).ToList();
        }

        /// <summary>
        /// Submits the given traversal query to the <see cref="Gremlin.Net.CosmosDb.IGraphClient"/>
        /// and returns the result
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="graphClient">The graph client.</param>
        /// <param name="traversal">The traversal.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <returns>Returns the result</returns>
        /// <exception cref="ArgumentNullException">traversal</exception>
        public static Task<IReadOnlyCollection<E>> SubmitAsync<S, E>(this IGraphClient graphClient, ITraversal<S, E> traversal, JsonSerializerSettings serializerSettings)
        {
            return graphClient.SubmitAsync<E>(traversal, serializerSettings);
        }

        /// <summary>
        /// Submits the given traversal query to the <see cref="Gremlin.Net.CosmosDb.IGraphClient"/>
        /// and returns the result
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="graphClient">The graph client.</param>
        /// <param name="traversal">The traversal.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <returns>Returns the result</returns>
        /// <exception cref="ArgumentNullException">traversal</exception>
        public static Task<IReadOnlyCollection<E>> SubmitAsync<S, E>(this IGraphClient graphClient, ISchemaBoundTraversal<S, E> traversal, JsonSerializerSettings serializerSettings)
        {
            return graphClient.SubmitAsync<E>(traversal.AsGraphTraversal(), serializerSettings);
        }

        /// <summary>
        /// Submits the given traversal query to the <see cref="Gremlin.Net.CosmosDb.IGraphClient"/>
        /// and returns the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="graphClient">The graph client.</param>
        /// <param name="traversal">The traversal.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <returns>Returns the result</returns>
        /// <exception cref="ArgumentNullException">traversal</exception>
        public static Task<IReadOnlyCollection<T>> SubmitAsync<T>(this IGraphClient graphClient, ITraversal traversal, JsonSerializerSettings serializerSettings)
        {
            if (traversal == null)
                throw new ArgumentNullException(nameof(traversal));

            var gremlinQuery = traversal.ToGremlinQuery();

            return graphClient.SubmitAsync<T>(gremlinQuery, serializerSettings);
        }

        /// <summary>
        /// Builds the default serializer settings.
        /// </summary>
        /// <returns>Returns the settings</returns>
        private static JsonSerializerSettings BuildDefaultSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                Converters = new JsonConverter[]
                {
                    new EdgeBaseJsonConverter(),
                    new ElementJsonConverter(),
                    new VertexBaseJsonConverter(),
                    new IsoDateTimeConverter
                    {
                        DateTimeStyles = DateTimeStyles.AdjustToUniversal
                    }
                },
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateParseHandling = DateParseHandling.DateTimeOffset,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
        }
    }
}