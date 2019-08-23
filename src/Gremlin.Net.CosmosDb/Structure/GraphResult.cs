using Gremlin.Net.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Model of the result with explicit attributes from CosmosDb. https://docs.microsoft.com/en-us/rest/api/cosmos-db/common-cosmosdb-rest-response-headers
    /// </summary>
    public class GraphResult
    {
        /// <summary>
        /// Represents a unique identifier for the operation. Commonly used for troubleshooting purposes.
        /// </summary>
        public string ActivityId => (string)ResultSet.StatusAttributes["x-ms-activity-id"];

        /// <summary>
        /// x-ms-cosmosdb-graph-request-charge
        /// </summary>
        public double CosmosDbGraphRequestCharge => (double)ResultSet.StatusAttributes["x-ms-cosmosdb-graph-request-charge"];

        /// <summary>
        /// This is the number of normalized requests a.k.a. request units (RU) for the operation.
        /// </summary>
        public double RequestCharge => (double)ResultSet.StatusAttributes["x-ms-request-charge"];

        /// <summary>
        /// The original gremlin result.
        /// </summary>
        public ResultSet<JToken> ResultSet { get; }

        /// <summary>
        /// The number of milliseconds to wait to retry the operation after an initial operation was throttled. This will
        /// be populated when attribute 'x-ms-status-code' returns 429.
        /// </summary>
        public double RetryAfterMs => (double)ResultSet.StatusAttributes["x-ms-retry-after-ms"];

        /// <summary>
        /// The sub-status code of the operation, specific to CosmosDB.
        /// </summary>
        public long StatusCode => (long)ResultSet.StatusAttributes["x-ms-status-code"];

        /// <summary>
        /// StorageRU
        /// </summary>
        public double StorageRU => (double)ResultSet.StatusAttributes["StorageRU"];

        /// <summary>
        /// The total request units charged for processing a request.
        /// </summary>
        public double TotalRequestCharge => (double)ResultSet.StatusAttributes["x-ms-total-request-charge"];

        public GraphResult(ResultSet<JToken> resultSet)
        {
            ResultSet = resultSet ?? throw new System.ArgumentNullException(nameof(resultSet));
        }

        public GraphResult<T> ApplyType<T>(JsonSerializer serializer)
        {
            return new GraphResult<T>(ResultSet, serializer);
        }
    }

    /// <summary>
    /// A <see cref="GraphResult"/> with typed deserialized data.
    /// </summary>
    /// <typeparam name="T">The type to deserialize as.</typeparam>
    public class GraphResult<T> : GraphResult, IEnumerable<T>
    {
        /// <summary>
        /// The deserialized results of the operation.
        /// </summary>
        public IReadOnlyCollection<T> Result { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphResult{T}"/> class.
        /// </summary>
        /// <param name="resultSet">The result set.</param>
        /// <param name="serializer">The serializer.</param>
        /// <exception cref="System.ArgumentNullException">resultSet</exception>
        internal GraphResult(ResultSet<JToken> resultSet, JsonSerializer serializer) : base(resultSet)
        {
            if (resultSet == null)
            {
                throw new System.ArgumentNullException(nameof(resultSet));
            }

            Result = resultSet.Select(token => token.ToObject<T>(serializer)).ToList();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator() => Result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Result.GetEnumerator();
    }
}