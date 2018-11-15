namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Marker interface for types having an outbound traversal.
    /// </summary>
    /// <typeparam name="TOutV">The type at the destination of the outbound traversal.</typeparam>
    public interface IHasOutV<TOutV>
        where TOutV : IVertex
    {
    }
}