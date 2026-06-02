using AngleSharp.Dom;
using AwesomeAssertions;
using Bunit;
using Microsoft.AspNetCore.Components.Web;
using ProtonBlazor.UnitTests.TestComponents.Switch;
using ProtonBlazor.UnitTests.Utilities;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class SwitchTest : BunitTest
    {
        [Test]
        public async Task Switch_KeyboardNavigation()
        {
            var comp = Context.Render<ProSwitchBasicTest>();
            var switchInstance = comp.FindComponent<ProSwitch<bool>>().Instance;
            IElement ProSwitch() => comp.Find("#switch");

            await comp.InvokeAsync(() => ProSwitch().KeyDownAsync(new KeyboardEventArgs { Key = "Enter", Type = "keydown", }));
            await comp.WaitForAssertionAsync(() => switchInstance.ReadValue.Should().Be(true));

            await comp.InvokeAsync(() => ProSwitch().KeyDownAsync(new KeyboardEventArgs { Key = "Delete", Type = "keydown", }));
            await comp.WaitForAssertionAsync(() => switchInstance.ReadValue.Should().Be(false));

            await comp.InvokeAsync(() => ProSwitch().KeyDownAsync(new KeyboardEventArgs { Key = "ArrowRight", Type = "keydown", }));
            await comp.WaitForAssertionAsync(() => switchInstance.ReadValue.Should().Be(true));

            await comp.InvokeAsync(() => ProSwitch().KeyDownAsync(new KeyboardEventArgs { Key = "ArrowLeft", Type = "keydown", }));
            await comp.WaitForAssertionAsync(() => switchInstance.ReadValue.Should().Be(false));

            await comp.InvokeAsync(() => ProSwitch().KeyDownAsync(new KeyboardEventArgs { Key = "NumpadEnter", Type = "keydown", }));
            await comp.WaitForAssertionAsync(() => switchInstance.ReadValue.Should().Be(true));

            await comp.InvokeAsync(() => ProSwitch().KeyDownAsync(new KeyboardEventArgs { Key = " ", Type = "keydown", }));
            await comp.WaitForAssertionAsync(() => switchInstance.ReadValue.Should().Be(false));

            await comp.InvokeAsync(() => ProSwitch().KeyDownAsync(new KeyboardEventArgs { Key = " ", Type = "keydown", }));
            await comp.WaitForAssertionAsync(() => switchInstance.ReadValue.Should().Be(true));

            await comp.SetParametersAndRenderAsync(parameters => parameters.Add(x => x.Disabled, true));
            await comp.InvokeAsync(() => ProSwitch().KeyDownAsync(new KeyboardEventArgs { Key = "ArrowLeft", Type = "keydown", }));
            await comp.WaitForAssertionAsync(() => switchInstance.ReadValue.Should().Be(true));
        }

        [Test]
        public void Switch_AriaLabel()
        {
            var comp = Context.Render<SwitchAriaLabelTest>();
            var switches = comp.FindAll(".pro-input-control-boolean-input");

            // verify switch one maintains it's original structure, no aria class used, label with a span element
            switches[0].GetElementsByClassName("pro-sr-only").Length.Should().Be(0);
            var element0 = comp.Find(".s1 label.pro-switch span.pro-typography");
            element0.HasAttribute("aria-hidden").Should().BeFalse();

            // switch two should have both a valid label with aria-hidden, an input with arialabelledby and the labelledby element
            switches[1].GetElementsByClassName("pro-sr-only").Length.Should().Be(1);
            var element1 = comp.Find(".s2 label.pro-switch span.pro-typography");
            element1.HasAttribute("aria-hidden").Should().BeTrue();
            var input1 = comp.Find(".s2 label.pro-switch input");
            var input1ForId = input1.GetAttribute("aria-labelledby");
            comp.Find($".s2 label.pro-switch #{input1ForId}").Should().NotBeNull();

            // switch three should have original structure intact, no aria class used, label with a span element for child content
            switches[2].GetElementsByClassName("pro-sr-only").Length.Should().Be(0);
            var element2 = comp.Find(".s3 label.pro-switch span.pro-typography");
            element2.HasAttribute("aria-hidden").Should().BeFalse();

            // switch four should look identical to two except this time it's with ChildContent
            switches[3].GetElementsByClassName("pro-sr-only").Length.Should().Be(1);
            var element3 = comp.Find(".s4 label.pro-switch span.pro-typography");
            element3.HasAttribute("aria-hidden").Should().BeTrue();
            var input3 = comp.Find(".s4 label.pro-switch input");
            var input3ForId = input3.GetAttribute("aria-labelledby");
            comp.Find($".s4 label.pro-switch #{input3ForId}").Should().NotBeNull();

            // switch five has no label, no child content, just arialabel
            switches[4].GetElementsByClassName("pro-sr-only").Length.Should().Be(1);
            comp.FindAll(".s5 label.pro-switch span.pro-typography").Count.Should().Be(0);
            var input4 = comp.Find(".s5 label.pro-switch input");
            var input4ForId = input4.GetAttribute("aria-labelledby");
            comp.Find($".s5 label.pro-switch #{input4ForId}").Should().NotBeNull();
        }

        [Test]
        [TestCase(Color.Default, Color.Primary)]
        [TestCase(Color.Primary, Color.Secondary)]
        [TestCase(Color.Secondary, Color.Info)]
        [TestCase(Color.Tertiary, Color.Success)]
        [TestCase(Color.Info, Color.Warning)]
        [TestCase(Color.Success, Color.Error)]
        [TestCase(Color.Warning, Color.Dark)]
        [TestCase(Color.Error, Color.Primary)]
        [TestCase(Color.Dark, Color.Primary)]
        public async Task SwitchColor(Color color, Color uncheckedcolor)
        {
            var comp = Context.Render<ProSwitch<bool>>(x => x.Add(c => c.Color, color).Add(b => b.UncheckedColor, uncheckedcolor));

            var box = comp.Instance;

            var checkboxClasses = comp.Find(".pro-button-root.pro-icon-button.pro-switch-base");
            // check initial state
            box.ReadValue.Should().Be(false);
            checkboxClasses.ClassList.Should().ContainInOrder(new[] { $"pro-{uncheckedcolor.ToStringFast(true)}-text", $"hover:pro-{uncheckedcolor.ToStringFast(true)}-hover" });

            // click and check if it has new color
            await comp.Find("input").ChangeAsync(true);
            box.ReadValue.Should().Be(true);
            checkboxClasses.ClassList.Should().ContainInOrder(new[] { $"pro-{color.ToStringFast(true)}-text", $"hover:pro-{color.ToStringFast(true)}-hover" });
        }

        [Test]
        public void SwitchDisabled()
        {
            var comp = Context.Render<SwitchWithLabelTest>();
            comp.FindAll("label.pro-switch")[3].ClassList.Should().Contain("pro-disabled"); // 4rd switch
        }

        [Test]
        public void SwitchLabelPlacement()
        {
            var comp = Context.Render<SwitchWithLabelTest>();

            comp.FindAll("label.pro-switch")[0].ClassList.Should().Contain("pro-input-content-placement-end"); // 1st switch: (default) Placement.End
            comp.FindAll("label.pro-switch")[2].ClassList.Should().Contain("pro-input-content-placement-start"); // 3rd switch: Placement.Start
        }

        [Test]
        public void SwitchLabel()
        {
            var value = new DisplayNameLabelClass();

            var comp = Context.Render<ProSwitch<bool>>(x => x.Add(f => f.For, () => value.Boolean));
            comp.Instance.Label.Should().Be("Boolean LabelAttribute"); //label should be set by the attribute

            var comp2 = Context.Render<ProSwitch<bool>>(x => x.Add(f => f.For, () => value.Boolean).Add(l => l.Label, "Label Parameter"));
            comp2.Instance.Label.Should().Be("Label Parameter"); //existing label should remain
        }

        /// <summary>
        /// Optional Switch should not have required attribute should be false.
        /// </summary>
        [Test]
        public void OptionalSwitch_Should_NotHaveRequiredAttribute()
        {
            var comp = Context.Render<ProSwitch<bool>>();

            comp.Find("input").HasAttribute("required").Should().BeFalse();
        }

        /// <summary>
        /// Required Switch should have the required attribute.
        /// </summary>
        [Test]
        public void RequiredSwitch_Should_HaveRequiredAttribute()
        {
            var comp = Context.Render<ProSwitch<bool>>(parameters => parameters
                .Add(p => p.Required, true));

            comp.Find("input").HasAttribute("required").Should().BeTrue();
        }

        /// <summary>
        /// Required Switch attribute should be dynamic.
        /// </summary>
        [Test]
        public async Task RequiredSwitchAttribute_Should_BeDynamic()
        {
            var comp = Context.Render<ProSwitch<bool>>();

            comp.Find("input").HasAttribute("required").Should().BeFalse();

            await comp.SetParametersAndRenderAsync(parameters => parameters
                .Add(p => p.Required, true));

            comp.Find("input").HasAttribute("required").Should().BeTrue();
        }

        [Test]
        public void Switch_Respects_Custom_TabIndex()
        {
            var comp = Context.Render<ProSwitch<bool>>(parameters => parameters.AddUnmatched("tabindex", "-1"));

            comp.Find("input").GetAttribute("tabindex").Should().Be("-1");
        }

        [Test]
        public void Switch_Uses_Default_TabIndex_When_Enabled()
        {
            var comp = Context.Render<ProSwitch<bool>>();

            comp.Find("input").GetAttribute("tabindex").Should().Be("0");
        }

        [Test]
        public void Switch_Uses_Default_TabIndex_When_Disabled()
        {
            var comp = Context.Render<ProSwitch<bool>>(parameters => parameters.Add(x => x.Disabled, true));

            comp.Find("input").GetAttribute("tabindex").Should().Be("-1");
        }

        [Test]
        public void Switch_Respects_Custom_TabIndex_CaseInsensitive()
        {
            var comp = Context.Render<ProSwitch<bool>>(parameters => parameters.AddUnmatched("TabIndex", "-1"));

            comp.Find("input").GetAttribute("tabindex").Should().Be("-1");
        }

        [Test]
        [TestCase(true, "true")]
        [TestCase(false, "false")]
        [TestCase(null, "mixed")]
        public void Switch_AriaChecked_Reflects_Value(bool? value, string expectedAriaChecked)
        {
            var comp = Context.Render<ProSwitch<bool?>>(parameters => parameters.Add(x => x.Value, value));
            var input = comp.Find("input");

            input.GetAttribute("aria-checked").Should().Be(expectedAriaChecked);
        }

        [Test]
        public void ReadOnlyDisabled_ShouldNot_Hover()
        {
            Context.Render<ProSwitch<bool>>(self => self.Add(x => x.ReadOnly, false)).Find("span.pro-button-root").ClassList.Should().Contain("hover:pro-default-hover");
            Context.Render<ProSwitch<bool>>(self => self.Add(x => x.ReadOnly, true)).Find("span.pro-button-root").ClassList.Should().NotContain("hover:pro-default-hover");
            Context.Render<ProSwitch<bool>>(self => self.Add(x => x.ReadOnly, true).Add(x => x.Disabled, false)).Find("span.pro-button-root").ClassList.Should().NotContain("hover:pro-default-hover");
            Context.Render<ProSwitch<bool>>(self => self.Add(x => x.Disabled, false)).Find("span.pro-button-root").ClassList.Should().Contain("hover:pro-default-hover");
            Context.Render<ProSwitch<bool>>(self => self.Add(x => x.Disabled, true).Add(x => x.ReadOnly, false)).Find("span.pro-button-root").ClassList.Should().NotContain("hover:pro-default-hover");
            Context.Render<ProSwitch<bool>>(self => self.Add(x => x.Disabled, true).Add(x => x.ReadOnly, true)).Find("span.pro-button-root").ClassList.Should().NotContain("hover:pro-default-hover");
        }
    }
}
