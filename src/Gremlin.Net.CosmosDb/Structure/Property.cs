using Newtonsoft.Json;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Represents a single property in a graph
    /// </summary>
    public sealed class Property
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty("id", Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [JsonProperty("label", Order = 2)]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("value", Order = 2)]
        public object Value { get; set; }
    }
}