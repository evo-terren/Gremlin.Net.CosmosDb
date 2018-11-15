using Gremlin.Net.CosmosDb.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// A parsed traversal result in tree representation. E.g. "g.V().hasLabel('person').outE('purchased').inV().tree()"
    /// </summary>
    public class Tree
    {
        /// <summary>
        /// The root vertices of the tree traversal.
        /// </summary>
        public TreeVertexNode[] RootVertexNodes { get; set; }

        /// <summary>
        /// Convert the parsed tree into the specified type.
        /// </summary>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <param name="treeConnectors">Custom connectors to use for attaching edges and vertices.</param>
        /// <returns></returns>
        public T[] ToObject<T>(IEnumerable<ITreeConnector> treeConnectors = null)
            where T : IVertex
        {
            var parser = new TreeParser(treeConnectors ?? Enumerable.Empty<ITreeConnector>());

            var vertices = RootVertexNodes.Select(n => parser.GetVertex(n, typeof(T))).Cast<T>();

            return vertices.ToArray();
        }

        private class TreeParser
        {
            private readonly List<IEdgeConnector> _edgeConnectors;
            private readonly List<IVertexConnector> _vertexConnectors;

            public TreeParser(IEnumerable<ITreeConnector> connectors)
            {
                if (!connectors.Any(c => c is AutoConnector))
                {
                    connectors = connectors.Append(new AutoConnector());
                }

                _vertexConnectors = connectors.OfType<IVertexConnector>().ToList();
                _edgeConnectors = connectors.OfType<IEdgeConnector>().ToList();
            }

            public IEdge GetEdge(TreeEdgeNode edgeNode, Type edgeType, bool directionIsOut)
            {
                var edge = (IEdge)edgeNode.Edge.ToObject(edgeType);

                var nextVertexType = directionIsOut ? GetEdgeInType(edgeType) : GetEdgeOutType(edgeType);

                var vertex = GetVertex(edgeNode.VertexNode, nextVertexType);

                foreach (var connector in _vertexConnectors)
                {
                    if (connector.ConnectVertex(edge, vertex)) continue;
                }

                return edge;
            }

            public IVertex GetVertex(TreeVertexNode vertexNode, Type vertexType)
            {
                var vertexObject = (IVertex)vertexNode.Vertex.ToObject(vertexType);
                var vertexLabel = LabelNameResolver.GetLabelName(vertexType);

                var edgeProps = GetEdgeProps(vertexType);

                foreach (var edgeProp in edgeProps)
                {
                    var label = LabelNameResolver.GetLabelName(edgeProp);

                    var edgeNodes = vertexNode.EdgeNodes.Where(e => e.Edge.Label == label).ToList();
                    var inV = GetEdgeInType(edgeProp.PropertyType);
                    var inVLabel = LabelNameResolver.GetLabelName(inV);
                    var outV = GetEdgeOutType(edgeProp.PropertyType);
                    var outVLabel = LabelNameResolver.GetLabelName(outV);

                    foreach (var node in edgeNodes)
                    {
                        var isOut = node.Edge.OutVLabel == outVLabel;

                        var edge = GetEdge(node, edgeProp.PropertyType, isOut);

                        foreach (var connector in _edgeConnectors)
                        {
                            if (connector.ConnectEdge(vertexObject, edge, edgeProp)) continue;
                        }
                    }
                }

                return vertexObject;
            }

            private static Type GetEdgeInType(Type e) => e.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == TypeCache.IHasInV).GetGenericArguments()[0];

            private static Type GetEdgeOutType(Type e) => e.GetInterfaces().FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == TypeCache.IHasOutV).GetGenericArguments()[0];

            private static PropertyInfo[] GetEdgeProps(Type v) => v.GetProperties().Where(p => TypeCache.IEdge.IsAssignableFrom(p.PropertyType)).ToArray();
        }
    }
}