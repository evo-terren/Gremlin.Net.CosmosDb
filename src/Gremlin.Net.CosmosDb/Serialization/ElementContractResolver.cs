using Gremlin.Net.CosmosDb.Structure;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Gremlin.Net.CosmosDb.Serialization
{
    /// <summary>
    /// Json.net contract resolver that helps with the resolution of contracts for known element types
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.Serialization.DefaultContractResolver"/>
    internal sealed class ElementContractResolver : DefaultContractResolver
    {
        private static readonly Type TYPE_OF_EDGEBASE = typeof(EdgeBase);
        private static readonly Type TYPE_OF_ELEMENT = typeof(Element);
        private static readonly Type TYPE_OF_VERTEXBASE = typeof(VertexBase);

        /// <summary>
        /// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonObjectContract"/> for the given type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// A <see cref="T:Newtonsoft.Json.Serialization.JsonObjectContract"/> for the given type.
        /// </returns>
        /// <exception cref="ArgumentNullException">objectType</exception>
        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            if (objectType == null)
                throw new ArgumentNullException(nameof(objectType));

            var contract = base.CreateObjectContract(objectType);

            if (TYPE_OF_ELEMENT.IsAssignableFrom(objectType))
            {
                contract.Properties.Remove(PropertyNames.Label);
            }
            if (TYPE_OF_EDGEBASE.IsAssignableFrom(objectType))
            {
                contract.Properties.Remove(PropertyNames.InVertexId);
                contract.Properties.Remove(PropertyNames.InVertexLabel);
                contract.Properties.Remove(PropertyNames.OutVertexId);
                contract.Properties.Remove(PropertyNames.OutVertexLabel);
            }
            else if (TYPE_OF_VERTEXBASE.IsAssignableFrom(objectType))
            {
                var edgeProps = new List<JsonProperty>();
                foreach (var prop in contract.Properties)
                {
                    if (TYPE_OF_EDGEBASE.IsAssignableFrom(prop.PropertyType))
                        edgeProps.Add(prop);
                }
                edgeProps.ForEach(p => contract.Properties.Remove(p));
            }

            return contract;
        }
    }
}