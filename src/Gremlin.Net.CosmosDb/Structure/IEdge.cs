namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Base marker interface for edges. Don't implement this, instead implement <see cref="IEdge{TOutV, TInV}"/>.
    /// </summary>
    public interface IEdge
    {
    }

    /// <summary>
    /// A marker interface for edges.
    /// </summary>
    /// <typeparam name="TOutV">The Out type.</typeparam>
    /// <typeparam name="TInV">The In type.</typeparam>
    public interface IEdge<TOutV, TInV> : IHasOutV<TOutV>, IHasInV<TInV>, IEdge
        where TOutV : IVertex
        where TInV : IVertex
    {
    }
}