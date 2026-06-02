using AwesomeAssertions;
using Bunit;
using ProtonBlazor.UnitTests.TestComponents.NavMenu;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class NavMenuTests : BunitTest
    {
        /// <summary>
        /// Change all styling parameters so that all default values have the correct classes.
        /// </summary>
        [Test]
        public void NavMenuTests_DefaultValues()
        {
            var comp = Context.Render<ProNavMenu>();

            comp.Instance.Bordered.Should().Be(false);
            comp.Instance.Color.Should().Be(Color.Default);
            comp.Instance.Dense.Should().Be(false);
            comp.Instance.Margin.Should().Be(Margin.None);
            comp.Instance.Rounded.Should().Be(false);

            comp.FindAll("pro-navmenu-bordered").Count.Should().Be(0);
            comp.FindAll("pro-navmenu-success").Count.Should().Be(0);
            comp.FindAll("pro-navmenu-dense").Count.Should().Be(0);
            comp.FindAll("pro-navmenu-margin-dense").Count.Should().Be(0);
            comp.FindAll("pro-navmenu-rounded").Count.Should().Be(0);
        }

        /// <summary>
        /// Change all styling parameters from its default values and check that the correct classes are added.
        /// </summary>
        [Test]
        public void NavMenuTests_CheckAllStyling()
        {
            var comp = Context.Render<ProNavMenu>(x =>
            {
                x.Add(p => p.Bordered, true);
                x.Add(p => p.Color, Color.Success);
                x.Add(p => p.Dense, true);
                x.Add(p => p.Margin, Margin.Dense);
                x.Add(p => p.Rounded, true);
            });

            comp.Markup.Should().Contain("pro-navmenu-bordered");
            comp.Markup.Should().Contain("pro-navmenu-success");
            comp.Markup.Should().Contain("pro-navmenu-dense");
            comp.Markup.Should().Contain("pro-navmenu-margin-dense");
            comp.Markup.Should().Contain("pro-navmenu-rounded");
        }

        /// <summary>
        /// This component is initially Expanded with the property Expand set to immutable true <c>Expand=true</c>
        /// And even so, he changes when clicked
        /// </summary>
        [Test]
        public async Task One_Way_Bindable()
        {
            var comp = Context.Render<NavMenuOneWay>();
            comp.Markup.Should().Contain("pro-expanded");
            comp.Markup.Should().Contain("aria-hidden=\"false\"");

            var navgroup = comp.Find(".pro-nav-group>button");
            await navgroup.ClickAsync();

            comp.Markup.Should().NotContain("pro-expanded");
            comp.Markup.Should().Contain("aria-hidden=\"true\"");
        }

        /// <summary>
        /// This component has a field _expanded two-way bound to Expanded property
        /// Initially is set to false and after clicking the navgroup should change to true
        /// </summary>
        [Test]
        public async Task Two_Way_Bindable()
        {
            var comp = Context.Render<NavMenuTwoWay>();
            comp.Markup.Should().NotContain("pro-expanded");
            comp.Markup.Should().Contain("aria-hidden=\"true\"");
            var expanded = comp.Instance.Expanded;
            expanded.Should().BeFalse();

            var navgroup = comp.Find(".pro-nav-group>button");
            await navgroup.ClickAsync();

            expanded = comp.Instance.Expanded;
            expanded.Should().BeTrue();
            comp.Markup.Should().Contain("pro-expanded");
            comp.Markup.Should().Contain("aria-hidden=\"false\"");
        }
    }
}
