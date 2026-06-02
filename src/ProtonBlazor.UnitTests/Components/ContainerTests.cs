using AwesomeAssertions;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class ContainerTests : BunitTest
    {
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void GuttersProperty_AddsClass(bool gutters)
        {
            // Arrange
            var component = Context.Render<ProContainer>(builder => builder
                .Add(p => p.Gutters, gutters)
            );

            // Assert
            if (gutters)
            {
                component.Markup.Should().Contain("pro-container--gutters");
            }
            else
            {
                component.Markup.Should().NotContain("pro-container--gutters");
            }
        }
    }
}
