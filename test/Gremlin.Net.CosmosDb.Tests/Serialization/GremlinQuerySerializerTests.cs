using FluentAssertions;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Remote;
using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using Xunit;

namespace Gremlin.Net.CosmosDb.Serialization
{
	public class GremlinQuerySerializerTests
	{
		private static readonly JsonSerializerSettings settings = new JsonSerializerSettings
		{
			Converters = new JsonConverter[]
				{
					new TreeJsonConverter(),
					new IEdgeJsonConverter(),
					new ElementJsonConverter(),
					new IVertexJsonConverter(),
					new IsoDateTimeConverter
					{
						DateTimeStyles = DateTimeStyles.AdjustToUniversal
					}
				},
			DateFormatHandling = DateFormatHandling.IsoDateFormat,
			DateParseHandling = DateParseHandling.DateTimeOffset,
			DateTimeZoneHandling = DateTimeZoneHandling.Utc
		};

		[Fact]
		private void GremlinQuerySerializer()
		{
			//	TODO: Need to mock
			var server = new GremlinServer();

			var client = new GremlinClient(server);

			var g = AnonymousTraversalSource.Traversal().WithRemote(new DriverRemoteConnection(client));

			var entQuery = g.V().HasLabel("Enterprise")
				.Has("Registry", "xxx")
				.Has("PrimaryAPIKey", "XXX");

			var appQuery = g.V(("aaa", "AAA"))
				.HasLabel("Application")
				.Has("Registry", "xxx2");

			var query = g.V(entQuery).AddE("Owns").To(appQuery).ToGremlinQuery();

			query.Should().Be("g.V(g.V().hasLabel(\"Enterprise\").has(\"Registry\",\"xxx\").has(\"PrimaryAPIKey\",\"XXX\")).addE(\"Owns\").to(g.V([\"aaa\", \"AAA\"]).hasLabel(\"Application\").has(\"Registry\",\"xxx2\"))");
		}
	}
}