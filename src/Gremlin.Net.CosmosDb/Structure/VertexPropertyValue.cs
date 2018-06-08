using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Data container for the value of a vertex's property
    /// </summary>
    public sealed class VertexPropertyValue
    {
        /// <summary>
        /// Gets or sets the property value identifier.
        /// </summary>
        [JsonProperty(PropertyNames.Id)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the properties (if any).
        /// </summary>
        [JsonProperty(PropertyNames.Properties)]
        public IDictionary<string, object> Properties
        {
            get { return _properties; }
            set { _properties = value ?? new Dictionary<string, object>(); }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty(PropertyNames.Value)]
        public object Value { get; set; }

        private IDictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// Gets a value indicating whether the Properties property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeProperties() => Properties.Any();
    }
}