using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Simple edge-piece interface for schema to represent an edge that has an "in" vertex
    /// </summary>
    /// <typeparam name="TinV">The type of the in v.</typeparam>
    public interface IEdgeIn<TinV>
    {
    }

    /// <summary>
    /// Simple edge-piece interface for schema to represent an edge that has an "out" vertex
    /// </summary>
    /// <typeparam name="ToutV">The type of the out v.</typeparam>
    public interface IEdgeOut<ToutV>
    {
    }

    /// <summary>
    /// Represents an edge on a graph
    /// </summary>
    /// <seealso cref="Gremlin.Net.CosmosDb.Structure.Element"/>
    public class Edge : Element
    {
        /// <summary>
        /// Gets or sets the id of the "in" vertex.
        /// </summary>
        [JsonProperty("inV", Order = 3)]
        public virtual string InV { get; set; }

        /// <summary>
        /// Gets or sets the "in" vertex label.
        /// </summary>
        [JsonProperty("inVLabel", Order = 4)]
        public virtual string InVLabel { get; set; }

        /// <summary>
        /// Gets or sets the id of the "out" vertex.
        /// </summary>
        [JsonProperty("outV", Order = 5)]
        public virtual string OutV { get; set; }

        /// <summary>
        /// Gets or sets the "out" vertex label.
        /// </summary>
        [JsonProperty("outVLabel", Order = 6)]
        public virtual string OutVLabel { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        [JsonProperty("properties", Order = 7)]
        public virtual IReadOnlyDictionary<string, object> Properties
        {
            get { return _properties; }
            set { _properties = value ?? new Dictionary<string, object>(); }
        }

        private IReadOnlyDictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// Gets a value indicating whether the Properties property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool ShouldSerializeProperties() => Properties.Any();

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"e[{Id}][{OutV}-{Label}->{InV}]";
        }
    }

    /// <summary>
    /// Schema-bound container of a graph's edge
    /// </summary>
    /// <typeparam name="ToutV">The type of the "out"/source vertex.</typeparam>
    /// <typeparam name="TinV">The type of the "in"/target vertex.</typeparam>
    /// <seealso cref="Gremlin.Net.CosmosDb.Structure.Edge"/>
    public abstract class Edge<ToutV, TinV> : Edge, IEdgeOut<ToutV>, IEdgeIn<TinV>
    {
    }
}