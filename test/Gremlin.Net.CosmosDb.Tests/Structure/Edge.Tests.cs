using FluentAssertions;
using Xunit;

namespace Gremlin.Net.CosmosDb.Structure
{
    public static class EdgeTests
    {
        [Theory, AutoMoqData]
        public static void ToObject_Returns_Expected(SomeObject expected,
            Edge sut)
        {
            sut.Properties[nameof(SomeObject.BooleanProperty)] = expected.BooleanProperty;
            sut.Properties[nameof(SomeObject.DecimalProperty)] = expected.DecimalProperty;
            sut.Properties[nameof(SomeObject.IntegerProperty)] = expected.IntegerProperty;
            sut.Properties[nameof(SomeObject.StringProperty)] = expected.StringProperty;

            var result = sut.ToObject<SomeObject>();

            expected.Should().BeEquivalentTo(result);
        }

        public sealed class SomeObject
        {
            public bool BooleanProperty { get; set; }
            public decimal DecimalProperty { get; set; }
            public int IntegerProperty { get; set; }
            public string StringProperty { get; set; }
        }
    }
}