using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Represents an edge on a graph
    /// </summary>
    public class Edge : Edge<IReadOnlyDictionary<string, object>>
    {
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        [JsonProperty("properties")]
        public override IReadOnlyDictionary<string, object> Properties
        {
            get { return _properties; }
            set { _properties = value ?? new Dictionary<string, object>(); }
        }

        private IReadOnlyDictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// Gets a value indicating whether the Properties property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool ShouldSerializeProperties() => Properties.Any();
    }

    /// <summary>
    /// Represents an edge on a graph
    /// </summary>
    /// <seealso cref="Gremlin.Net.CosmosDb.Structure.Element{TPropertiesModel}"/>
    public class Edge<TPropertiesModel> : Element<TPropertiesModel>
        where TPropertiesModel : class
    {
        /// <summary>
        /// Gets or sets the id of the "in" vertex.
        /// </summary>
        [JsonProperty("inV")]
        public virtual string InV { get; set; }

        /// <summary>
        /// Gets or sets the "in" vertex label.
        /// </summary>
        [JsonProperty("inVLabel")]
        public virtual string InVLabel { get; set; }

        /// <summary>
        /// Gets or sets the id of the "out" vertex.
        /// </summary>
        [JsonProperty("outV")]
        public virtual string OutV { get; set; }

        /// <summary>
        /// Gets or sets the "out" vertex label.
        /// </summary>
        [JsonProperty("outVLabel")]
        public virtual string OutVLabel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge{TPropertiesModel}"/> class.
        /// </summary>
        protected Edge()
        {
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return $"e[{Id}][{OutV}-{Label}->{InV}]";
        }
    }

    /// <summary>
    /// Schema-bound container of a graph's edge that has a specific model for its properties
    /// </summary>
    /// <typeparam name="ToutV">The type of the "out"/source vertex.</typeparam>
    /// <typeparam name="TinV">The type of the "in"/target vertex.</typeparam>
    /// <typeparam name="TPropertiesModel">The type of the properties model.</typeparam>
    public abstract class Edge<ToutV, TinV, TPropertiesModel> : Edge<TPropertiesModel>, IEdgeOut<ToutV>, IEdgeIn<TinV>
        where TPropertiesModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Edge{ToutV, TinV, TPropertiesModel}"/> class.
        /// </summary>
        protected Edge()
        {
        }
    }

    /// <summary>
    /// Schema-bound container of a graph's edge
    /// </summary>
    /// <typeparam name="ToutV">The type of the "out"/source vertex.</typeparam>
    /// <typeparam name="TinV">The type of the "in"/target vertex.</typeparam>
    /// <seealso cref="Gremlin.Net.CosmosDb.Structure.Edge"/>
    public abstract class Edge<ToutV, TinV> : Edge<ToutV, TinV, IReadOnlyDictionary<string, object>>
    {
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        [JsonProperty("properties")]
        public override IReadOnlyDictionary<string, object> Properties
        {
            get { return _properties; }
            set { _properties = value ?? new Dictionary<string, object>(); }
        }

        private IReadOnlyDictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge{ToutV, TinV}"/> class.
        /// </summary>
        protected Edge()
        {
        }

        /// <summary>
        /// Gets a value indicating whether the Properties property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool ShouldSerializeProperties() => Properties.Any();
    }
}