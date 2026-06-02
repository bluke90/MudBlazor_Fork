using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AwesomeAssertions;
using Bunit;
using Bunit.Rendering;
using Microsoft.AspNetCore.Components.Web;
using ProtonBlazor.Docs.Examples;
using ProtonBlazor.UnitTests.TestComponents.Button;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class ButtonsTests : BunitTest
    {
        /// <summary>
        /// ProButton without specifying HtmlTag, renders a button
        /// </summary>
        [Test]
        public void ProButtonShouldRenderAButtonByDefault()
        {
            var comp = Context.Render<ProButton>();
            //no HtmlTag nor Link properties are set, so HtmlTag is button by default
            comp.Instance
                .HtmlTag
                .Should()
                .Be("button");
            //it is a button, and has by default stopPropagation on onclick
            comp.Markup
                .Replace(" ", string.Empty)
                .Should()
                .StartWith("<button")
                .And
                .Contain("stopPropagation");
        }

        /// <summary>
        /// ProButton renders an anchor element when Link is set
        /// </summary>
        [Test]
        public void ProButtonShouldRenderAnAnchorIfLinkIsSetAndIsNotDisabled()
        {
            var comp = Context.Render<ProButton>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Target, "_blank"));
            //Link property is set, so it has to render an anchor element
            comp.Instance
                .HtmlTag
                .Should()
                .Be("a");
            //Target property is set, so it must have the rel attribute set to noopener
            comp.Markup
                .Should()
                .Contain("rel=\"noopener\"");
            //it is an anchor and not contains stopPropagation 
            comp.Markup
                .Replace(" ", string.Empty)
                .Should()
                .StartWith("<a")
                .And
                .NotContain("__internal_stopPropagation_onclick");

            comp = Context.Render<ProButton>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Target, "_blank")
                .Add(p => p.Disabled, true));
            comp.Instance.HtmlTag.Should().Be("button");

        }

        /// <summary>
        /// ProButton should render with value of Rel property
        /// </summary>
        [Test]
        public void ProButtonShouldRenderRelIfSet()
        {
            var comp = Context.Render<ProButton>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Rel, "nofollow"));
            comp
                .Find("a")
                .GetAttribute("rel")
                .Should()
                .Be("nofollow");
        }

        /// <summary>
        /// ProButton should have rel="nofollow" if Rel is set to "nofollow", even if Target is _blank
        /// </summary>
        [Test]
        public void ProButtonShouldHaveNoopenerOverridenByRel()
        {
            var comp = Context.Render<ProButton>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Target, "_blank")
                .Add(p => p.Rel, "nofollow"));
            comp
                .Find("a")
                .GetAttribute("rel")
                .Should()
                .Be("nofollow");
        }

        /// <summary>
        /// ProButton should have rel="" Rel is explicitly set to empty, even if Target is _blank
        /// </summary>
        [Test]
        public void ProButtonShouldHaveHaveNoRelWhenSetToEmpty()
        {
            var comp = Context.Render<ProButton>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Rel, "")
                .Add(p => p.Target, "_blank"));
            comp
                .Find("a")
                .GetAttribute("rel")
                .Should()
                .Be("");
        }

        /// <summary>
        /// ProButton should not render rel if it's null and target is not _blank
        /// </summary>
        [Test]
        public void ProButtonShouldNotRenderRelIfNullAndTargetNotBlank()
        {
            var comp = Context.Render<ProButton>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Rel, null)
                .Add(p => p.Target, "_notblank"));
            comp
                .Find("a")
                .HasAttribute("rel")
                .Should()
                .BeFalse();
        }

        /// <summary>
        /// ProButton whithout specifying HtmlTag, renders a button
        /// </summary>
        [Test]
        public void ProIconButtonShouldRenderAButtonByDefault()
        {
            var comp = Context.Render<ProIconButton>();
            //no HtmlTag nor Link properties are set, so HtmlTag is button by default
            comp.Instance
                .HtmlTag
                .Should()
                .Be("button");
            //it is a button
            comp.Markup
                .Replace(" ", string.Empty)
                .Should()
                .StartWith("<button");
        }

        /// <summary>
        /// ProButton renders an anchor element when Link is set
        /// </summary>
        [Test]
        public void ProIconButtonShouldRenderAnAnchorIfLinkIsSet()
        {
            using var ctx = new Bunit.BunitContext();
            var comp = ctx.Render<ProIconButton>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Target, "_blank"));
            //Link property is set, so it has to render an anchor element
            comp.Instance
                .HtmlTag
                .Should()
                .Be("a");
            //Target property is set, so it must have the rel attribute set to noopener
            comp.Markup
                .Should()
                .Contain("rel=\"noopener\"");
            //it is an anchor
            comp.Markup
                .Replace(" ", string.Empty)
                .Should()
                .StartWith("<a");
        }

        /// <summary>
        /// ProIconButton should render with value of Rel property
        /// </summary>
        [Test]
        public void ProIconButtonShouldRenderRelIfSet()
        {
            var comp = Context.Render<ProIconButton>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Rel, "nofollow"));
            comp
                .Find("a")
                .GetAttribute("rel")
                .Should()
                .Be("nofollow");
        }

        /// <summary>
        /// ProIconButton should have rel="nofollow" if Rel is set to "nofollow", even if Target is _blank
        /// </summary>
        [Test]
        public void ProIconButtonShouldHaveNoopenerOverridenByRel()
        {
            var comp = Context.Render<ProIconButton>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Target, "_blank")
                .Add(p => p.Rel, "nofollow"));
            comp
                .Find("a")
                .GetAttribute("rel")
                .Should()
                .Be("nofollow");
        }

        /// <summary>
        /// ProButton should have rel="" Rel is explicitly set to empty, even if Target is _blank
        /// </summary>
        [Test]
        public void ProIconButtonShouldHaveHaveNoRelWhenSetToEmpty()
        {
            var comp = Context.Render<ProIconButton>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Target, "_blank")
                .Add(p => p.Rel, ""));
            comp
                .Find("a")
                .GetAttribute("rel")
                .Should()
                .Be("");
        }

        /// <summary>
        /// ProIconButton should not render rel if it's null and target is not _blank
        /// </summary>
        [Test]
        public void ProIconButtonShouldNotRenderRelIfNullAndTargetNotBlank()
        {
            var comp = Context.Render<ProIconButton>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Rel, null)
                .Add(p => p.Target, "_notblank"));
            comp
                .Find("a")
                .HasAttribute("rel")
                .Should()
                .BeFalse();
        }

        /// <summary>
        /// ProButton whithout specifying HtmlTag, renders a button
        /// </summary>
        [Test]
        public void ProFabShouldRenderAButtonByDefault()
        {
            var comp = Context.Render<ProFab>();
            //no HtmlTag nor Link properties are set, so HtmlTag is button by default
            comp.Instance
                .HtmlTag
                .Should()
                .Be("button");
            //it is a button
            comp.Markup
                .Replace(" ", string.Empty)
                .Should()
                .StartWith("<button");
        }

        /// <summary>
        /// ProButton renders an anchor element when Link is set
        /// </summary>
        [Test]
        public void ProFabShouldRenderAnAnchorIfLinkIsSet()
        {
            var comp = Context.Render<ProFab>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Target, "_blank"));
            //Link property is set, so it has to render an anchor element
            comp.Instance
                .HtmlTag
                .Should()
                .Be("a");
            //Target property is set, so it must have the rel attribute set to noopener
            comp.Markup
                .Should()
                .Contain("rel=\"noopener\"");
            //it is an anchor
            comp.Markup
                .Replace(" ", string.Empty)
                .Should()
                .StartWith("<a");
        }

        /// <summary>
        /// ProFab should only render an icon if one is specified.
        /// </summary>
        [Test]
        public void ProFabShouldNotRenderIconIfNoneSpecified()
        {
            var comp = Context.Render<ProFab>();
            comp.Markup
                .Should()
                .NotContainAny("pro-icon-root");
        }

        /// <summary>
        /// ProFab should render with value of Rel property
        /// </summary>
        [Test]
        public void ProFabShouldRenderRelIfSet()
        {
            var comp = Context.Render<ProFab>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Rel, "nofollow"));
            comp
                .Find("a")
                .GetAttribute("rel")
                .Should()
                .Be("nofollow");
        }

        /// <summary>
        /// ProFab should have rel="nofollow" if Rel is set to "nofollow", even if Target is _blank
        /// </summary>
        [Test]
        public void ProFabShouldHaveNoopenerOverridenByRel()
        {
            var comp = Context.Render<ProFab>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Target, "_blank")
                .Add(p => p.Rel, "nofollow"));
            comp
                .Find("a")
                .GetAttribute("rel")
                .Should()
                .Be("nofollow");
        }

        /// <summary>
        /// ProFab should have rel="" Rel is explicitly set to empty, even if Target is _blank
        /// </summary>
        [Test]
        public void ProFabShouldHaveHaveNoRelWhenSetToEmpty()
        {
            var comp = Context.Render<ProFab>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Target, "_blank")
                .Add(p => p.Rel, ""));
            comp
                .Find("a")
                .GetAttribute("rel")
                .Should()
                .Be("");
        }

        /// <summary>
        /// ProFab should not render rel if it's null and target is not _blank
        /// </summary>
        [Test]
        public void ProFabShouldNotRenderRelIfNullAndTargetNotBlank()
        {
            var comp = Context.Render<ProFab>(parameters => parameters
                .Add(p => p.Href, "https://www.google.com")
                .Add(p => p.Rel, null)
                .Add(p => p.Target, "_notblank"));
            comp
                .Find("a")
                .HasAttribute("rel")
                .Should()
                .BeFalse();
        }

        [Test]
        public async Task ProToggleIcon()
        {
            var comp = Context.Render<ProToggleIconButton>();
            await comp.SetParametersAndRenderAsync(parameters => parameters.Add(x => x.Disabled, true));
            await comp.InvokeAsync(() => comp.Instance.SetToggledAsync(true));
            await comp.WaitForAssertionAsync(() => comp.Instance.Toggled.Should().BeFalse());
        }

        [Test]
        public void ProButtonSizes()
        {
            var comp = Context.Render<ButtonSizeIconSizeTest>();

            var buttons = comp.Nodes.Where(n => n.NodeName.Equals("BUTTON")).ToArray();
            buttons.Length.Should().Be(6);

            // Buttons 1-3: Explicit button sizes
            ((IHtmlButtonElement)buttons[0]).ClassList.Contains("pro-button-filled-size-small").Should().BeTrue();  // Size="Size.Small"
            ((IHtmlButtonElement)buttons[1]).ClassList.Contains("pro-button-filled-size-medium").Should().BeTrue(); // Size="Size.Medium"
            ((IHtmlButtonElement)buttons[2]).ClassList.Contains("pro-button-filled-size-large").Should().BeTrue();  // Size="Size.Large"
        }

        [Test]
        public void ProButtonIconSizes()
        {
            var comp = Context.Render<ButtonSizeIconSizeTest>();

            var buttons = comp.Nodes.Where(n => n.NodeName.Equals("BUTTON")).ToArray();

            // Button 4: Small button- with large icon size: Size="Size.Small", IconSize="Size.Large"
            ((IHtmlButtonElement)buttons[3]).ClassList.Contains("pro-button-filled-size-small").Should().BeTrue();
            var button4Span = ((IHtmlButtonElement)buttons[3]).Children[0].Children[0];
            button4Span.ClassName.Contains("pro-button-icon-size-large").Should().BeTrue();
            var button4Svg = button4Span.Children[0];
            button4Svg.ClassName.Contains("pro-icon-size-large").Should().BeTrue();

            // Button 5: Defaults: Medium button- and icon size.
            ((IHtmlButtonElement)buttons[4]).ClassList.Contains("pro-button-filled-size-medium").Should().BeTrue();
            var button5Span = ((IHtmlButtonElement)buttons[4]).Children[0].Children[0];
            button5Span.ClassName.Contains("pro-button-icon-size-medium").Should().BeTrue();
            var button5Svg = button5Span.Children[0];
            button5Svg.ClassName.Contains("pro-icon-size-medium").Should().BeTrue();

            // Button 6: Large button- with small icon size: Size="Size.Large", IconSize="Size.Small"
            ((IHtmlButtonElement)buttons[5]).ClassList.Contains("pro-button-filled-size-large").Should().BeTrue();
            var button6Span = ((IHtmlButtonElement)buttons[5]).Children[0].Children[0];
            button6Span.ClassName.Contains("pro-button-icon-size-small").Should().BeTrue();
            var button6Svg = button6Span.Children[0];
            button6Svg.ClassName.Contains("pro-icon-size-small").Should().BeTrue();
        }

        /// <summary>
        /// Ensures buttons inherit their disabled state
        /// </summary>
        [Test]
        public async Task ButtonsNestedDisabled()
        {
            var comp = Context.Render<ButtonsNestedDisabledTest>();

            comp.FindComponent<ProButton>().Find("button").HasAttribute("disabled").Should().BeFalse();
            comp.FindComponent<ProFab>().Find("button").HasAttribute("disabled").Should().BeFalse();
            comp.FindComponent<ProIconButton>().Find("button").HasAttribute("disabled").Should().BeFalse();

            await comp.SetParametersAndRenderAsync(parameters => parameters.Add(x => x.Disabled, true)); //buttons should be disabled when the cascading value is disabled

            comp.FindComponent<ProButton>().Find("button").HasAttribute("disabled").Should().BeTrue();
            comp.FindComponent<ProFab>().Find("button").HasAttribute("disabled").Should().BeTrue();
            comp.FindComponent<ProIconButton>().Find("button").HasAttribute("disabled").Should().BeTrue();
        }

        [Test]
        public async Task ButtonsOnClickErrorContentCaughtException()
        {
            var comp = Context.Render<ButtonErrorContenCaughtException>();
            var alertTextFunc = () => ProAlert().Find("div.pro-alert-message");
            IRenderedComponent<ProAlert> ProAlert() => comp.FindComponent<ProAlert>();
            IReadOnlyList<IElement> Buttons() => comp.FindAll("button.pro-button-root");
            IElement ProButton() => Buttons()[0];
            IElement ProFab() => Buttons()[1];
            IElement ProIconButton() => Buttons()[2];

            // ProButton
            await ProButton().ClickAsync(new MouseEventArgs());
            alertTextFunc().InnerHtml.Should().Be("Something went wrong...");
            await comp.InvokeAsync(comp.Instance.Recover);
            alertTextFunc.Should().Throw<ComponentNotFoundException>();

            // ProFab
            await ProFab().ClickAsync(new MouseEventArgs());
            alertTextFunc().InnerHtml.Should().Be("Something went wrong...");
            await comp.InvokeAsync(comp.Instance.Recover);
            alertTextFunc.Should().Throw<ComponentNotFoundException>();

            // ProIconButton
            await ProIconButton().ClickAsync(new MouseEventArgs());
            alertTextFunc().InnerHtml.Should().Be("Something went wrong...");
            await comp.InvokeAsync(comp.Instance.Recover);
            alertTextFunc.Should().Throw<ComponentNotFoundException>();
        }
    }
}
