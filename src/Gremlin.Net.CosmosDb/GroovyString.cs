namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// A groovy.lang.GString wrapper that supports groovy syntax for strings not supported by C#
    /// </summary>
    public sealed class GroovyString
    {
        /// <summary>
        /// Gets the raw string value.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroovyString"/> class.
        /// </summary>
        /// <param name="value">The raw GString value.</param>
        public GroovyString(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return Value?.ToString();
        }
    }
}