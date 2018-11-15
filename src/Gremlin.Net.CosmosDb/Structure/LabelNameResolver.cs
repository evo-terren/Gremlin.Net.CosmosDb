using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Helper class that resolves label names for elements
    /// </summary>
    internal static class LabelNameResolver
    {
        private static readonly ConcurrentDictionary<PropertyInfo, string> _propertyLabelLookup = new ConcurrentDictionary<PropertyInfo, string>();
        private static readonly ConcurrentDictionary<Type, string> _typeLabelLookup = new ConcurrentDictionary<Type, string>();

        /// <summary>
        /// Gets the name of the label. Defaults to the name of the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Returns the name</returns>
        public static string GetLabelName(Type type)
        {
            return _typeLabelLookup.GetOrAdd(type, t =>
            {
                var attr = type.GetCustomAttributes(TypeCache.LabelAttribute, false).OfType<LabelAttribute>().FirstOrDefault();

                return attr?.Name ?? type.Name.Substring(0, 1).ToLower() + type.Name.Substring(1);
            });
        }

        /// <summary>
        /// Gets the name of the label for a property. Defaults to the name of the property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>Returns the label name.</returns>
        public static string GetLabelName(PropertyInfo property)
        {
            return _propertyLabelLookup.GetOrAdd(property, p =>
            {
                var attr = p.GetCustomAttribute<LabelAttribute>() ?? p.PropertyType.GetCustomAttribute<LabelAttribute>();

                return attr?.Name ?? p.Name.Substring(0, 1).ToLower() + p.Name.Substring(1);
            });
        }
    }
}