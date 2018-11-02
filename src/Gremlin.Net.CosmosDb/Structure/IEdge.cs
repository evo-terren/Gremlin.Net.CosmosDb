namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Base marker interface for edges.
    /// Don't implement this, instead implement <see cref="IEdge{TOutV, TInV}"/>.
    /// </summary>
    public interface IEdge
    {
    }

    /// <summary>
    /// Marker interface for types having an outbound traversal.
    /// </summary>
    /// <typeparam name="TOutV">The type at the destination of the outbound traversal.</typeparam>
    public interface IHasOutV<TOutV>
        where TOutV : IVertex
    {
    }

    /// <summary>
    /// Marker interface for types having an inbound traversal.
    /// </summary>
    /// <typeparam name="TInV">The type at the destination of the inbound traversal.</typeparam>
    public interface IHasInV<TInV>
        where TInV : IVertex
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
