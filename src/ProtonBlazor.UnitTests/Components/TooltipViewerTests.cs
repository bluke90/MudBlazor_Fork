using AwesomeAssertions;
using Bunit;
using ProtonBlazor.UnitTests.TestComponents.Tooltip;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class TooltipViewerTests : BunitTest
    {
        [Test]
        public void Tooltip_InlineTrue_Allows_FullWidth_Child()
        {
            var comp = Context.Render<TooltipFullWidthViewerTest>();

            var inlineWrapper = comp.Find(".pro-tooltip-root.pro-tooltip-inline");
            inlineWrapper.Should().NotBeNull();

            comp.Find("#btn-inline-full").TagName.Should().Be("BUTTON");
        }

        [Test]
        public void Tooltip_InlineFalse_Control_Renders_FullWidth_Child()
        {
            var comp = Context.Render<TooltipFullWidthViewerTest>();
            comp.Find("#btn-block-full").TagName.Should().Be("BUTTON");
        }
    }
}
