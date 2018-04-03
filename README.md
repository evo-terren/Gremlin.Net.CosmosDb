# Gremlin.Net.CosmosDb

Gremlin.Net.CosmosDb is a helper library to be used in conjuntion with Gremlin.Net [Gremlin.Net](https://github.com/apache/tinkerpop/tree/master/gremlin-dotnet).

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

    //use the SubmitAsync(ITraversal traversal) extension methods that accept Gremlin.Net traversals
    var response = await graphClient.SubmitAsync(query);

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

## Utlize Newtonsoft.Json De-Serialization

The `SubmitAsync()` method has overloads that allow you to specify the return type as well as optionally setting the Newtonsoft.Json serializer settings if your application requires:

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
var response = await graphClient.SubmitAsync<YourCustomType>(query, serializerSettings);
```

For more information about Newtonsoft.Json, please refer to their documentation: https://www.newtonsoft.com/json.

## Graph With Custom Schema

You can utilize the types defined in Gremlin.Net.CosmosDb.Structure to further utilize the compiler to maintain schema definitions:

```c#
using Gremlin.Net.CosmosDb.Structure;

[Label("person")]
public class PersonVertex : Vertex
{
    public PersonPurchasedProductEdge Purchased { get; }
}

[Label("product")]
public class ProductVertex : Vertex
{
    public PersonPurchasedProductEdge PurchasedBy { get; }
}

[Label("purchased")]
public class PersonPurchasedProductEdge : Edge<Person, Product>
{

}

...

var query = g.V<PersonVertex>("person-vertex-id")   //typed to a specific Vertex class
             .Out(pv => pv.Purchased)               //this gets us to the ProductVertex
             .In(pv => pv.PurchasedBy)              //now we're back to the PersonVertex
             .OutE(pv => pv.Purchased)              //now we're on the edge (still type-specific)
             .InV();                                //back to the product vertex
var response = await graphClient.SubmitAsync(query);

foreach (ProductVertex product in response)
{
	...
}
```

## Vertex and Edge With Property Containers

This is a TBD, but my main goal is to enable the ability to specify the model for each of your vertex and edge objects' "properties" bag.

Something like this (DOES NOT WORK YET):

```c#
public class Person
{
	public string Name { get; set; }

	public DateTimeOffset Birthdate { get; set; }
}

public class PersonVertex : Vertex<Person>
{

}

...
//usage in application
...

var query = g.V<PersonVertex>().has(p => p.Name, "Todd");
```
