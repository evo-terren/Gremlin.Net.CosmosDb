using Newtonsoft.Json;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Represents a single property in a graph
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty("id", Order = 1)]
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [JsonProperty("label", Order = 2)]
        public virtual string Label { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("value", Order = 2)]
        public virtual object Value { get; set; }
    }
}