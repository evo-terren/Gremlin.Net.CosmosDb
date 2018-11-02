using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gremlin.Net.CosmosDb.Structure;

namespace Gremlin.Net.CosmosDb.Serialization
{
    internal class AutoConnector : IVertexConnector, IEdgeConnector
    {
        public bool CanConnectEdge(IVertex vertex, IEdge edge, PropertyInfo property) => true;
        public bool CanConnectVertex(IEdge edge, IVertex vertex) => true;

        public bool ConnectEdge(IVertex vertex, IEdge edge, PropertyInfo property)
        {
            if (!(property.GetValue(vertex) is IEdge existingEdge))
            {
                property.SetValue(vertex, edge);
            }
            else
            {
                var vertexProps = edge.GetType().GetProperties().Where(p => TypeCache.IVertex.IsAssignableFrom(TypeHelper.UnderlyingType(p.PropertyType)));

                foreach (var prop in vertexProps)
                {
                    if (prop.GetValue(edge) is object newEdgeVertex)
                    {
                        if (newEdgeVertex is IVertex v)
                        {
                            ConnectVertex(existingEdge, v);
                            continue;
                        }
                        else if (newEdgeVertex is IEnumerable vEnum)
                        {
                            foreach (var vert in vEnum.OfType<IVertex>())
                            {
                                ConnectVertex(existingEdge, vert);
                            }
                            continue;
                        }
                    }
                }
            }

            return true;
        }

        public bool ConnectVertex(IEdge edge, IVertex vertex)
        {
            var vertexType = vertex.GetType();
            var potentialVertexProperties = edge.GetType().GetProperties().Where(p => TypeHelper.UnderlyingType(p.PropertyType).IsAssignableFrom(vertexType)).ToList();

            if (potentialVertexProperties.Count == 1)
            {
                var prop = potentialVertexProperties[0];

                if (TypeHelper.IsScalar(prop.PropertyType))
                {
                    prop.SetValue(edge, vertex);
                }
                else if (prop.PropertyType.IsArray)
                {
                    var currentValue = prop.GetValue(edge) as Array;
                    IEnumerable<IVertex> vertices;

                    if (currentValue != null)
                    {
                        vertices = currentValue.OfType<IVertex>();
                    }
                    else
                    {
                        vertices = new List<IVertex>() { vertex };
                    }

                    var newArr = Array.CreateInstance(prop.PropertyType.GetElementType(), currentValue.Length + 1);

                    int index = 0;

                    foreach (var item in vertices)
                    {
                        newArr.SetValue(item, index++);
                    }

                    newArr.SetValue(vertex, index);

                    prop.SetValue(edge, newArr);
                }
                else
                {
                    if (prop.GetValue(edge) is IList oldList)
                    {
                        oldList.Add(vertex);
                    }
                    else
                    {
                        var newList = Activator.CreateInstance(prop.PropertyType) as IList;
                        newList.Add(vertex);
                        prop.SetValue(edge, newList);
                    }
                }
            }
            else
            {
                var exception = new Exception($"Unable to attach vertex {vertex.GetType().Name} to edge {edge.GetType().Name}, found {potentialVertexProperties.Count} possible properties, but can only use exactly 1.");

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
