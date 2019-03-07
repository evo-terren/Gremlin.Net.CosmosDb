using Gremlin.Net.CosmosDb.Structure;
using Gremlin.Net.Process.Traversal;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

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

        private static readonly string[] INSTRUCTIONS_REQUIRING_DOUBLE_UNDERSCORES_FOR_ANONYMOUS
            = new[] { "in", "not", "V" };

        private readonly JsonSerializerSettings _serializerSettings;
        private readonly TextWriter _writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="GremlinQuerySerializer"/> class.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <exception cref="ArgumentNullException">writer</exception>
        public GremlinQuerySerializer(TextWriter writer)
        {
            _serializerSettings = DEFAULT_SERIALIZER_SETTINGS;
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GremlinQuerySerializer"/> class.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="serializerSettings">The serializer settings.</param>
        /// <exception cref="ArgumentNullException">writer</exception>
        public GremlinQuerySerializer(TextWriter writer, JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings ?? throw new ArgumentNullException(nameof(serializerSettings));
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        /// <summary>
        /// Serializes the specified traversal.
        /// </summary>
        /// <param name="traversal">The traversal.</param>
        /// <exception cref="ArgumentNullException">traversal</exception>
        public void Serialize(ITraversal traversal)
        {
            Serialize(traversal, startAnonTraversal: false);
        }

        /// <summary>
        /// Serializes the specified traversal.
        /// </summary>
        /// <param name="traversal">The traversal.</param>
        /// <param name="startAnonTraversal">true indicates that we're starting an anonymous traversal</param>
        /// <exception cref="ArgumentNullException">traversal</exception>
        private void Serialize(ITraversal traversal, bool startAnonTraversal)
        {
            if (traversal == null)
                throw new ArgumentNullException(nameof(traversal));

            Serialize(traversal.Bytecode, startAnonTraversal);
        }

        /// <summary>
        /// Serializes the specified bytecode.
        /// </summary>
        /// <param name="bytecode">The bytecode.</param>
        private void Serialize(Bytecode bytecode)
        {
            Serialize(bytecode, startAnonTraversal: false);
        }

        /// <summary>
        /// Serializes the specified bytecode.
        /// </summary>
        /// <param name="bytecode">The bytecode.</param>
        /// <param name="startAnonTraversal">true indicates that we're starting an anonymous traversal</param>
        private void Serialize(Bytecode bytecode, bool startAnonTraversal)
        {
            var first = true;
            foreach (var instr in bytecode.SourceInstructions)
            {
                if (!first)
                    _writer.Write('.');

                _writer.Write(instr.OperatorName);

                first = false;
                startAnonTraversal = false;
            }

            foreach (var instr in bytecode.StepInstructions)
            {
                if (!first)
                {
                    _writer.Write('.');
                }
                else if (startAnonTraversal
                    && INSTRUCTIONS_REQUIRING_DOUBLE_UNDERSCORES_FOR_ANONYMOUS.Contains(instr.OperatorName))
                {
                    _writer.Write("__.");
                }

                Serialize(instr);

                first = false;
                startAnonTraversal = false;
            }
        }

        /// <summary>
        /// Serializes the specified cardinality.
        /// </summary>
        /// <param name="cardinality">The cardinality.</param>
        private void Serialize(Cardinality cardinality)
        {
            _writer.Write(cardinality.EnumValue);
        }

        /// <summary>
        /// Serializes the specified Column enum.
        /// </summary>
        /// <param name="column">The column.</param>
        private void Serialize(Column column)
        {
            _writer.Write(column.EnumValue);
        }

        /// <summary>
        /// Serializes the specified Pop enum.
        /// </summary>
        /// <param name="pop">The pop.</param>
        private void Serialize(Pop pop)
        {
            _writer.Write(pop.EnumValue);
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
        /// Serializes the specified instruction.
        /// </summary>
        /// <param name="instruction">The instruction.</param>
        private void Serialize(Instruction instruction)
        {
            _writer.Write(instruction.OperatorName);
            _writer.Write('(');
            SerializeListWithCommas(instruction.Arguments);
            _writer.Write(')');
        }

        /// <summary>
        /// Serializes the specified order.
        /// </summary>
        /// <param name="order">The order.</param>
        private void Serialize(Order order)
        {
            _writer.Write(order.EnumValue.ToString().ToLowerInvariant());
        }

        /// <summary>
        /// Serializes the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        private void Serialize(P predicate)
        {
            _writer.Write(predicate.OperatorName);
            _writer.Write('(');
            if (predicate.Value != null)
            {
                var predicateValues = predicate.Value as IEnumerable<dynamic>;
                if (predicateValues?.Any() ?? false)
                    SerializeListWithCommas(predicateValues);
                else if (predicate.Value is ITraversal traversal)
                    Serialize(traversal, startAnonTraversal: true);
                else
                    Serialize(predicate.Value);
            }
            _writer.Write(')');
        }

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void Serialize(object obj)
        {
            var serializedObj = JsonConvert.SerializeObject(obj, Formatting.None, _serializerSettings);

            //make sure objects and arrays are serialized as string values
            //double-escape double quoted values that could be serialized inside the object as property keys/values
            if (serializedObj.Length > 0 && (serializedObj[0] == '{' || serializedObj[0] == '['))
                serializedObj = '"' + serializedObj.Replace("\"", "\\\"").Replace("\\\\\"", "\\\\\\\"") + '"';

            //dollar signs are escaped to avoid parsing errors on Cosmos'
            //end since they are used for interpolation in groovy strings
            serializedObj = serializedObj.Replace("$", "\\$");

            _writer.Write(serializedObj);
        }

        /// <summary>
        /// Serializes the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        private void Serialize(GroovyString str)
        {
            _writer.Write(str?.Value);
        }

        /// <summary>
        /// Serializes the specified <see cref="PartitionKeyIdPair"/>
        /// </summary>
        /// <param name="pair"></param>
        private void Serialize(PartitionKeyIdPair pair)
        {
            _writer.Write($"[\"{pair.PartitionKey}\", \"{pair.Id}\"]");
        }

        /// <summary>
        /// Helper method that serializes a list of things with comma separators.
        /// </summary>
        /// <param name="list">The list.</param>
        private void SerializeListWithCommas(IEnumerable<dynamic> list)
        {
            var addComma = false;
            foreach (var value in list)
            {
                if (addComma)
                    _writer.Write(',');

                if (value is ITraversal traversal)
                    Serialize(traversal, startAnonTraversal: true);
                else
                    Serialize(value);

                addComma = true;
            }
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