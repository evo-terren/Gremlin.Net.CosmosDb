# Gremlin.Net.CosmosDb
 
**I have officially abandoned this project. If you are looking for a good alternative, I recommend https://github.com/ExRam/ExRam.Gremlinq at the time of writing.**

Gremlin.Net.CosmosDb is a helper library to be used in conjuntion with [Gremlin.Net](https://github.com/apache/tinkerpop/tree/master/gremlin-dotnet).

To date, the [only documented way](https://docs.microsoft.com/en-us/azure/cosmos-db/create-graph-dotnet) to utilize Gremlin.Net with a CosmosDb graph instance is to utilize the `gremlinClient.SubmitAsync<dynamic>("gremlin query here")` method, which only accepts a string and returns a `dynamic`, which is pretty error-prone and not very helpful in a production C# application. The primary limitation to utilizing Gremlin.Net to its fullest potential is that it sends the gremlin query to the server as java bytecode, [something CosmosDb does not yet support](https://feedback.azure.com/forums/263030-azure-cosmos-db/suggestions/33632779-support-gremlin-bytecode-to-enable-the-fluent-api).

Before proceeding, understand that this is VERY MUCH a work in progress. I will work on it as often as I am able, but definitely cannot make any promises. I am honestly only pursuing this at the time due to work/business requirements. If things change, I will likely bail.

## Basic Usage

To use this library, add your using, setup a `GraphClient`, create a traversal source and query to your heart's content:

```c#
using Gremlin.Net.CosmosDb;

...

using (var graphClient = new GraphClient("Your Gremlin Cosmos Db Host", "Your DB Name", 
			"Your Graph Name", "Your Access Key"))
{
    //Create the traversal source using the graph client
    //This is the typical "g" that your gremlin queries start with
    var g = graphClient.CreateTraversalSource();

    //build a query using Gremlin.Net traversals
    var query = g.V("some-vertex-id").Out("some-edge-label");

    //use the QueryAsync(ITraversal traversal) extension methods that accept Gremlin.Net traversals
    var response = await graphClient.QueryAsync(query);

    //use the .ToGremlinQuery() extension method if you need the raw query 
    //in string form for any reason (logging, validating this code actually works, etc.)
    Console.WriteLine(query.ToGremlinQuery());

    Console.WriteLine();
    Console.WriteLine("Response:");
    foreach (var result in response)
    {
        Console.WriteLine(result);
    }
}
```

Newtonsoft.Json is used under the hood to perform query serialization, so you have a lot of flexibility at your disposal when creating queries:

```c#
var uniqueId = new Guid("c551b8d1-1d06-4513-a7b3-f81a925bd676");
var todaysDate = DateTimeOffset.Now;
var query = g.V(uniqueId).property("some-date', todaysDate);
```

## Partitioned Graph
If you're partitioning your CosmosDb graph, you can query look up partitioned vertices like so:
```c#
using (var graphClient = new GraphClient(...)
{
    var g = graphClient.CreateTraversalSource();

    // lookup by id
    var slowQuery = g.V("some-vertex-id");

    // lookup by partition key and id
    var fastQuery = g.V(("some-vertex-partition-key", "some-vertex-id"));

    // multiple vertices
    var fastQuery = g.V(("some-vertex-partition-key", "some-vertex-id"), ("other-partition-key", "other-id"));
}
```

## Utlize Newtonsoft.Json De-Serialization

The `QueryAsync()` method has overloads that allow you to specify the return type as well as optionally setting the Newtonsoft.Json serializer settings if your application requires:

```c#
using Gremlin.Net.CosmosDb;
using Newtonsoft.Json;

...

var query = //your gremlin query traversal
var serializerSettings = new JsonSerializerSettings
{
    Converters = new []
    {
        new YourCustomTypeJsonConverter()
    }
};
var response = await graphClient.QueryAsync<YourCustomType>(query, serializerSettings);
```

For more information about Newtonsoft.Json, please refer to their documentation: https://www.newtonsoft.com/json.

## Graph With Custom Schema

You can utilize the types defined in Gremlin.Net.CosmosDb.Structure to further utilize the compiler to maintain schema definitions:

```c#
using Gremlin.Net.CosmosDb.Structure;

[Label("person")]
public class PersonVertex : VertexBase
{
    public PersonPurchasedProductEdge Purchased { get; }
}

[Label("product")]
public class ProductVertex : VertexBase
{
    public PersonPurchasedProductEdge PurchasedBy { get; }
}

[Label("purchased")]
public class PersonPurchasedProductEdge : EdgeBase<PersonVertex, ProductVertex>
{

}

...

var query = g.V<PersonVertex>("person-vertex-id")   //typed to a specific Vertex class
             .Out(pv => pv.Purchased)               //this gets us to the ProductVertex
             .In(pv => pv.PurchasedBy)              //now we're back to the PersonVertex
             .OutE(pv => pv.Purchased)              //now we're on the edge (still type-specific)
             .InV();                                //back to the product vertex
var response = await graphClient.QueryAsync(query);

foreach (ProductVertex product in response)
{
	...
}
```

## Strongly-Typed Vertex and Edge Objects

Create strongly-typed vertex and edge objects to define properties and help with deserialization.

```c#
using Gremlin.Net.CosmosDb.Structure;

[Label("person")]
public class PersonVertex : VertexBase
{
    public string Name { get; set; }

    public DateTimeOffset Birthdate { get; set; }
}

...
//usage in application
...

var query = g.V<PersonVertex>().Has(v => v.Name, "Todd").Property(v => v.Birthdate, DateTimeOffset.Now);
var response = await graphClient.QueryAsync(query);

foreach (var vertex in response)
{
    Console.WriteLine(vertex.Birthdate.ToString());
    Console.WriteLine(vertex.Name);
}
```

### A Note on IEnumerable Properties

In order to specify that a property has an array of values instead of just one, you normally must specify the Cardinality to be List (read more about graph properties here: http://tinkerpop.apache.org/docs/current/reference/#vertex-properties). However, there are certain things about the usage of cardinality that aren't as inuitive to the user as I would like. For example, `g.V('some-id').property(list, 'some-property', 'some-value')` ALWAYS adds the value to the existing list no matter what. Updating the array of values becomes non-intuitive, especially if you are trying to update the properties in one query.

What does all of this mean to you?

When using the schema-bound `Property()` method with an enumerable property, this will likely add more gremlin query than you may initially expect, but this is to your benefit (hopefully).

Assuming you have an object defined as such:

```c#
using Gremlin.Net.CosmosDb.Structure;

public class YourVertex : VertexBase
{
    public IEnumerable<int> Numbers { get; set; }
}
```

Performing this traversal:

```c#
var query = g.V<YourVertex>('some-id').Property(v => v.Numbers, new [] { 4, 5, 6 });
```

Results in the following gremlin query:

```
g.V('some-id').sideEffect(properties('Numbers').drop()).property(list, 'Numbers', 4).property(list, 'Numbers', 5).property(list, 'Numbers', 6)
```

This will effectively drop the existing set of values and add 4, then 5, then 6.

If you would like to simply add a single value, you can use this syntax:

```c#
g.V<YourVertex>('some-id').Property(v => v.Numbers, 8);
```

This results in the following gremlin query:

```
g.V('some-id').property(list, 'Numbers', 8)
```
