using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Helper methods for common traversal recipes
    /// </summary>
    internal static class TraversalHelper
    {
        /// <summary>
        /// Adds the property steps for all properties defined by the given object.
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="traversal">The traversal.</param>
        /// <param name="obj">The object.</param>
        /// <param name="serializationSettings">The serialization settings.</param>
        /// <returns>Returns the resulting traversal</returns>
        public static GraphTraversal<S, E> AddObjectProperties<S, E>(GraphTraversal<S, E> traversal, object obj, JsonSerializerSettings serializationSettings)
        {
            var serializedObj = JObject.FromObject(obj, JsonSerializer.Create(serializationSettings));

            foreach (var prop in serializedObj)
            {
                //ignore null values
                switch (prop.Value.Type)
                {
                    //ignore null values
                    case JTokenType.Null:
                    case JTokenType.Undefined:
                        break;

                    case JTokenType.Array:
                        //ignore arrays that have no values
                        if (!prop.Value.HasValues)
                            break;

                        foreach (var value in prop.Value.Values())
                        {
                            traversal = traversal.Property(Cardinality.List, prop.Key, value);
                        }
                        break;

                    default:
                        traversal = traversal.Property(prop.Key, prop.Value);
                        break;
                }
                if (prop.Value.Type == JTokenType.Null)
                    continue;
            }

            return traversal;
        }
    }
}