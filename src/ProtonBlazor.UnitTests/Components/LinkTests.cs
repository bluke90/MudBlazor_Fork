using AngleSharp.Dom;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ProtonBlazor.UnitTests.TestComponents.Link;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components;

[TestFixture]
public class LinkTests : BunitTest
{
    [Test]
    public void DefaultPropertyValues()
    {
        var comp = Context.Render<ProLink>();

        comp.Instance.Color.Should().Be(Color.Primary);
        comp.Instance.Typo.Should().Be(Typo.inherit);
        comp.Instance.Underline.Should().Be(Underline.Hover);
        comp.Instance.Href.Should().BeNull();
        comp.Instance.Target.Should().BeNull();
        comp.Instance.Disabled.Should().BeFalse();
    }

    [Test]
    public void DefaultTypo_ShouldInheritParentTypography()
    {
        var comp = Context.Render<ProLink>();

        var linkElement = comp.Find("a");
        linkElement.GetAttribute("class").Should().NotContain("pro-typography-");
    }

    [Test]
    public void InlineLink_ShouldRenderWithParentTypography()
    {
        var comp = Context.Render<ProText>(parameters => parameters
            .Add(p => p.Typo, Typo.caption)
            .AddChildContent(childBuilder =>
            {
                childBuilder.AddContent(0, "ProtonBlazor is ");
                childBuilder.OpenComponent<ProLink>(1);
                childBuilder.AddAttribute(2, nameof(ProLink.ChildContent), (RenderFragment)(linkBuilder => linkBuilder.AddContent(0, "Awesome")));
                childBuilder.CloseComponent();
            }));

        var textElement = comp.Find("span.pro-typography");
        textElement.ClassList.Should().Contain("pro-typography-caption");

        var linkElement = textElement.QuerySelector("a.pro-link")!;
        linkElement.ClassList.Should().Contain("pro-typography");
        linkElement.GetAttribute("class").Should().NotContain("pro-typography-");
        linkElement.TextContent.Should().Be("Awesome");

        comp.MarkupMatches("""<span class="pro-typography pro-typography-caption">ProtonBlazor is <a class="pro-typography pro-link pro-primary-text pro-link-underline-hover">Awesome</a></span>""");
    }

    [Test]
    public void DisabledProperty_DisplaysAsDisabled()
    {
        var comp = Context.Render<ProLink>(parameters => parameters
            .Add(x => x.Href, "#")
            .Add(x => x.Disabled, true));

        var linkElement = comp.Find("a");
        linkElement.GetAttribute("href").Should().BeNullOrEmpty();
        linkElement.GetAttribute("aria-disabled").Should().Be("true");
        linkElement.ClassList.Should().Contain("pro-link-disabled");
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task ShouldExecute_OnClick(bool disabled)
    {
        var calls = 0;
        var comp = Context.Render<ProLink>(builder => builder
            .Add(p => p.OnClick, e => calls++)
            .Add(p => p.Disabled, disabled)
        );

        await comp.Find("a").ClickAsync(new MouseEventArgs());

        if (disabled)
        {
            calls.Should().Be(0);
        }
        else
        {
            calls.Should().Be(1);
        }
    }

    [Test]
    public void Icons_RenderInMarkup()
    {
        var comp = Context.Render<ProLink>(parameters => parameters
            .Add(p => p.StartIcon, Icons.Material.Filled.Home)
            .Add(p => p.EndIcon, Icons.Material.Filled.OpenInNew)
            .AddChildContent("Link Text"));

        comp.Find(".pro-link-icon-start").Should().NotBeNull();
        comp.Find(".pro-link-icon-end").Should().NotBeNull();
        comp.Markup.Should().Contain("Link Text");
    }

    [Test]
    public void Icons_CustomClass()
    {
        var comp = Context.Render<ProLink>(parameters => parameters
            .Add(p => p.StartIcon, Icons.Material.Filled.Home)
            .Add(p => p.IconClass, "custom-icon-class"));

        var icon = comp.FindComponent<ProIcon>();
        icon.Instance.Class.Should().Contain("custom-icon-class");
        icon.Instance.Class.Should().Contain("pro-link-icon-start");
    }

    [Test]
    public async Task OnClickErrorContentCaughtException()
    {
        var comp = Context.Render<LinkErrorContenCaughtException>();
        IElement AlertText() => ProAlert().Find("div.pro-alert-message");
        IRenderedComponent<ProAlert> ProAlert() => comp.FindComponent<ProAlert>();
        IReadOnlyList<IElement> Links() => comp.FindAll("a.pro-link");
        IElement ProLink() => Links()[0];

        await ProLink().ClickAsync(new MouseEventArgs());

        AlertText().InnerHtml.Should().Be("Oh my! We caught an error and handled it!");
    }

    [TestCase(Color.Primary, "pro-primary-text")]
    [TestCase(Color.Secondary, "pro-secondary-text")]
    [TestCase(Color.Tertiary, "pro-tertiary-text")]
    public void ColorProperty_AppliesCorrectClass(Color color, string expectedClass)
    {
        var comp = Context.Render<ProLink>(builder => builder
            .Add(p => p.Color, color)
        );

        var linkElement = comp.Find("a");
        linkElement.ClassList.Should().Contain(expectedClass);
    }

    [TestCase(Typo.h1, "pro-typography-h1")]
    [TestCase(Typo.subtitle1, "pro-typography-subtitle1")]
    [TestCase(Typo.caption, "pro-typography-caption")]
    public void TypoProperty_AppliesCorrectClass(Typo typo, string expectedClass)
    {
        var comp = Context.Render<ProLink>(builder => builder
            .Add(p => p.Typo, typo)
        );

        var linkElement = comp.Find("a");
        linkElement.ClassList.Should().Contain(expectedClass);
    }

    [TestCase(Underline.None, "pro-link-underline-none")]
    [TestCase(Underline.Hover, "pro-link-underline-hover")]
    [TestCase(Underline.Always, "pro-link-underline-always")]
    public void UnderlineProperty_AppliesCorrectClass(Underline underline, string expectedClass)
    {
        var comp = Context.Render<ProLink>(builder => builder
            .Add(p => p.Underline, underline)
        );

        var linkElement = comp.Find("a");
        linkElement.ClassList.Should().Contain(expectedClass);
    }

    [TestCase("_blank")]
    [TestCase("_self")]
    [TestCase("_parent")]
    [TestCase("_top")]
    public void TargetProperty_AppliesCorrectAttribute(string target)
    {
        var comp = Context.Render<ProLink>(builder => builder
            .Add(p => p.Href, "#")
            .Add(p => p.Target, target)
        );

        var linkElement = comp.Find("a");
        linkElement.GetAttribute("target").Should().Be(target);
    }

    [Test]
    public void ChildContent_IsRenderedCorrectly()
    {
        var comp = Context.Render<ProLink>(builder => builder
            .AddChildContent("<span>Test content</span>")
        );

        var linkElement = comp.Find("a");
        linkElement.InnerHtml.Should().Be("<span>Test content</span>");
    }

    [Test]
    public void UserAttributes_OverrideDefaultAttributes()
    {
        var comp = Context.Render<ProLink>(builder => builder
            .Add(p => p.Href, "#")
            .Add(p => p.Target, "_self")
            .Add(p => p.UserAttributes, new()
            {
                { "target", "custom-target" },
                { "role", "custom-role" },
                { "custom-attribute", "custom-value" }
            })
        );

        var linkElement = comp.Find("a");

        // Verify that user-defined attributes override default attributes, even ones set by properties.
        linkElement.GetAttribute("target").Should().Be("custom-target");
        linkElement.GetAttribute("role").Should().Be("custom-role");
        linkElement.GetAttribute("custom-attribute").Should().Be("custom-value");
    }
}
