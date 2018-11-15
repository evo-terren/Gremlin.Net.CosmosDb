using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json.Linq;
using System;

namespace Gremlin.Net.CosmosDb.Serialization
{
    /// <summary>
    /// GraphSON reader that just returns the json token to get around weird implementation choices of gremlin .net
    /// </summary>
    /// <seealso cref="Gremlin.Net.Structure.IO.GraphSON.GraphSONReader"/>
    internal sealed class GraphSONJTokenReader : GraphSONReader
    {
        /// <summary>
        /// Deserializes GraphSON to an object.
        /// </summary>
        /// <param name="jToken">The GraphSON to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public override dynamic ToObject(JToken jToken)
        {
            return ConvertDateJson(jToken);
        }

        /// <summary>
        /// Converts the dates from local to universal.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Returns the converted version of the token</returns>
        /// <remarks>
        /// This is only in place due to how deserialization is handled by the Gremlin.Net code. By default json.net
        /// assumes local time no matter what the input if you don't specify certain settings during deserialization.
        /// This code overrides that by assuming universal since that's what we're going to use most of the time.
        /// </remarks>
        private static JToken ConvertDateJson(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Array:
                    var arr = (JArray)token;
                    for (var i = 0; i < arr.Count; i++)
                    {
                        arr[i] = ConvertDateJson(arr[i]);
                    }
                    return arr;

                case JTokenType.Date:
                    var date = (DateTime)token;
                    return JToken.FromObject(date.ToUniversalTime());

                case JTokenType.Object:
                    var obj = (JObject)token;
                    foreach (var prop in obj.Properties())
                    {
                        obj[prop.Name] = ConvertDateJson(obj[prop.Name]);
                    }
                    return obj;

                default:
                    return token;
            }
        }
    }
}