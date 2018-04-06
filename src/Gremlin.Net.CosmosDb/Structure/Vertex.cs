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
    public class Vertex : Vertex<IReadOnlyDictionary<string, IReadOnlyCollection<VertexPropertyValue>>>
    {
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        [JsonProperty("properties", Order = 3)]
        public override IReadOnlyDictionary<string, IReadOnlyCollection<VertexPropertyValue>> Properties
        {
            get { return _properties; }
            set { _properties = value ?? new Dictionary<string, IReadOnlyCollection<VertexPropertyValue>>(); }
        }

        private IReadOnlyDictionary<string, IReadOnlyCollection<VertexPropertyValue>> _properties = new Dictionary<string, IReadOnlyCollection<VertexPropertyValue>>();

        /// <summary>
        /// Gets a value indicating whether the Properties property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool ShouldSerializeProperties() => Properties.Any();
    }

    /// <summary>
    /// Represents a single vertex/node within a graph
    /// </summary>
    /// <typeparam name="TPropertiesModel">The type of the properties model.</typeparam>
    /// <seealso cref="Gremlin.Net.CosmosDb.Structure.Element{TPropertiesModel}"/>
    public abstract class Vertex<TPropertiesModel> : Element<TPropertiesModel>
        where TPropertiesModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex{TPropertiesModel}"/> class.
        /// </summary>
        protected Vertex()
        {
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return $"v[{Id}]";
        }
    }
}