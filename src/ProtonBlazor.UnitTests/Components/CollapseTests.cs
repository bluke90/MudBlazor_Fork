// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AngleSharp.Dom;
using AwesomeAssertions;
using Bunit;
using ProtonBlazor.UnitTests.TestComponents.Collapse;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class CollapseTests : BunitTest
    {
        [Test]
        public async Task Collapse_TwoWayBinding_Test1()
        {
            var comp = Context.Render<CollapseBindingTest>();
            var collapse = comp.FindComponent<ProCollapse>();

            collapse.Markup.Should().Contain("pro-collapse-entered");

            IElement Button() => comp.Find("#outside_btn");

            IRenderedComponent<ProSwitch<bool>> ProSwitch() => comp.FindComponent<ProSwitch<bool>>();
            // Initial state is expanded
            ProSwitch().Find("input").HasAttribute("checked").Should().BeTrue();

            // Collapse via button
            await Button().ClickAsync();
            ProSwitch().Find("input").HasAttribute("checked").Should().BeFalse();

            // Expand via button
            await Button().ClickAsync();
            ProSwitch().Find("input").HasAttribute("checked").Should().BeTrue();

            // Collapse via switch
            await ProSwitch().Find("input").ChangeAsync(false);
            ProSwitch().Find("input").HasAttribute("checked").Should().BeFalse();

            // Expand via switch
            await ProSwitch().Find("input").ChangeAsync(true);
            ProSwitch().Find("input").HasAttribute("checked").Should().BeTrue();
        }

        [Test]
        public async Task Collapse_OnAnimationEnd_ShouldIgnoreChildTransitionEnd()
        {
            var comp = Context.Render<CollapseAnimationEndChildTransitionTest>();
            comp.Find("#animation_end_count").TextContent.Should().Be("0");

            await comp.Find("#inner").TriggerEventAsync("ontransitionend", EventArgs.Empty);

            comp.Find("#animation_end_count").TextContent.Should().Be("0");
        }
    }
}
