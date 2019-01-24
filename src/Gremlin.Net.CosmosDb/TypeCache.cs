using Gremlin.Net.CosmosDb.Structure;
using System;
using System.Collections;

namespace Gremlin.Net.CosmosDb
{
    public static class TypeCache
    {
        public static readonly Type Element = typeof(Element);
        public static readonly Type IEdge = typeof(IEdge);
        public static readonly Type IEnumerable = typeof(IEnumerable);
        public static readonly Type IHasInV = typeof(IHasInV<>);
        public static readonly Type IHasOutV = typeof(IHasOutV<>);
        public static readonly Type IVertex = typeof(IVertex);
        public static readonly Type LabelAttribute = typeof(LabelAttribute);
        public static readonly Type String = typeof(string);
    }
}