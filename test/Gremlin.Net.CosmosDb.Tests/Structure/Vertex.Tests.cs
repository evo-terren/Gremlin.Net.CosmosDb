using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Gremlin.Net.CosmosDb.Structure
{
    public static class VertexTests
    {
        [Theory, AutoMoqData]
        public static void ToObject_Returns_Expected(SomeObject expected,
            Vertex sut)
        {
            sut.Properties[nameof(SomeObject.BooleanProperty)] = new[] { new VertexPropertyValue { Value = expected.BooleanProperty } };
            sut.Properties[nameof(SomeObject.BooleanEnumerableProperty)] = expected.BooleanEnumerableProperty.Select(v => new VertexPropertyValue { Value = v }).ToList();
            sut.Properties[nameof(SomeObject.DecimalProperty)] = new[] { new VertexPropertyValue { Value = expected.DecimalProperty } };
            sut.Properties[nameof(SomeObject.DecimalEnumerableProperty)] = expected.DecimalEnumerableProperty.Select(v => new VertexPropertyValue { Value = v }).ToList();
            sut.Properties[nameof(SomeObject.IntegerProperty)] = new[] { new VertexPropertyValue { Value = expected.IntegerProperty } };
            sut.Properties[nameof(SomeObject.IntegerEnumerableProperty)] = expected.IntegerEnumerableProperty.Select(v => new VertexPropertyValue { Value = v }).ToList();
            sut.Properties[nameof(SomeObject.StringProperty)] = new[] { new VertexPropertyValue { Value = expected.StringProperty } };
            sut.Properties[nameof(SomeObject.StringEnumerableProperty)] = expected.StringEnumerableProperty.Select(v => new VertexPropertyValue { Value = v }).ToList();

            var result = sut.ToObject<SomeObject>();

            expected.Should().BeEquivalentTo(result);
        }

        public sealed class SomeObject
        {
            public IEnumerable<bool> BooleanEnumerableProperty
            {
                get { return _booleans; }
                set { _booleans = value.ToList(); }
            }

            public bool BooleanProperty { get; set; }

            public IEnumerable<decimal> DecimalEnumerableProperty
            {
                get { return _decimals; }
                set { _decimals = value.ToList(); }
            }

            public decimal DecimalProperty { get; set; }

            public IEnumerable<int> IntegerEnumerableProperty
            {
                get { return _integers; }
                set { _integers = value.ToList(); }
            }

            public int IntegerProperty { get; set; }

            public IEnumerable<string> StringEnumerableProperty
            {
                get { return _strings; }
                set { _strings = value.ToList(); }
            }

            public string StringProperty { get; set; }
            private IEnumerable<bool> _booleans;
            private IEnumerable<decimal> _decimals;
            private IEnumerable<int> _integers;
            private IEnumerable<string> _strings;
        }
    }
}