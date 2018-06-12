using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using Xunit;

namespace Gremlin.Net.CosmosDb.Structure
{
    public static class LabelAttributeTests
    {
        [Theory, AutoData]
        public static void Ctor_Sets_Properties(
            string name)
        {
            var sut = new LabelAttribute(name);

            sut.Name.Should().Be(name);
        }

        [Fact]
        public static void Ctor_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new LabelAttribute(null));
        }
    }
}