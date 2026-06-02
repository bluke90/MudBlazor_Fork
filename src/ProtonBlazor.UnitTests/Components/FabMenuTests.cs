using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Web;
using ProtonBlazor.UnitTests.TestComponents.Button;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components;

[TestFixture]
public class FabMenuTests : BunitTest
{
    [Test]
    public void RendersCorrectly()
    {
        var comp = Context.Render<FabMenuTest>();
        comp.FindAll(".pro-fab-menu").Count.Should().Be(1);
        comp.FindAll(".pro-fab-menu.pro-fab-menu-open").Count.Should().Be(0);
        comp.FindAll(".pro-fab-menu-item").Count.Should().Be(3);
    }

    [Test]
    public async Task RendersCorrectlyOnClick()
    {
        var comp = Context.Render<FabMenuTest>();

        await comp.FindAll(".pro-fab-menu-button")[0].ClickAsync();
        await comp.WaitForAssertionAsync(() => { comp.FindAll(".pro-fab-menu.pro-fab-menu-open").Count.Should().Be(1); });

        await comp.FindAll(".pro-fab-menu-item")[0].ClickAsync();
        await comp.WaitForAssertionAsync(() => { comp.FindAll(".pro-fab-menu.pro-fab-menu-open").Count.Should().Be(0); });
    }

    [Test]
    public async Task RendersCorrectlyOnTouch()
    {
        var compNoHover = Context.Render<FabMenuTest>();

        compNoHover.FindAll(".pro-fab-menu-button")[0].TouchStart();
        await compNoHover.FindAll(".pro-fab-menu-button")[0].ClickAsync();
        await compNoHover.WaitForAssertionAsync(() => { compNoHover.FindAll(".pro-fab-menu.pro-fab-menu-open").Count.Should().Be(1); });

        compNoHover.FindAll(".pro-fab-menu-button")[0].TouchStart();
        await compNoHover.FindAll(".pro-fab-menu-item")[0].ClickAsync();
        await compNoHover.WaitForAssertionAsync(() => { compNoHover.FindAll(".pro-fab-menu.pro-fab-menu-open").Count.Should().Be(0); });

        var compHover = Context.Render<FabMenuTest>(parameters => parameters.Add(p => p.OpenOnMouseHover, true));

        compHover.FindAll(".pro-fab-menu-button")[0].TouchStart();
        await compHover.FindAll(".pro-fab-menu-button")[0].ClickAsync();
        await compHover.WaitForAssertionAsync(() => { compHover.FindAll(".pro-fab-menu.pro-fab-menu-open").Count.Should().Be(1); });

        compHover.FindAll(".pro-fab-menu-button")[0].TouchStart();
        await compHover.FindAll(".pro-fab-menu-item")[0].ClickAsync();
        await compHover.WaitForAssertionAsync(() => { compHover.FindAll(".pro-fab-menu.pro-fab-menu-open").Count.Should().Be(0); });
    }

    [Test]
    public async Task RendersCorrectlyOnHover()
    {
        var comp = Context.Render<FabMenuTest>(parameters => parameters.Add(p => p.OpenOnMouseHover, true));

        await comp.FindAll(".pro-fab-menu-container")[0].MouseEnterAsync(new MouseEventArgs());
        await comp.WaitForAssertionAsync(() => { comp.FindAll(".pro-fab-menu.pro-fab-menu-open").Count.Should().Be(1); });

        await comp.FindAll(".pro-fab-menu-item")[0].ClickAsync();
        await comp.WaitForAssertionAsync(() => { comp.FindAll(".pro-fab-menu.pro-fab-menu-open").Count.Should().Be(0); });

        await comp.FindAll(".pro-fab-menu-container")[0].MouseEnterAsync(new MouseEventArgs());
        await comp.WaitForAssertionAsync(() => { comp.FindAll(".pro-fab-menu.pro-fab-menu-open").Count.Should().Be(1); });

        await comp.FindAll(".pro-fab-menu-container")[0].MouseLeaveAsync(new MouseEventArgs());
        await comp.WaitForAssertionAsync(() => { comp.FindAll(".pro-fab-menu.pro-fab-menu-open").Count.Should().Be(0); });
    }

    [TestCase(Direction.Top, "pro-fab-menu-direction-top")]
    [TestCase(Direction.Bottom, "pro-fab-menu-direction-bottom")]
    [TestCase(Direction.Left, "pro-fab-menu-direction-left")]
    [TestCase(Direction.Right, "pro-fab-menu-direction-right")]
    [TestCase(Direction.Start, "pro-fab-menu-direction-start")]
    [TestCase(Direction.End, "pro-fab-menu-direction-end")]
    public void AppliesDirectionClass(Direction direction, string expectedClass)
    {
        var comp = Context.Render<ProFabMenu>(parameters => parameters
            .Add(p => p.Direction, direction));

        comp.Find(".pro-fab-menu").ClassList.Contains(expectedClass).Should().BeTrue();
    }

    [TestCase(Origin.TopLeft, "pro-fab-anchor-top-left")]
    [TestCase(Origin.TopCenter, "pro-fab-anchor-top-center")]
    [TestCase(Origin.TopRight, "pro-fab-anchor-top-right")]
    [TestCase(Origin.CenterLeft, "pro-fab-anchor-center-left")]
    [TestCase(Origin.CenterCenter, "pro-fab-anchor-center-center")]
    [TestCase(Origin.CenterRight, "pro-fab-anchor-center-right")]
    [TestCase(Origin.BottomLeft, "pro-fab-anchor-bottom-left")]
    [TestCase(Origin.BottomCenter, "pro-fab-anchor-bottom-center")]
    [TestCase(Origin.BottomRight, "pro-fab-anchor-bottom-right")]
    public void AppliesAnchorClassWhenFixed(Origin anchor, string expectedClass)
    {
        var comp = Context.Render<ProFabMenu>(parameters => parameters
            .Add(p => p.Fixed, true)
            .Add(p => p.Anchor, anchor));

        var container = comp.Find(".pro-fab-menu-container");
        container.ClassList.Contains("fixed").Should().BeTrue();
        container.ClassList.Contains(expectedClass).Should().BeTrue();
    }

    [Test]
    public void DoesNotApplyAnchorClassWhenNotFixed()
    {
        var comp = Context.Render<ProFabMenu>(parameters => parameters
            .Add(p => p.Anchor, Origin.TopLeft));

        var container = comp.Find(".pro-fab-menu-container");
        container.ClassList.Contains("fixed").Should().BeFalse();
        container.ClassList.Contains("pro-fab-anchor-top-left").Should().BeFalse();
    }

    [Test]
    [Combinatorial]
    public void FabMenuItem_ShouldRenderAnchorIfHrefIsSet(
        [Values("", "ASDF", "_blank")] string target,
        [Values(null, "noopener", "nofollow")] string rel)
    {
        var comp = Context.Render<ProFabMenu>(parameters => parameters
            .AddChildContent<ProFabMenuItem>(item => item
                .Add(x => x.Href, "https://example.com")
                .Add(x => x.Target, target)
                .Add(x => x.Rel, rel)
                .Add(x => x.Label, "Link")));

        var item = comp.Find(".pro-fab-menu-item");

        item.TagName.Should().Be("A");
        item.GetAttribute("href").Should().Be("https://example.com");
        item.GetAttribute("target").Should().Be(target);

        var expectedRel = rel ?? (target == "_blank" ? "noopener" : null);
        item.GetAttribute("rel").Should().Be(expectedRel);
    }
}
