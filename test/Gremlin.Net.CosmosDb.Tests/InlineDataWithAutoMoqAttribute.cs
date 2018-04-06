using AutoFixture.Xunit2;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Combines xUnit's <see cref="Xunit.InlineDataAttribute"/> with AutoFixture's mocking capabilities
    /// </summary>
    /// <seealso cref="AutoFixture.Xunit2.InlineAutoDataAttribute"/>
    public sealed class InlineDataWithAutoMoqAttribute : InlineAutoDataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineDataWithAutoMoqAttribute"/> class.
        /// </summary>
        /// <param name="values">The data values to pass to the theory.</param>
        public InlineDataWithAutoMoqAttribute(params object[] values)
            : base(new AutoMoqDataAttribute(), values)
        {
        }
    }
}