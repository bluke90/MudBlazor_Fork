using AwesomeAssertions;
using Bunit;
using ProtonBlazor.UnitTests.TestComponents.ButtonGroup;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class ButtonGroupTests : BunitTest
    {
        [Test]
        public void WithFullWidthAndNoneButtonIsStreched_ThenAllButtonsStreched()
        {
            // Arrange

            var comp = Context.Render<ButtonGroupWithThreeButtons>(
                parameters => parameters
                    .Add(c => c.ButtonGroupFullWidth, true)
                    .Add(c => c.Button1FullWidth, false)
                    .Add(c => c.Button2FullWidth, false)
                    .Add(c => c.Button3FullWidth, false)
            );

            // Assert

            comp.FindAll(".pro-button-group-root.pro-width-full").Count.Should().Be(1);
            comp.FindAll(".pro-button-root.pro-width-full").Count.Should().Be(3);
        }

        [Test]
        public void WithFullWidthAndOneButtonIsStreched_ThenOtherButtonsNotStreched()
        {
            // Arrange

            var comp = Context.Render<ButtonGroupWithThreeButtons>(
                parameters => parameters
                    .Add(c => c.ButtonGroupFullWidth, true)
                    .Add(c => c.Button1FullWidth, true)
                    .Add(c => c.Button2FullWidth, false)
                    .Add(c => c.Button3FullWidth, false)
            );

            // Assert

            comp.FindAll(".pro-button-group-root.pro-width-full").Count.Should().Be(1);
            var buttonComps = comp.FindAll(".pro-button-root");
            buttonComps[0].ClassList.Should().Contain("pro-width-full");
            buttonComps[1].ClassList.Should().NotContain("pro-width-full");
            buttonComps[2].ClassList.Should().NotContain("pro-width-full");
        }

        [Test]
        public async Task WithFullWidth_WhenButtonWithFullWidthIsRemoved_ThenOtherButtonsAreStreched()
        {
            // Arrange

            var comp = Context.Render<ButtonGroupWithThreeButtons>(
                parameters => parameters
                    .Add(c => c.ButtonGroupFullWidth, true)
                    .Add(c => c.Button1FullWidth, true)
                    .Add(c => c.Button2FullWidth, false)
                    .Add(c => c.Button3FullWidth, false)
            );

            // Act

            await comp.SetParametersAndRenderAsync(parameters => parameters.Add(c => c.Button1Displayed, false));

            // Assert

            comp.FindAll(".pro-button-group-root.pro-width-full").Count.Should().Be(1);
            var buttonComps = comp.FindAll(".pro-button-root");
            buttonComps.Count.Should().Be(2);
            buttonComps[0].ClassList.Should().Contain("pro-width-full");
            buttonComps[1].ClassList.Should().Contain("pro-width-full");
        }
    }
}
