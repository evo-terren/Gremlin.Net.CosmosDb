using Newtonsoft.Json;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Base class for data elements within a graph
    /// </summary>
    public abstract class Element
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty(PropertyNames.Id, NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [JsonProperty(PropertyNames.Label)]
        public virtual string Label { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Element"/> class.
        /// </summary>
        protected internal Element()
        {
        }
    }
}