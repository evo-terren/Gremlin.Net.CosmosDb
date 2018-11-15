using Gremlin.Net.CosmosDb.Structure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gremlin.Net.CosmosDb.Serialization
{
    /// <summary>
    /// Used for converting the result of a tree() traversal into the <see cref="Tree"/> data structure.
    /// </summary>
    internal class TreeJsonConverter : JsonConverter
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter"/> can write JSON.
        /// </summary>
        /// <value><c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter"/> can write JSON; otherwise, <c>false</c>.</value>
        public override bool CanWrite => false;

        private static Dictionary<Type, Func<JsonReader, JsonSerializer, object>> _deserializers = new Dictionary<Type, Func<JsonReader, JsonSerializer, object>>
        {
            [typeof(Tree)] = ReadTree,
            [typeof(TreeVertexNode)] = ReadTreeVertexNode,
            [typeof(TreeEdgeNode)] = ReadTreeEdgeNode
        };

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return _deserializers.ContainsKey(objectType);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var deserializer = _deserializers[objectType];

            return deserializer(reader, serializer);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { }

        private static object ReadTree(JsonReader reader, JsonSerializer serializer)
        {
            var map = serializer.Deserialize<TreeVertexIntermediateNode>(reader);

            var vertices = map.Values.ToArray();

            return new Tree
            {
                RootVertexNodes = vertices
            };
        }

        private static object ReadTreeEdgeNode(JsonReader reader, JsonSerializer serializer)
        {
            var jObj = JObject.ReadFrom(reader);
            var edge = jObj["key"].ToObject<Edge>(serializer);
            var vertex = jObj["value"].ToObject<TreeVertexIntermediateNode>(serializer);

            var element = new TreeEdgeNode
            {
                Edge = edge,
                VertexNode = vertex.Values.FirstOrDefault()
            };

            return element;
        }

        private static object ReadTreeVertexNode(JsonReader reader, JsonSerializer serializer)
        {
            var jObj = JObject.ReadFrom(reader);
            var vertex = jObj["key"].ToObject<Vertex>(serializer);
            var edges = jObj["value"].ToObject<TreeEdgeIntermediateNode>(serializer);

            var element = new TreeVertexNode
            {
                Vertex = vertex,
                EdgeNodes = edges.Select(e => e.Value).ToArray()
            };

            return element;
        }
    }
}