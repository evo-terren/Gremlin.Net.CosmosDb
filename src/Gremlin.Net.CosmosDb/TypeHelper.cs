using System;
using System.Linq;

namespace Gremlin.Net.CosmosDb
{
    public static class TypeHelper
    {
        public static bool IsScalar(Type t) => t == TypeCache.String || !(TypeCache.IEnumerable.IsAssignableFrom(t) || t.IsArray);

        public static Type UnderlyingType(Type t) => IsScalar(t) ? t : (t.IsArray ? t.GetElementType() : t.GenericTypeArguments.FirstOrDefault());
    }
}