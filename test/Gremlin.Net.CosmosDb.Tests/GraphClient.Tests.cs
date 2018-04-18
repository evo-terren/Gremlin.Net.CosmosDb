using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace Gremlin.Net.CosmosDb
{
    public static class GraphClientTests
    {
        [Theory, AutoData]
        public static void CreateTraversalSource_ReturnsExpected(
            GraphClient sut)
        {
            var result = sut.CreateTraversalSource();

            result.Should().BeOfType<IGraphTraversalSource>();
        }
    }
}