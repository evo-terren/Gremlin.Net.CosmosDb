using System.Globalization;
using FluentAssertions;
using Gremlin.Net.CosmosDb.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Gremlin.Net.CosmosDb.Serialization
{
    public class ConverterTests
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
        private void JToken_can_be_deserialized_to_IVertex()
        {
            var token = JToken.FromObject(new
            {
                FirstName = "John",
                LastName = "Doe"
            });

            var serializer = JsonSerializer.Create(settings);

            var person = token.ToObject<Person>(serializer);

            person.FirstName.Should().Be("John");
            person.LastName.Should().Be("Doe");
        }
    }
}
