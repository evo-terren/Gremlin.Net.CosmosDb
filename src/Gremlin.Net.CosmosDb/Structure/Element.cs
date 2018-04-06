using Newtonsoft.Json;
using System.ComponentModel;

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
        [JsonProperty("id")]
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [JsonProperty("label")]
        public virtual string Label { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Element"/> class.
        /// </summary>
        protected internal Element()
        {
        }
    }

    /// <summary>
    /// Base class for data elements within a graph that has a specified model for its properties
    /// </summary>
    /// <typeparam name="TPropertiesModel">The type of the properties model.</typeparam>
    public abstract class Element<TPropertiesModel> : Element
        where TPropertiesModel : class
    {
        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        [JsonProperty("properties")]
        public virtual TPropertiesModel Properties { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Element{TPropertiesModel}"/> class.
        /// </summary>
        protected internal Element()
        {
        }

        /// <summary>
        /// Gets a value indicating whether the Properties property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool ShouldSerializeProperties() => Properties != default(TPropertiesModel);
    }
}