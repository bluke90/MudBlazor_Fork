// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using Bunit;
using NUnit.Framework;
using ProtonBlazor.UnitTests.TestComponents.Utilities;

namespace ProtonBlazor.UnitTests.Converters;

#nullable enable
[TestFixture]
public class ConverterTests : BunitTest
{
    [Test]
    public async Task Converter_ShouldUseAppropriateConverterBasedOnParameterValue()
    {
        var comp = Context.Render<ConverterCompTest>(parameters => parameters.Add(x => x.Converter, null));
        var numericComp = comp.FindComponent<ProNumericField<int>>();
        numericComp.Instance.Converter.Should().BeNull();
        numericComp.Instance.GetConverter().Should().BeOfType<DefaultConverter<int>>();

        await comp.SetParametersAndRenderAsync(parameters => parameters.Add(x => x.Converter, new DeferredConverter<int, string?>()));

        numericComp.Instance.Converter.Should().BeOfType<DeferredConverter<int, string?>>();
        numericComp.Instance.GetConverter().Should().BeOfType<DeferredConverter<int, string?>>();

        await comp.SetParametersAndRenderAsync(parameters => parameters.Add(x => x.Converter, null));
        numericComp.Instance.Converter.Should().BeNull();
        numericComp.Instance.GetConverter().Should().BeOfType<DefaultConverter<int>>();
    }
}
