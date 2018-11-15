using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Represents an schema-less edge on a graph
    /// </summary>
    public class Edge : Element
    {
        /// <summary>
        /// Gets or sets the id of the "in" vertex.
        /// </summary>
        [JsonProperty(PropertyNames.InVertexId)]
        public virtual string InV { get; set; }

        /// <summary>
        /// Gets or sets the "in" vertex label.
        /// </summary>
        [JsonProperty(PropertyNames.InVertexLabel)]
        public virtual string InVLabel { get; set; }

        /// <summary>
        /// Gets or sets the id of the "out" vertex.
        /// </summary>
        [JsonProperty(PropertyNames.OutVertexId)]
        public virtual string OutV { get; set; }

        /// <summary>
        /// Gets or sets the "out" vertex label.
        /// </summary>
        [JsonProperty(PropertyNames.OutVertexLabel)]
        public virtual string OutVLabel { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        [JsonProperty(PropertyNames.Properties)]
        public virtual IDictionary<string, object> Properties
        {
            get { return _properties; }
            set { _properties = value ?? new Dictionary<string, object>(); }
        }

        private IDictionary<string, object> _properties = new Dictionary<string, object>();

        /// <summary>
        /// Gets a value indicating whether the Properties property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool ShouldSerializeProperties() => Properties.Any();

        /// <summary>
        /// Converts this edge to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of object to convert to</typeparam>
        /// <returns>Returns the converted object</returns>
        public T ToObject<T>()
            where T : class
        {
            return ToObject<T>(new JsonSerializerSettings());
        }

        /// <summary>
        /// Converts this edge to an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of object to convert to</typeparam>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <returns>Returns the converted object</returns>
        public T ToObject<T>(JsonSerializerSettings serializerSettings)
            where T : class
        {
            return (T)ToObject(typeof(T), serializerSettings);
        }

        /// <summary>
        /// Converts this edge to an object of type <paramref name="objectType"/>.
        /// </summary>
        /// <param name="objectType">The type of object to convert to</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <returns>Returns the converted object</returns>
        public object ToObject(Type objectType, JsonSerializerSettings serializerSettings = null)
        {
            var serializer = JsonSerializer.Create(serializerSettings ?? new JsonSerializerSettings());
            var properties = new Dictionary<string, object>(Properties)
            {
                [PropertyNames.Id] = Id,
                [PropertyNames.InVertexId] = InV,
                [PropertyNames.InVertexLabel] = InVLabel,
                [PropertyNames.Label] = Label,
                [PropertyNames.OutVertexId] = OutV,
                [PropertyNames.OutVertexLabel] = OutVLabel
            };

            return JObject.FromObject(Properties).ToObject(objectType, serializer);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return $"{OutVLabel}-{Label}->{InVLabel}";
        }
    }
}