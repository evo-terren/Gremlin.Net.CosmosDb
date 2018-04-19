namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Graph schema marker that indicates this object has an in vertex of the given type
    /// </summary>
    /// <typeparam name="TVertex">The type of the vertex.</typeparam>
    public interface IHasInVertex<out TVertex>
        where TVertex : VertexBase
    {
    }
}