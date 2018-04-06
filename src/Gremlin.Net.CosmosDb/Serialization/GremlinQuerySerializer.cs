using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
using System.IO;

namespace Gremlin.Net.CosmosDb.Serialization
{
    /// <summary>
    /// Helper gremlin query serializer for <see cref="Gremlin.Net.Process.Traversal.ITraversal"/> objects
    /// </summary>
    /// <seealso cref="System.IDisposable"/>
    internal sealed class GremlinQuerySerializer : IDisposable
    {
        private static readonly JsonSerializerSettings DEFAULT_SERIALIZER_SETTINGS = new JsonSerializerSettings
        {
            Converters = new JsonConverter[]
            {
                new IsoDateTimeConverter
                {
                    DateTimeStyles = DateTimeStyles.AssumeUniversal
                }
            },
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateParseHandling = DateParseHandling.DateTimeOffset,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };

        private readonly JsonSerializer _serializer;
        private readonly TextWriter _writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="GremlinQuerySerializer"/> class.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <exception cref="ArgumentNullException">writer</exception>
        public GremlinQuerySerializer(TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            _serializer = JsonSerializer.Create(DEFAULT_SERIALIZER_SETTINGS);
            _writer = writer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GremlinQuerySerializer"/> class.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <exception cref="ArgumentNullException">writer</exception>
        public GremlinQuerySerializer(TextWriter writer, JsonSerializerSettings serializerSettings)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            _serializer = JsonSerializer.Create(serializerSettings);
            _writer = writer;
        }

        /// <summary>
        /// Serializes the specified traversal.
        /// </summary>
        /// <param name="traversal">The traversal.</param>
        /// <exception cref="ArgumentNullException">traversal</exception>
        public void Serialize(ITraversal traversal)
        {
            if (traversal == null)
                throw new ArgumentNullException(nameof(traversal));

            _writer.Write("g");

            foreach (var instr in traversal.Bytecode.StepInstructions)
            {
                _writer.Write('.');
                Serialize(instr);
            }
        }

        /// <summary>
        /// Serializes the specified instruction.
        /// </summary>
        /// <param name="instruction">The instruction.</param>
        private void Serialize(Instruction instruction)
        {
            _writer.Write(instruction.OperatorName);
            _writer.Write('(');
            var addComma = false;
            foreach (var arg in instruction.Arguments)
            {
                if (addComma)
                    _writer.Write(',');

                Serialize(arg);

                addComma = true;
            }
            _writer.Write(')');
        }

        /// <summary>
        /// Serializes the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        private void Serialize(TraversalPredicate predicate)
        {
            _writer.Write(predicate.OperatorName);
            _writer.Write('(');
            if (predicate.Value != null)
                Serialize(predicate.Value);
            _writer.Write(')');
        }

        /// <summary>
        /// Serializes the specified order.
        /// </summary>
        /// <param name="order">The order.</param>
        private void Serialize(Order order)
        {
            _writer.Write(order.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Serializes the specified cardinality.
        /// </summary>
        /// <param name="cardinality">The cardinality.</param>
        private void Serialize(Cardinality cardinality)
        {
            _writer.Write(cardinality.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Serializes the specified enum.
        /// </summary>
        /// <param name="enum">The enum.</param>
        private void Serialize(Enum @enum)
        {
            Serialize(@enum.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Serializes the specified unique identifier.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        private void Serialize(Guid guid)
        {
            Serialize(guid.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void Serialize(object obj)
        {
            _serializer.Serialize(_writer, obj);
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;

                if (disposing)
                {
                    _writer.Dispose();
                }
            }
        }

        #endregion IDisposable Support
    }
}