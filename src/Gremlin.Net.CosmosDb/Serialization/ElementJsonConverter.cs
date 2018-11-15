using Gremlin.Net.CosmosDb.Structure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Gremlin.Net.CosmosDb.Serialization
{
    /// <summary>
    /// Json.Net converter for <see cref="Gremlin.Net.CosmosDb.Structure.Element"/> objects
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter"/>
    internal sealed class ElementJsonConverter : JsonConverter
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter"/> can write JSON.
        /// </summary>
        /// <value><c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter"/> can write JSON; otherwise, <c>false</c>.</value>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == TypeCache.Element;
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        /// <exception cref="ArgumentNullException">reader</exception>
        /// <exception cref="NotImplementedException"></exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            var jo = JObject.Load(reader);
            var elementType = jo[PropertyNames.Type].Value<string>();
            Element element;
            switch (elementType)
            {
                case "edge":
                    element = new Edge();
                    break;

                case "vertex":
                    element = new Vertex();
                    break;

                default:
                    throw new NotImplementedException($"Unable to deserialize type '{elementType}'");
            }

            serializer.Populate(jo.CreateReader(), element);

            return element;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // won't be called because CanWrite returns false
        }
    }
}