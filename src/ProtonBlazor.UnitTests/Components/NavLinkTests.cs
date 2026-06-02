using AngleSharp.Dom;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Web;
using NUnit.Framework;
using ProtonBlazor.UnitTests.TestComponents.NavLink;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class NavLinkTests : BunitTest
    {
        /// <summary>
        /// When Target is not empty, rel attribute should be equals to "noopener noreferrer" on the a element
        /// </summary>
        [TestCase(null, "")]
        [TestCase("", "")]
        [TestCase("_self", "noopener noreferrer")]
        [TestCase("_blank", "noopener noreferrer")]
        [TestCase("_parent", "noopener noreferrer")]
        [TestCase("_top", "noopener noreferrer")]
        [TestCase("myFrameName", "noopener noreferrer")]
        public void NavLink_CheckRelAttribute(string target, string expectedRel)
        {
            var comp = Context.Render<ProNavLink>(parameters => parameters.Add(x => x.Target, target));
            // print the generated html
            // select elements needed for the test
            comp.Find("a").GetAttribute("rel").Should().Be(expectedRel);
        }

        [Test]
        public async Task NavLink_CheckOnClickEvent()
        {
            var clicked = false;
            var comp = Context.Render<ProNavLink>(parameters => parameters.Add(x => x.OnClick, (MouseEventArgs args) => { clicked = true; }));
            // print the generated html
            comp.FindAll("a").Should().BeEmpty();
            await comp.Find(".pro-nav-link").ClickAsync();
            clicked.Should().BeTrue();
        }

        [Test]
        public async Task NavLink_Active()
        {
            const string activeClass = "Custom__nav_active_css";
            var comp = Context.Render<ProNavLink>(parameters => parameters.Add(x => x.ActiveClass, activeClass));
            await comp.Find(".pro-nav-link").ClickAsync();
            comp.Markup.Should().Contain(activeClass);
        }

        [Test]
        public async Task NavLink_Enabled_CheckNavigation()
        {
            var comp = Context.Render<NavLinkDisabledTest>(parameters => parameters.Add(x => x.Disabled, false));
            await comp.Find("a").ClickAsync();
            comp.Instance.IsNavigated.Should().BeTrue();
        }

        [Test]
        public async Task NavLink_Disabled_CheckNoNavigation()
        {
            var comp = Context.Render<NavLinkDisabledTest>(parameters => parameters.Add(x => x.Disabled, true));
            await comp.Find("a").ClickAsync();
            comp.Instance.IsNavigated.Should().BeFalse();
        }

        [Test]
        public async Task NavLinkOnClickErrorContentCaughtException()
        {
            var comp = Context.Render<NavLinkErrorContenCaughtException>();
            IElement AlertText() => ProAlert().Find("div.pro-alert-message");
            IRenderedComponent<ProAlert> ProAlert() => comp.FindComponent<ProAlert>();
            IReadOnlyList<IElement> Links() => comp.FindAll(".pro-nav-link");
            IElement ProLink() => Links()[0];

            await ProLink().ClickAsync(new MouseEventArgs());

            AlertText().InnerHtml.Should().Be("Oh my! We caught an error and handled it!");
        }
    }
}
