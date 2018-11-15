using FluentAssertions;
using Gremlin.Net.CosmosDb.Schema;
using Gremlin.Net.CosmosDb.Structure;
using Newtonsoft.Json;
using Xunit;

namespace Gremlin.Net.CosmosDb.Serialization
{
    public class TreeTests
    {
        [Fact]
        private void TreeJsonConverter_deserializes_a_tree_that_includes_edges()
        {
            var settings = new JsonSerializerSettings
            {
                Converters = new JsonConverter[]
                {
                    new TreeJsonConverter(),
                    new IEdgeJsonConverter(),
                    new ElementJsonConverter(),
                    new IVertexJsonConverter(),
                },
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateParseHandling = DateParseHandling.DateTimeOffset,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };

            var tree = JsonConvert.DeserializeObject<Tree>(TEST_TREE, settings);

            tree.RootVertexNodes.Should().NotBeNullOrEmpty();
            tree.RootVertexNodes[0].Vertex.Properties.Should().ContainKeys("firstName", "lastName");

            tree.RootVertexNodes[0].EdgeNodes.Should().NotBeNullOrEmpty();
            tree.RootVertexNodes[0].EdgeNodes[0].Edge.Label.Should().Be("purchased");

            tree.RootVertexNodes[0].EdgeNodes[0].VertexNode.Vertex.Should().NotBeNull();
            tree.RootVertexNodes[0].EdgeNodes[0].VertexNode.Vertex.Label.Should().Be("product");
            tree.RootVertexNodes[0].EdgeNodes[0].VertexNode.Vertex.Properties.Should().ContainKeys("name", "price");

            var test = tree.ToObject<Person>();

            test.Length.Should().Be(1);
            test[0].FirstName.Should().Be("John");
            test[0].LastName.Should().Be("Doe");

            test[0].Purchased.Should().NotBeNull();
            test[0].Purchased.InV.Should().ContainItemsAssignableTo<Product>();
            test[0].Purchased.InV[0].Name.Should().Be("gold");
            test[0].Purchased.InV[1].Name.Should().Be("silver");
        }

        private const string TEST_TREE = @"
{
    ""8d168895-d073-437c-9d30-3cac28e51a8e"": {
        ""key"": {
            ""id"": ""8d168895-d073-437c-9d30-3cac28e51a8e"",
            ""label"": ""person"",
            ""type"": ""vertex"",
            ""properties"": {
                ""firstName"": [
                    {
                        ""id"": ""cf18b76a-6d2f-4a91-b749-0f43db38038f"",
                        ""value"": ""John""
                    }
                ],
                ""lastName"": [
                    {
                        ""id"": ""d46ee61b-eda5-4e9f-947d-2025ebf9b221"",
                        ""value"": ""Doe""
                    }
                ]
            }
        },
        ""value"": {
            ""e[81320aad-09ee-4192-aa41-a44c6f41bfab]8d168895-d073-437c-9d30-3cac28e51a8e(person)-purchased->eb92c655-f2b7-46c5-8859-58e136dd593f(product)"": {
                ""key"": {
                    ""id"": ""81320aad-09ee-4192-aa41-a44c6f41bfab"",
                    ""label"": ""purchased"",
                    ""type"": ""edge"",
                    ""inVLabel"": ""product"",
                    ""outVLabel"": ""person"",
                    ""inV"": ""eb92c655-f2b7-46c5-8859-58e136dd593f"",
                    ""outV"": ""8d168895-d073-437c-9d30-3cac28e51a8e""
                },
                ""value"": {
                    ""eb92c655-f2b7-46c5-8859-58e136dd593f"": {
                        ""key"": {
                            ""id"": ""eb92c655-f2b7-46c5-8859-58e136dd593f"",
                            ""label"": ""product"",
                            ""type"": ""vertex"",
                            ""properties"": {
                                ""name"": [
                                    {
                                        ""id"": ""02a6d631-8884-4930-b44e-8a4e6aeb9f49"",
                                        ""value"": ""gold""
                                    }
                                ],
                                ""price"": [
                                    {
                                        ""id"": ""345c798c-4627-419a-bcd5-a4d2009802fb"",
                                        ""value"": 5
                                    }
                                ]
                            }
                        },
                        ""value"": {}
                    },
                }
            },
            ""e[9a5da054-5251-4d39-a0e0-d3e55ec79b67]8d168895-d073-437c-9d30-3cac28e51a8e(person)-purchased->da7df9a9-db0b-4dc8-bb50-d56bb9110005(product)"": {
                ""key"": {
                    ""id"": ""9a5da054-5251-4d39-a0e0-d3e55ec79b67"",
                    ""label"": ""purchased"",
                    ""type"": ""edge"",
                    ""inVLabel"": ""product"",
                    ""outVLabel"": ""person"",
                    ""inV"": ""da7df9a9-db0b-4dc8-bb50-d56bb9110005"",
                    ""outV"": ""8d168895-d073-437c-9d30-3cac28e51a8e""
                },
                ""value"": {
                    ""da7df9a9-db0b-4dc8-bb50-d56bb9110005"": {
                        ""key"": {
                            ""id"": ""da7df9a9-db0b-4dc8-bb50-d56bb9110005"",
                            ""label"": ""product"",
                            ""type"": ""vertex"",
                            ""properties"": {
                                ""name"": [
                                    {
                                        ""id"": ""02a6d631-8884-4930-b44e-8a4e6aeb9f49"",
                                        ""value"": ""silver""
                                    }
                                ],
                                ""price"": [
                                    {
                                        ""id"": ""345c798c-4627-419a-bcd5-a4d2009802fb"",
                                        ""value"": 3
                                    }
                                ]
                            }
                        },
                        ""value"": {}
                    },
                }
            }
        }
    }
}";
    }
}
