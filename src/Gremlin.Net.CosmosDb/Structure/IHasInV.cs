namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Marker interface for types having an inbound traversal.
    /// </summary>
    /// <typeparam name="TInV">The type at the destination of the inbound traversal.</typeparam>
    public interface IHasInV<TInV>
        where TInV : IVertex
    {
    }
}