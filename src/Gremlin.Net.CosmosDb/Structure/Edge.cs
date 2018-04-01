using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Represents an edge on a graph
    /// </summary>
    /// <seealso cref="Gremlin.Net.CosmosDb.Structure.Element"/>
    public sealed class Edge : Element
    {
        /// <summary>
        /// Gets or sets the id of the "in" vertex.
        /// </summary>
        [JsonProperty("inV", Order = 3)]
        public string InV { get; set; }

        /// <summary>
        /// Gets or sets the "in" vertex label.
        /// </summary>
        [JsonProperty("inVLabel", Order = 4)]
        public string InVLabel { get; set; }

        /// <summary>
        /// Gets or sets the id of the "out" vertex.
        /// </summary>
        [JsonProperty("outV", Order = 5)]
        public string OutV { get; set; }

        /// <summary>
        /// Gets or sets the "out" vertex label.
        /// </summary>
        [JsonProperty("outVLabel", Order = 6)]
        public string OutVLabel { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        [JsonProperty("properties", Order = 7)]
        public IReadOnlyDictionary<string, object> Properties
        {
            get { return _properties; }
            set { _properties = value ?? new Dictionary<string, object>(); }
        }

        private IReadOnlyDictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// Gets a value indicating whether the Properties property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeProperties() => Properties.Any();

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"e[{Id}][{OutV}-{Label}->{InV}]";
        }
    }

    /// <summary>
    /// Schema-bound container of a graph's edge
    /// </summary>
    /// <typeparam name="TinV">The type of the in vertex.</typeparam>
    /// <typeparam name="ToutV">The type of the out vertex.</typeparam>
    /// <seealso cref="Gremlin.Net.CosmosDb.Structure.Element"/>
    public abstract class Edge<TinV, ToutV> : IInVEdge<TinV>, IOutVEdge<ToutV>
    {
    }
}