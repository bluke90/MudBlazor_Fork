using AwesomeAssertions;
using Bunit;
using ProtonBlazor.UnitTests.TestComponents.NavMenu;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class NavGroupTests : BunitTest
    {
        /// <summary>
        /// Checking the disable group button disables the group and it's children
        /// Adding the pro-nav-group-disabled css tag to the group
        /// </summary>
        [Test]
        public async Task Two_Way_Bindable_Disabled()
        {
            var comp = Context.Render<NavMenuGroupDisabledTest>();

            comp.Markup.Should().NotContain("pro-nav-group-disabled");
            comp.Markup.Should().NotContain("pro-expanded");

            await comp.Find("input").ChangeAsync(true);

            comp.Markup.Should().Contain("pro-nav-group-disabled");
        }

        /// <summary>
        /// NavGroup should generate a nav tag with an aria-label.
        /// </summary>
        [Test]
        public void NavGroup_Should_UseNavTag()
        {
            var expectedTitle = "navgroup-title";
            var comp = Context.Render<ProNavGroup>(parameters =>
                    parameters.Add(p => p.Title, expectedTitle));

            comp.FindAll("nav").Should().Contain(navNode => navNode.GetAttribute("aria-label") == expectedTitle);
        }

        /// <summary>
        /// NavGroup should expand and collapse via Expanded binding.
        /// </summary>
        [Test]
        public async Task NavGroup_Should_Expand_Via_Expanded_Binding()
        {
            var comp = Context.Render<NavGroupWithExpandedBindingTest>();
            GetExpandedState().Should().BeFalse();

            await comp.Find("#navgroup-switch").ChangeAsync(true);

            GetExpandedState().Should().BeTrue();

            await comp.Find("#navgroup-switch").ChangeAsync(false);

            GetExpandedState().Should().BeFalse();
            return;

            bool GetExpandedState() => comp.FindComponent<ProCollapse>().Instance.Expanded;
        }
    }
}

