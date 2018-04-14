namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Represents a single vertex/node within a graph
    /// </summary>
    /// <seealso cref="Gremlin.Net.CosmosDb.Structure.Element"/>
    public abstract class VertexBase : Element
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex"/> class.
        /// </summary>
        protected VertexBase()
        {
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return $"v[{Id}]";
        }
    }
}