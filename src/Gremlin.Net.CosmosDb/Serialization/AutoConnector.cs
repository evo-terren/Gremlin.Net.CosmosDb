using Gremlin.Net.CosmosDb.Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gremlin.Net.CosmosDb.Serialization
{
    /// <summary>
    /// This class provides a naive approach to connecting vertices and edges in tree form. The intention is that if
    /// there isn't a more specific connector written, this one should be applied, hence
    /// <see cref="CanConnectEdge(IVertex, IEdge, PropertyInfo)"/> and <see cref="CanConnectVertex(IEdge, IVertex)"/>
    /// both simply return true.
    /// </summary>
    internal class AutoConnector : IVertexConnector, IEdgeConnector
    {
        public bool CanConnectEdge(IVertex vertex, IEdge edge, PropertyInfo property) => true;

        public bool CanConnectVertex(IEdge edge, IVertex vertex) => true;

        /// <summary>
        /// This method tries to connect the <see cref="IEdge"/> to the <see cref="IVertex"/>.
        /// </summary>
        public bool ConnectEdge(IVertex vertex, IEdge edge, PropertyInfo property)
        {
            // First see if the property already has a value
            if (!(property.GetValue(vertex) is IEdge existingEdge))
            {
                // If this is not the case, simply set the value
                property.SetValue(vertex, edge);
            }
            else
            {
                // If we are here, it means that we need to do the hard work Since the vertex already have an edge here,
                // what we need to do is merge the two edges.

                // these are the potential properties that contains the vertices further down in the tree
                var vertexProps = edge.GetType().GetProperties().Where(p => TypeCache.IVertex.IsAssignableFrom(TypeHelper.UnderlyingType(p.PropertyType)));

                // we only expect one of them to be populated proceed if they have a value.
                foreach (var prop in vertexProps)
                {
                    switch (prop.GetValue(edge))
                    {
                        case IVertex v:
                            // if it's a scalar value, the job is easy simply connect the vertex to the existing edge
                            ConnectVertex(existingEdge, v);
                            continue;
                        case IEnumerable vEnum:
                            // if not, connect them all to the existing edge.
                            foreach (var vert in vEnum.OfType<IVertex>())
                            {
                                ConnectVertex(existingEdge, vert);
                            }
                            continue;
                        default:
                            break;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// This method tries to connect the <see cref="IVertex"/> to the <see cref="IEdge"/>.
        /// </summary>
        public bool ConnectVertex(IEdge edge, IVertex vertex)
        {
            var vertexType = vertex.GetType();

            // potential properties on the edge that are compatible with the vertex type
            var potentialVertexProperties = edge.GetType().GetProperties().Where(p => TypeHelper.UnderlyingType(p.PropertyType).IsAssignableFrom(vertexType)).ToList();

            // if the number of properties isn't exactly 1, this is no longer trivial and a more specific implementation
            // should be used (create a specific IVertexConnector to handle this.)
            if (potentialVertexProperties.Count == 1)
            {
                var prop = potentialVertexProperties[0];

                // in case of scalar, simply set the value this would be the case for edges like
                // * OneToOne
                // * OneToMany
                // * ManyToOne
                if (TypeHelper.IsScalar(prop.PropertyType))
                {
                    prop.SetValue(edge, vertex);
                }
                else if (prop.PropertyType.IsArray)
                {
                    // If we are here, it means that the property is an array The strategy here is to create a new array
                    // with room for one more vertex, and then set the value of the property to this new array instead
                    var currentValue = prop.GetValue(edge) as Array;
                    List<IVertex> allVertices;

                    if (currentValue != null)
                    {
                        allVertices = currentValue.OfType<IVertex>().Append(vertex).ToList();
                    }
                    else
                    {
                        allVertices = new List<IVertex>() { vertex };
                    }

                    var newArr = Array.CreateInstance(prop.PropertyType.GetElementType(), allVertices.Count);

                    for (int i = 0; i < allVertices.Count; i++)
                    {
                        newArr.SetValue(allVertices[i], i);
                    }

                    prop.SetValue(edge, newArr);
                }
                else
                {
                    // If we are here it means that the property is non-scalar and not an array. As such, assume it's a
                    // List (otherwise it's non-trivial)
                    if (prop.GetValue(edge) is IList oldList)
                    {
                        // If it already exists, add the vertex
                        oldList.Add(vertex);
                    }
                    else
                    {
                        // If not, create the list and add the vertex
                        var underlying = TypeHelper.UnderlyingType(prop.PropertyType);
                        var newList = Activator.CreateInstance(typeof(List<>).MakeGenericType(underlying)) as IList;
                        newList.Add(vertex);
                        prop.SetValue(edge, newList);
                    }
                }
            }
            else
            {
                var exception = new Exception($"Unable to attach vertex {vertex.GetType().Name} to edge {edge.GetType().Name}, found {potentialVertexProperties.Count} possible properties, but can only use exactly 1.");

                // Add some (hopefully) useful information
                foreach (var prop in potentialVertexProperties)
                {
                    exception.Data.Add(prop.Name, prop);
                }

                throw exception;
            }

            return true;
        }
    }
}