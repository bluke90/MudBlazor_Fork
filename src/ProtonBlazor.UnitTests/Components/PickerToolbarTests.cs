// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using Bunit;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components;

[TestFixture]
public class PickerToolbarTests : BunitTest
{
    [Test]
    public void PickerToolbar_ShouldBeLandscape_WhenStaticAndOrientationLandscape()
    {
        var component = Context.Render<ProPickerToolbar>(parameters => parameters
            .Add(p => p.PickerVariant, PickerVariant.Static)
            .Add(p => p.Orientation, Orientation.Landscape));

        var pickerToolbar = component.Instance;
        component.FindAll(".pro-picker-toolbar-landscape").Count.Should().Be(1);
    }

    [Test]
    [TestCase(PickerVariant.Inline)]
    [TestCase(PickerVariant.Dialog)]
    public void PickerToolbar_ShouldNotBeLandscape_WhenNonStaticAndOrientationLandscape(PickerVariant pickerVariant)
    {
        var component = Context.Render<ProPickerToolbar>(parameters => parameters
            .Add(p => p.PickerVariant, pickerVariant)
            .Add(p => p.Orientation, Orientation.Landscape));

        var pickerToolbar = component.Instance;
        component.FindAll(".pro-picker-toolbar-landscape").Count.Should().Be(0);
    }
}
