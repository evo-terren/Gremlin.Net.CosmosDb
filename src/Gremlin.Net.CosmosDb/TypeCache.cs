using Gremlin.Net.CosmosDb.Structure;
using System;
using System.Collections;

namespace Gremlin.Net.CosmosDb
{
    internal static class TypeCache
    {
        internal static readonly Type Element = typeof(Element);
        internal static readonly Type IEdge = typeof(IEdge);
        internal static readonly Type IEnumerable = typeof(IEnumerable);
        internal static readonly Type IHasInV = typeof(IHasInV<>);
        internal static readonly Type IHasOutV = typeof(IHasOutV<>);
        internal static readonly Type IVertex = typeof(IVertex);
        internal static readonly Type LabelAttribute = typeof(LabelAttribute);
        internal static readonly Type String = typeof(string);
    }
}