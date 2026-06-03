// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AngleSharp.Css.Dom;
using AngleSharp.Html.Dom;
using AwesomeAssertions;
using Bunit;
using NUnit.Framework;
using ProtonBlazor.UnitTests.TestComponents.Timeline;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class TimelineTests : BunitTest
    {
        [Test]
        public void Timeline_DefaultValues()
        {
            var comp = Context.Render<ProTimeline>();

            comp.Instance.TimelineOrientation.Should().Be(TimelineOrientation.Vertical);
            comp.Instance.TimelinePosition.Should().Be(TimelinePosition.Alternate);
            comp.Instance.TimelineAlign.Should().Be(TimelineAlign.Default);
            comp.Instance.Reverse.Should().Be(false);
            comp.Instance.Modifiers.Should().Be(true);

        }

        /// <summary>
        /// Default Timeline, with five items.
        /// Testing if selection is sync with move commands.
        /// </summary>
        [Test]
        public async Task Timeline()
        {
            var comp = Context.Render<TimelineTest>();
            // print the generated html
            //// select elements needed for the test
            var timeline = comp.FindComponent<ProTimeline>().Instance;
            //// validating some renders
            timeline.Should().NotBeNull();
            await comp.WaitForAssertionAsync(() => comp.FindAll("div.pro-timeline").Count.Should().Be(1));
            comp.FindAll("div.pro-timeline-item").Count.Should().Be(5);
            var items = comp.FindComponents<ProTimelineItem>();
            items.Count.Should().Be(5);
            //// changing current index
            for (var i = 1; i <= 4; i++)
            {
                await comp.InvokeAsync(() => timeline.MoveTo(i));
                timeline.SelectedIndex.Should().Be(i);
                timeline.SelectedContainer.Should().Be(items[i].Instance);
            }
            await comp.InvokeAsync(() => timeline.MoveTo(0));
            timeline.SelectedIndex.Should().Be(0);
            timeline.SelectedContainer.Should().Be(items[0].Instance);
        }

        [Test]

        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.Alternate, false, new[] { "pro-timeline-horizontal", "pro-timeline-position-alternate" })]
        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.Start, false, new[] { "pro-timeline-horizontal", "pro-timeline-position-alternate" })]
        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.Left, false, new[] { "pro-timeline-horizontal", "pro-timeline-position-alternate" })]
        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.Right, false, new[] { "pro-timeline-horizontal", "pro-timeline-position-alternate" })]
        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.End, false, new[] { "pro-timeline-horizontal", "pro-timeline-position-alternate" })]

        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.Top, false, new[] { "pro-timeline-horizontal", "pro-timeline-position-top" })]
        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.Bottom, false, new[] { "pro-timeline-horizontal", "pro-timeline-position-bottom" })]

        [TestCase(TimelineOrientation.Vertical, TimelinePosition.Alternate, false, new[] { "pro-timeline-vertical", "pro-timeline-position-alternate" })]
        [TestCase(TimelineOrientation.Vertical, TimelinePosition.Top, false, new[] { "pro-timeline-vertical", "pro-timeline-position-alternate" })]
        [TestCase(TimelineOrientation.Vertical, TimelinePosition.Bottom, false, new[] { "pro-timeline-vertical", "pro-timeline-position-alternate" })]

        [TestCase(TimelineOrientation.Vertical, TimelinePosition.Start, false, new[] { "pro-timeline-vertical", "pro-timeline-position-start" })]
        [TestCase(TimelineOrientation.Vertical, TimelinePosition.End, false, new[] { "pro-timeline-vertical", "pro-timeline-position-end" })]

        [TestCase(TimelineOrientation.Vertical, TimelinePosition.Left, false, new[] { "pro-timeline-vertical", "pro-timeline-position-start" })]
        [TestCase(TimelineOrientation.Vertical, TimelinePosition.Right, false, new[] { "pro-timeline-vertical", "pro-timeline-position-end" })]

        //RTL to true

        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.Alternate, true, new[] { "pro-timeline-horizontal", "pro-timeline-position-alternate" })]
        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.Start, true, new[] { "pro-timeline-horizontal", "pro-timeline-position-alternate" })]
        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.Left, true, new[] { "pro-timeline-horizontal", "pro-timeline-position-alternate" })]
        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.Right, true, new[] { "pro-timeline-horizontal", "pro-timeline-position-alternate" })]
        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.End, true, new[] { "pro-timeline-horizontal", "pro-timeline-position-alternate" })]

        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.Top, true, new[] { "pro-timeline-horizontal", "pro-timeline-position-top" })]
        [TestCase(TimelineOrientation.Horizontal, TimelinePosition.Bottom, true, new[] { "pro-timeline-horizontal", "pro-timeline-position-bottom" })]

        [TestCase(TimelineOrientation.Vertical, TimelinePosition.Alternate, true, new[] { "pro-timeline-vertical", "pro-timeline-position-alternate" })]
        [TestCase(TimelineOrientation.Vertical, TimelinePosition.Top, true, new[] { "pro-timeline-vertical", "pro-timeline-position-alternate" })]
        [TestCase(TimelineOrientation.Vertical, TimelinePosition.Bottom, true, new[] { "pro-timeline-vertical", "pro-timeline-position-alternate" })]

        [TestCase(TimelineOrientation.Vertical, TimelinePosition.Start, true, new[] { "pro-timeline-vertical", "pro-timeline-position-start" })]
        [TestCase(TimelineOrientation.Vertical, TimelinePosition.End, true, new[] { "pro-timeline-vertical", "pro-timeline-position-end" })]

        [TestCase(TimelineOrientation.Vertical, TimelinePosition.Left, true, new[] { "pro-timeline-vertical", "pro-timeline-position-end" })]
        [TestCase(TimelineOrientation.Vertical, TimelinePosition.Right, true, new[] { "pro-timeline-vertical", "pro-timeline-position-start" })]

        public async Task TimelineTest_Position(TimelineOrientation orientation, TimelinePosition position, bool rtl, string[] expectedClass)
        {
            var comp = Context.Render<TimelineTest>(p => p.AddCascadingValue("RightToLeft", rtl));

            var timeline = comp.FindComponent<ProTimeline>();

            await timeline.SetParametersAndRenderAsync(p =>
            {
                p.Add(x => x.TimelineOrientation, orientation);
                p.Add(x => x.TimelinePosition, position);
            });

            timeline.Nodes.Should().ContainSingle();
            timeline.Nodes[0].Should().BeAssignableTo<IHtmlDivElement>();

            (timeline.Nodes[0] as IHtmlDivElement).ClassList.Should().Contain(expectedClass);
        }

        [Test]
        public async Task Timeline_SelectItem()
        {
            var comp = Context.Render<TimelineTest>();

            var itemsDiv = comp.FindAll(".pro-timeline-item");

            itemsDiv.Should().HaveCount(5);

            for (var i = 0; i < 5; i++)
            {
                await itemsDiv[i].ClickAsync();

                comp.Instance.SelectedIndex.Should().Be(i);
            }
        }

        [Test]
        public async Task Timeline_DotClass()
        {
            var comp = Context.Render<TimelineTest>();
            var firstItem = comp.FindComponent<ProTimelineItem>();

            comp.Find("div.pro-timeline-item-dot-inner").ClassList.Should().NotContain("timeline-red-dot");

            await firstItem.SetParametersAndRenderAsync(p =>
            {
                p.Add(t => t.DotClass, "timeline-red-dot");
            });

            comp.Find("div.pro-timeline-item-dot-inner").ClassList.Should().Contain("timeline-red-dot");
        }

        [Test]
        public async Task Timeline_DotStyle()
        {
            var comp = Context.Render<TimelineTest>();
            var firstItem = comp.FindComponent<ProTimelineItem>();
            comp.Find("div.pro-timeline-item-dot-inner").GetStyle()["background-color"].Should().Be("");

#pragma warning disable CS0618 // Type or member is obsolete
            await firstItem.SetParametersAndRenderAsync(p =>
            {
                p.Add(t => t.DotStyle, "background-color: #ff0000");
            });
#pragma warning restore CS0618 // Type or member is obsolete

            comp.Find("div.pro-timeline-item-dot-inner").GetStyle()["background-color"].Should().Be("rgba(255, 0, 0, 1)");
        }

        /// <summary>
        /// Test horizontal timeline inside vertical timeline.
        /// </summary>
        [Test]
        public async Task HorizontalTimelineInsideVerticalTimeline()
        {
            var comp = Context.Render<HorizontalTimelineInsideVerticalTimelineTest>();
            // select elements needed for the test
            var timeline = comp.FindComponent<ProTimeline>().Instance;
            // validating some renders
            timeline.Should().NotBeNull();
            await comp.WaitForAssertionAsync(() => comp.FindAll("div.pro-timeline").Count.Should().Be(2));
            comp.FindAll("div.pro-timeline-item").Count.Should().Be(9);
            var items = comp.FindComponents<ProTimelineItem>();
            items.Count.Should().Be(9);
        }
    }
}
