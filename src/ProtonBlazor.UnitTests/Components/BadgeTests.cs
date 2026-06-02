using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Web;
using ProtonBlazor.UnitTests.TestComponents.Badge;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class BadgeTests : BunitTest
    {
        [Test]
        public async Task Badge_Renders_Using_Default_Values()
        {
            var comp = Context.Render<ProBadge>();
            comp.FindAll("span").Should().HaveCount(3, "Default behavior of badge is to render 3 spans");

            await comp.InvokeAsync(() => comp.Instance.HandleBadgeClick(new MouseEventArgs()));
        }

        [Test]
        public async Task Badge_Renders_When_VisibleIsTrue()
        {
            var comp = Context.Render<ProBadge>();
            await comp.SetParametersAndRenderAsync(parameters => parameters.Add(x => x.Visible, true));
            comp.FindAll("span").Should().HaveCount(3, "Visible badge renders 3 spans");
        }

        [Test]
        public async Task Badge_Does_Not_Render_When_VisibleIsFalse()
        {
            var comp = Context.Render<ProBadge>();
            await comp.SetParametersAndRenderAsync(parameters => parameters.Add(x => x.Visible, false));
            comp.FindAll("span").Should().HaveCount(1, "Hidden badge renders 1 span");
        }

        [Test]
        public async Task Badge_Click()
        {
            var comp = Context.Render<BadgeClickTest>();
            var badge = comp.FindComponent<ProBadge>();
            var numeric = comp.FindComponent<ProNumericField<int>>();
            await comp.WaitForAssertionAsync(() => numeric.Instance.Value.Should().Be(0));
            await comp.InvokeAsync(() => badge.Instance.HandleBadgeClick(new MouseEventArgs()));
            await comp.WaitForAssertionAsync(() => numeric.Instance.Value.Should().Be(1));
        }

        [Test]
        public void Badge_AccessibilityAttributes()
        {
            // Arrange
            const string BadgeAriaLabel = "New notifications";

            // Act
            var cut = Context.Render<ProBadge>(parameters => parameters
                .Add(p => p.BadgeAriaLabel, BadgeAriaLabel)
                .Add(p => p.Visible, true)
                .AddChildContent("Test Content")
            );

            // Assert
            var badge = cut.Find(".pro-badge");
            badge.GetAttribute("role").Should().Be("status");
            badge.GetAttribute("aria-live").Should().Be("polite");
            badge.GetAttribute("aria-label").Should().Be(BadgeAriaLabel);
        }
    }
}
