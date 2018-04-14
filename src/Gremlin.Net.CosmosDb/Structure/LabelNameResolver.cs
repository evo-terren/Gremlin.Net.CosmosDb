using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Helper class that resolves label names for elements
    /// </summary>
    internal static class LabelNameResolver
    {
        private static readonly ConcurrentDictionary<Type, string> _labelLookup = new ConcurrentDictionary<Type, string>();

        /// <summary>
        /// Gets the name of the label.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Returns the name</returns>
        public static string GetLabelName(Type type)
        {
            return _labelLookup.GetOrAdd(type, t =>
            {
                var attr = type.GetCustomAttributes(typeof(LabelAttribute), false).OfType<LabelAttribute>().FirstOrDefault();

                return attr?.Name;
            });
        }
    }
}