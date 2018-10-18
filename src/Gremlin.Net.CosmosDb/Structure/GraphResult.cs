using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gremlin.Net.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Model of the result with explicit attributes from CosmosDb.
    /// https://docs.microsoft.com/en-us/rest/api/cosmos-db/common-cosmosdb-rest-response-headers
    /// </summary>
    public class GraphResult
    {
        /// <summary>
        /// The original gremlin result.
        /// </summary>
        public ResultSet<JToken> ResultSet { get; }

        /// <summary>
        /// The status code of the operation.
        /// </summary>
        public long StatusCode => (long)ResultSet.StatusAttributes["x-ms-status-code"];

        /// <summary>
        /// This is the number of normalized requests a.k.a. request units (RU) for the operation.
        /// </summary>
        public double RequestCharge => (double)ResultSet.StatusAttributes["x-ms-request-charge"];

        /// <summary>
        /// x-ms-total-request-charge
        /// </summary>
        public double TotalRequestCharge => (double)ResultSet.StatusAttributes["x-ms-total-request-charge"];

        /// <summary>
        /// x-ms-cosmosdb-graph-request-charge
        /// </summary>
        public double CosmosDbGraphRequestCharge => (double)ResultSet.StatusAttributes["x-ms-cosmosdb-graph-request-charge"];

        /// <summary>
        /// StorageRU
        /// </summary>
        public double StorageRU => (double)ResultSet.StatusAttributes["StorageRU"];

        internal GraphResult(ResultSet<JToken> resultSet)
        {
            ResultSet = resultSet ?? throw new System.ArgumentNullException(nameof(resultSet));
        }

        internal GraphResult<T> ApplyType<T>(JsonSerializer serializer)
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
        internal GraphResult(ResultSet<JToken> resultSet, JsonSerializer serializer) : base(resultSet)
        {
            if (resultSet == null)
            {
                throw new System.ArgumentNullException(nameof(resultSet));
            }

            Result = resultSet.Select(token => token.ToObject<T>(serializer)).ToList();
        }

        /// <summary>
        /// The deserialized results of the operation.
        /// </summary>
        public IReadOnlyCollection<T> Result { get; }

        public IEnumerator<T> GetEnumerator() => Result.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Result.GetEnumerator();
    }
}
