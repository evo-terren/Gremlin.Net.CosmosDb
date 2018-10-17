using System;
using System.Collections.Generic;
using System.Linq;
using Gremlin.Net.CosmosDb.Structure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.CosmosDb.Serialization
{
    /// <summary>
    /// Used for converting the result of a tree() traversal into the <see cref="Tree"/> data structure.
    /// </summary>
    public class TreeJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { }

        private static Dictionary<Type, Func<JsonReader, JsonSerializer, object>> _deserializers = new Dictionary<Type, Func<JsonReader, JsonSerializer, object>>
        {
            [typeof(Tree)] = ReadTree,
            [typeof(TreeVertexNode)] = ReadTreeVertexNode,
            [typeof(TreeEdgeNode)] = ReadTreeEdgeNode
        };

        public override bool CanConvert(Type objectType)
        {
            return _deserializers.ContainsKey(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var deserializer = _deserializers[objectType];

            return deserializer(reader, serializer);
        }


        private static object ReadTree(JsonReader reader, JsonSerializer serializer)
        {
            var map = serializer.Deserialize<TreeVertexIntermediateNode>(reader);

            var vertices = map.Values.ToArray();

            return new Tree
            {
                RootVertexNodes = vertices
            };
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
    }
}
