using Newtonsoft.Json;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Schema-bound container of a graph's edge that has specified in/out vertices
    /// </summary>
    public abstract class EdgeBase : Element
    {
        /// <summary>
        /// Gets or sets the id of the "in" vertex.
        /// </summary>
        [JsonProperty(PropertyNames.InVertexId)]
        public virtual string InV { get; set; }

        /// <summary>
        /// Gets or sets the "in" vertex label.
        /// </summary>
        [JsonProperty(PropertyNames.InVertexLabel)]
        public virtual string InVLabel { get; set; }

        /// <summary>
        /// Gets or sets the id of the "out" vertex.
        /// </summary>
        [JsonProperty(PropertyNames.OutVertexId)]
        public virtual string OutV { get; set; }

        /// <summary>
        /// Gets or sets the "out" vertex label.
        /// </summary>
        [JsonProperty(PropertyNames.OutVertexLabel)]
        public virtual string OutVLabel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeBase{ToutV, TinV}"/> class.
        /// </summary>
        protected internal EdgeBase()
        {
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return $"e[{Id}][{OutV}-{Label}->{InV}]";
        }
    }

    /// <summary>
    /// Schema-bound container of a graph's edge that has specified in/out vertices
    /// </summary>
    /// <typeparam name="ToutV">The type of the "out"/source vertex.</typeparam>
    /// <typeparam name="TinV">The type of the "in"/target vertex.</typeparam>
    public abstract class EdgeBase<ToutV, TinV> : EdgeBase, IHasOutVertex<ToutV>, IHasInVertex<TinV>
        where ToutV : VertexBase
        where TinV : VertexBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeBase{ToutV, TinV}"/> class.
        /// </summary>
        protected EdgeBase()
        {
        }
    }
}