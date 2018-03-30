using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Represents a single vertex/node within a graph
    /// </summary>
    /// <seealso cref="Gremlin.Net.CosmosDb.Structure.Element"/>
    public class Vertex : Element
    {
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        [JsonProperty("properties", Order = 3)]
        public virtual IReadOnlyDictionary<string, IReadOnlyCollection<VertexPropertyValue>> Properties
        {
            get { return _properties; }
            set { _properties = value ?? new Dictionary<string, IReadOnlyCollection<VertexPropertyValue>>(); }
        }

        private IReadOnlyDictionary<string, IReadOnlyCollection<VertexPropertyValue>> _properties = new Dictionary<string, IReadOnlyCollection<VertexPropertyValue>>();

        /// <summary>
        /// Gets a value indicating whether the Properties property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeProperties() => Properties.Any();

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"v[{Id}]";
        }
    }
}