using Gremlin.Net.CosmosDb.Structure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;

namespace Gremlin.Net.CosmosDb.Serialization
{
    /// <summary>
    /// Json.Net converter for <see cref="Gremlin.Net.CosmosDb.Structure.IVertex"/> objects
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter"/>
    internal sealed class IVertexJsonConverter : JsonConverter
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
            return TypeCache.IVertex.IsAssignableFrom(objectType);
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
            var propertiesObj = jo[PropertyNames.Properties] as JObject;
            if (propertiesObj != null)
                jo.Remove(PropertyNames.Properties);
            var vertex = jo.ToObject(objectType);

            if (propertiesObj != null)
            {
                var vertexContract = (JsonObjectContract)serializer.ContractResolver.ResolveContract(objectType);
                var convertedProperties = ConvertPropertiesObject(propertiesObj, vertexContract);
                serializer.Populate(convertedProperties.CreateReader(), vertex);
            }

            return vertex;
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

        /// <summary>
        /// Converts the given vertex properties object into a key/value pair object that lines up a little better with
        /// how json serialization typically works.
        /// </summary>
        /// <param name="propertiesObj">The properties object.</param>
        /// <param name="targetContract">The target contract.</param>
        /// <returns>Returns the converted object</returns>
        private static JObject ConvertPropertiesObject(JObject propertiesObj, JsonObjectContract targetContract)
        {
            var converted = new JObject();

            foreach (var key in propertiesObj)
            {
                var propertyContract = targetContract.Properties.GetClosestMatchProperty(key.Key);
                if (propertyContract == null)
                    continue;

                var value = key.Value as JArray;
                if (value == null)
                    continue;

                var simplifiedValues = SimplifyArrayOfValues(value);
                converted[key.Key] = TypeHelper.IsScalar(propertyContract.PropertyType)
                    ? simplifiedValues.First
                    : simplifiedValues;
            }

            return converted;
        }

        /// <summary>
        /// Helper method that simplifies the array of values of a vertex (array of object id/value
        /// pairs) to just the array of values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>Returns the simplified set of values</returns>
        private static JArray SimplifyArrayOfValues(JArray values)
        {
            var simplified = new JArray();

            foreach (var valueObj in values)
            {
                simplified.Add(valueObj[PropertyNames.Value]);
            }

            return simplified;
        }
    }
}