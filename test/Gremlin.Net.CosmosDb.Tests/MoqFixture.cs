using AutoFixture;
using AutoFixture.AutoMoq;

namespace Gremlin.Net.CosmosDb
{
    /// <summary>
    /// Fixture that is already registered to generate mocks
    /// </summary>
    /// <seealso cref="AutoFixture.Fixture"/>
    public class MoqFixture : Fixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoqFixture"/> class.
        /// </summary>
        public MoqFixture()
        {
            this.Customize(new AutoMoqCustomization { ConfigureMembers = true });
        }
    }
}