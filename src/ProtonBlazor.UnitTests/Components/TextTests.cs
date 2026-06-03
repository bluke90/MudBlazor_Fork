using System.Linq;
using AwesomeAssertions;
using Bunit;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components;

[TestFixture]
public class TextTests : BunitTest
{
    [Test]
    public void Defaults_ShouldExposeExpectedParameterValues()
    {
        var comp = Context.Render<ProText>();

        comp.Instance.Typo.Should().Be(Typo.body1);
        comp.Instance.Align.Should().Be(Align.Inherit);
        comp.Instance.Color.Should().Be(Color.Inherit);
        comp.Instance.GutterBottom.Should().BeFalse();
        comp.Instance.Inline.Should().BeFalse();
        comp.Instance.HtmlTag.Should().BeNull();
    }

    [Test]
    public void Defaults_ShouldRenderBody1ParagraphWithoutOptionalClasses()
    {
        var comp = Context.Render<ProText>();

        comp.MarkupMatches("""<p class="pro-typography pro-typography-body1"></p>""");
    }

    [Test]
    public void UserAttributes_ShouldBeSplattedOnTheRootElement()
    {
        var comp = Context.Render<ProText>(parameters => parameters
            .AddUnmatched("data-test", "test-value")
            .AddUnmatched("aria-label", "example"));

        var element = comp.Find(".pro-typography");

        element.GetAttribute("data-test").Should().Be("test-value");
        element.GetAttribute("aria-label").Should().Be("example");
    }

    [TestCase(Align.Inherit, false, null)]
    [TestCase(Align.Left, false, "pro-typography-align-left")]
    [TestCase(Align.Center, false, "pro-typography-align-center")]
    [TestCase(Align.Right, false, "pro-typography-align-right")]
    [TestCase(Align.Justify, false, "pro-typography-align-justify")]
    [TestCase(Align.Start, false, "pro-typography-align-left")]
    [TestCase(Align.End, false, "pro-typography-align-right")]
    [TestCase(Align.Start, true, "pro-typography-align-right")]
    [TestCase(Align.End, true, "pro-typography-align-left")]
    public void Align_ShouldRenderTheExpectedAlignmentClass(Align align, bool rightToLeft, string expectedClass)
    {
        var comp = Context.Render<ProText>(parameters => parameters
            .Add(p => p.Align, align)
            .Add(p => p.RightToLeft, rightToLeft));

        var element = comp.Find(".pro-typography");
        var alignmentClasses = element
            .ClassList
            .Where(x => x.StartsWith("pro-typography-align-"))
            .ToArray();

        if (expectedClass is null)
        {
            alignmentClasses.Should().BeEmpty();
        }
        else
        {
            alignmentClasses.Should().Contain(expectedClass);
            alignmentClasses.Should().HaveCount(1);
        }
    }

    [TestCase(Color.Inherit, null)]
    [TestCase(Color.Default, null)]
    [TestCase(Color.Primary, "pro-primary-text")]
    [TestCase(Color.Secondary, "pro-secondary-text")]
    [TestCase(Color.Tertiary, "pro-tertiary-text")]
    [TestCase(Color.Error, "pro-error-text")]
    public void Color_ShouldRenderTheExpectedTextColorClass(Color color, string expectedClass)
    {
        var comp = Context.Render<ProText>(parameters => parameters
            .Add(p => p.Color, color));

        var element = comp.Find(".pro-typography");
        var colorClasses = element
            .ClassList
            .Where(x => x.EndsWith("-text"))
            .ToArray();

        if (expectedClass is null)
        {
            colorClasses.Should().BeEmpty();
        }
        else
        {
            colorClasses.Should().Contain(expectedClass);
            colorClasses.Should().HaveCount(1);
        }
    }

    [TestCase(true)]
    [TestCase(false)]
    public void GutterBottom_ShouldRenderTheMarginClassOnlyWhenEnabled(bool gutterBottom)
    {
        var comp = Context.Render<ProText>(parameters => parameters
            .Add(p => p.GutterBottom, gutterBottom));

        var element = comp.Find(".pro-typography");

        if (gutterBottom)
        {
            element.ClassList.Should().Contain("pro-typography-gutterbottom");
        }
        else
        {
            element.ClassList.Should().NotContain("pro-typography-gutterbottom");
        }
    }

    [Test]
    public void ChildContent_ShouldRenderInsideTheTypographyElement()
    {
        var comp = Context.Render<ProText>(parameters => parameters
            .AddChildContent("Hello, World!"));

        comp.MarkupMatches("""<p class="pro-typography pro-typography-body1">Hello, World!</p>""");
    }

    [TestCase(Typo.inherit, "span", "pro-typography-inherit")]
    [TestCase(Typo.h1, "h1", "pro-typography-h1")]
    [TestCase(Typo.h2, "h2", "pro-typography-h2")]
    [TestCase(Typo.h3, "h3", "pro-typography-h3")]
    [TestCase(Typo.h4, "h4", "pro-typography-h4")]
    [TestCase(Typo.h5, "h5", "pro-typography-h5")]
    [TestCase(Typo.h6, "h6", "pro-typography-h6")]
    [TestCase(Typo.subtitle1, "p", "pro-typography-subtitle1")]
    [TestCase(Typo.subtitle2, "p", "pro-typography-subtitle2")]
    [TestCase(Typo.body1, "p", "pro-typography-body1")]
    [TestCase(Typo.body2, "p", "pro-typography-body2")]
    [TestCase(Typo.button, "span", "pro-typography-button")]
    [TestCase(Typo.caption, "span", "pro-typography-caption")]
    [TestCase(Typo.overline, "span", "pro-typography-overline")]
    public void Typo_ShouldRenderTheExpectedTagAndTypographyClass(Typo typo, string expectedTag, string expectedClass)
    {
        var comp = Context.Render<ProText>(parameters => parameters
            .Add(p => p.Typo, typo)
            .AddChildContent("content"));

        var element = comp.Find(".pro-typography");
        var typographyClasses = element
            .ClassList
            .Where(x => x.StartsWith("pro-typography-"))
            .ToArray();

        element.TagName.Should().Be(expectedTag.ToUpperInvariant());
        typographyClasses.Should().Contain(expectedClass);
        typographyClasses.Should().HaveCount(1);
        element.TextContent.Should().Be("content");
    }

    [TestCase(Typo.h1, null, "h1", "pro-typography-h1")]
    [TestCase(Typo.body1, "", "p", "pro-typography-body1")]
    [TestCase(Typo.caption, "p", "p", "pro-typography-caption")]
    [TestCase(Typo.h4, "span", "span", "pro-typography-h4")]
    public void HtmlTag_ShouldOverrideOrFallbackToTheTypoSelectedTag(Typo typo, string htmlTag, string expectedTag, string expectedClass)
    {
        var comp = Context.Render<ProText>(parameters => parameters
            .Add(p => p.Typo, typo)
            .Add(p => p.HtmlTag, htmlTag)
            .AddChildContent("content"));

        var element = comp.Find(".pro-typography");
        var typographyClasses = element
            .ClassList
            .Where(x => x.StartsWith("pro-typography-"))
            .ToArray();

        element.TagName.Should().Be(expectedTag.ToUpperInvariant());
        typographyClasses.Should().Contain(expectedClass);
        typographyClasses.Should().HaveCount(1);
        element.TextContent.Should().Be("content");
    }

    [TestCase(true)]
    [TestCase(false)]
    public void Inline_ShouldRenderTheDisplayClassOnlyWhenEnabled(bool inline)
    {
        var comp = Context.Render<ProText>(parameters => parameters
            .Add(p => p.Inline, inline));

        var element = comp.Find(".pro-typography");

        if (inline)
        {
            element.ClassList.Should().Contain("d-inline");
        }
        else
        {
            element.ClassList.Should().NotContain("d-inline");
        }
    }
}
