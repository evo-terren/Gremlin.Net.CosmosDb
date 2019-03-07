using System;
using System.Collections.Generic;
using System.Linq;
using Gremlin.Net.Process.Traversal;
using Xunit;

namespace Gremlin.Net.CosmosDb.Serialization
{
    public class QuerySerializerTests
    {
        private IGraphTraversalSource g = new GraphTraversalSource();

        [Fact]
        public void GetSingleVertex()
        {
            var id = Guid.NewGuid();
            var traversal = g.V(id);
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V(""{id}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        #region HasId
        [Fact(Skip = "Not supported")]
        public void SerializeHasIdWithIEnumerableOfObjectsWithOneItem()
        {
            var list = new List<object> { Guid.NewGuid() };
            var traversal = g.V().HasId(list.First(), list.Skip(1));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(""{list[0]}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializeHasIdWithArrayOfGuidsWithOneItem()
        {
            var list = new List<Guid> { Guid.NewGuid() };
            var traversal = g.V().HasId(list.First(), list.Skip(1).ToArray());
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(""{list[0]}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeHasIdWithArrayOfObjectsWithOneItem()
        {
            var list = new List<object> { Guid.NewGuid() };
            var traversal = g.V().HasId(list.First(), list.Skip(1).ToArray());
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(""{list[0]}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializeHasIdWithIEnumerableOfObjectsWithMultipleItems()
        {
            var list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var traversal = g.V().HasId(list.First(), list.Skip(1));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(""{list[0]}"",""{list[1]}"",""{list[2]}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializeHasIdWithArrayOfGuidsWithMultipleItems()
        {
            var list = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var traversal = g.V().HasId(list.First(), list.Skip(1).ToArray());
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(""{list[0]}"",""{list[1]}"",""{list[2]}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeHasIdWithArrayOfObjectsWithMultipleItems()
        {
            var list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var traversal = g.V().HasId(list.First(), list.Skip(1).ToArray());
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(""{list[0]}"",""{list[1]}"",""{list[2]}"")";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        #endregion HasId

        #region predicates accepting a single object
        [Fact]
        public void SerializePredicateEqualToNumber()
        {
            int value = 5;
            var traversal = g.V().Has("count", P.Eq(value));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().has(""count"",eq({value}))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateEqualToNumberAsObject()
        {
            object value = 5;
            var traversal = g.V().Has("count", P.Eq(value));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().has(""count"",eq({value}))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateEqualToString()
        {
            string value = "John";
            var traversal = g.V().Has("name", P.Eq(value));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().has(""name"",eq(""{value}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateEqualToStringAsObject()
        {
            object value = "John";
            var traversal = g.V().Has("name", P.Eq(value));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().has(""name"",eq(""{value}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        #endregion predicates accepting a single object

        #region predicates accepting exactly two numbers
        [Fact]
        public void SerializePredicateBetweenWithListOfObjects()
        {
            var list = new List<object> { 5, 10 };
            var traversal = g.V().Has("count", P.Between(list));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().has(""count"",between({list[0]},{list[1]}))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializePredicateBetweenWithListOfInts()
        {
            var list = new List<int> { 5, 10 };
            var traversal = g.V().Has("count", P.Between(list));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().has(""count"",between({list[0]},{list[1]}))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializePredicateBetweenWithArrayOfInts()
        {
            var array = new[] { 5, 10 };
            var traversal = g.V().Has("count", P.Between(array));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().has(""count"",between({array[0]},{array[1]}))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateBetweenWithTwoDistinctInts()
        {
            var array = new[] { 5, 10 };
            var traversal = g.V().Has("count", P.Between(array[0], array[1]));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().has(""count"",between({array[0]},{array[1]}))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        #endregion predicates accepting exactly two numbers

        #region predicates acceptiong a list of objects

        [Fact]
        public void SerializePredicateWithinWithListOfSingleObject()
        {
            var list = new List<object> { Guid.NewGuid() };
            var traversal = g.V().HasId(P.Within(list));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{list[0]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithListOfObjects()
        {
            var list = new List<object> { Guid.NewGuid(), Guid.NewGuid() };
            var traversal = g.V().HasId(P.Within(list));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{list[0]}"",""{list[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithListOfSingleGuid()
        {
            var list = new List<Guid> { Guid.NewGuid() };
            var traversal = g.V().HasId(P.Within(list));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{list[0]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithListOfGuids()
        {
            var list = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            var traversal = g.V().HasId(P.Within(list));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{list[0]}"",""{list[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithArrayOfSingleGuid()
        {
            var array = new[] { Guid.NewGuid() };
            var traversal = g.V().HasId(P.Within(array));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithArrayOfGuids()
        {
            var array = new[] { Guid.NewGuid(), Guid.NewGuid() };
            var traversal = g.V().HasId(P.Within(array));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}"",""{array[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializePredicateWithinWithIEnumerableOfSingleObject()
        {
            var list = new List<object> { Guid.NewGuid() }.Select(item => item);
            var traversal = g.V().HasId(P.Within(list));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{list.First()}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializePredicateWithinWithIEnumerableOfObjects()
        {
            var list = new List<object> { Guid.NewGuid(), Guid.NewGuid() }.Select(item => item);
            var traversal = g.V().HasId(P.Within(list));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{list.First()}"",""{list.Skip(1).First()}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializePredicateWithinWithIEnumerableOfSingleGuid()
        {
            var list = new List<Guid> { Guid.NewGuid() }.Select(item => item);
            var traversal = g.V().HasId(P.Within(list));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{list.First()}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact(Skip = "Not supported")]
        public void SerializePredicateWithinWithIEnumerableOfGuids()
        {
            var list = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }.Select(item => item);
            var traversal = g.V().HasId(P.Within(list));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{list.First()}"",""{list.Skip(1).First()}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithTwoDistinctGuids()
        {
            var array = new[] { Guid.NewGuid(), Guid.NewGuid() };
            var traversal = g.V().HasId(P.Within(array[0], array[1]));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}"",""{array[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithListOfSingleString()
        {
            var array = new List<string> { "abc" };
            var traversal = g.V().HasId(P.Within(array));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithListOfStrings()
        {
            var list = new List<string> { "abc", "123" };
            var traversal = g.V().HasId(P.Within(list));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{list[0]}"",""{list[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithArrayOfSingleString()
        {
            var array = new[] { "abc" };
            var traversal = g.V().HasId(P.Within(array));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithArrayOfStrings()
        {
            var array = new[] { "abc", "123" };
            var traversal = g.V().HasId(P.Within(array));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}"",""{array[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializePredicateWithinWithTwoDistinctStrings()
        {
            var array = new[] { "abc", "123" };
            var traversal = g.V().HasId(P.Within(array[0], array[1]));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().hasId(within(""{array[0]}"",""{array[1]}""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        #endregion predicates acceptiong a list of objects

        #region anonymous traversals

        [Fact]
        public void SerializeAnonymousVeeAsStepArgument()
        {
            var list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var traversal = g.V().Has("p1", __.V("someId").Values<string>("p1"));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().has(""p1"",__.V(""someId"").values(""p1""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeAnonymousVeeAsPredicateArgument()
        {
            var list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var traversal = g.V().Has("p1", P.Neq(__.V("someId").Values<string>("p1")));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().has(""p1"",neq(__.V(""someId"").values(""p1"")))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeAnonymousNotAsStepArgument()
        {
            var list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var traversal = g.V().And(__.Has("prop1"), __.Not(__.Has("prop2")));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().and(has(""prop1""),__.not(has(""prop2"")))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeAnonymousNotAsPredicateArgument()
        {
            var list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var traversal = g.V().Where(P.Neq(__.Not(__.Has("prop2")))); // contrived example
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V().where(neq(__.not(has(""prop2""))))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeAnonymousInAsStepArgument()
        {
            var list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var traversal = g.V("someId").Optional<object>(__.In("edgeLabel").In("edgeLabel2"));
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V(""someId"").optional(__.in(""edgeLabel"").in(""edgeLabel2""))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }

        [Fact]
        public void SerializeAnonymousInAsPredicateArgument()
        {
            var list = new List<object> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var traversal = g.V("someId").Where(P.Eq(__.In("edgeLabel").In("edgeLabel2"))); // contrived example
            var actualGremlinQuery = traversal.ToGremlinQuery();
            var expectedGremlinQuery = $@"g.V(""someId"").where(eq(__.in(""edgeLabel"").in(""edgeLabel2"")))";
            Assert.Equal(expectedGremlinQuery, actualGremlinQuery);
        }
        #endregion anonymous traversals
    }
}