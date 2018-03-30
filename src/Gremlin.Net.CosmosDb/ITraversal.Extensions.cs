using Gremlin.Net.CosmosDb.Serialization;
using Gremlin.Net.Process.Traversal;
using System.IO;
using System.Text;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// <see cref="Gremlin.Net.Process.Traversal.ITraversal"/> extension methods
    /// </summary>
    public static class ITraversalExtensions
    {
        /// <summary>
        /// Returns the string-equivalent of the given traversal
        /// </summary>
        /// <param name="traversal">The traversal.</param>
        /// <returns>Returns the string query</returns>
        public static string ToGremlinQuery(this ITraversal traversal)
        {
            var sb = new StringBuilder();
            using (var serializer = new GremlinQuerySerializer(new StringWriter(sb)))
            {
                serializer.Serialize(traversal);
            }

            return sb.ToString();
        }
    }
}