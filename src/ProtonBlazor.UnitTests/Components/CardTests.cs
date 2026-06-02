// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using Bunit;
using ProtonBlazor.UnitTests.TestComponents.Card;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class CardTests : BunitTest
    {
        [Test]
        public async Task CardChildContent()
        {
            //Card header with child content should be render successfully
            var comp = Context.Render<CardChildContentTest>();
            var button = comp.FindComponent<ProButton>();
            var numeric = comp.FindComponent<ProNumericField<int>>();
            await comp.WaitForAssertionAsync(() => numeric.Instance.Value.Should().Be(0));
            await comp.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
            await comp.WaitForAssertionAsync(() => numeric.Instance.Value.Should().Be(1));
        }
    }
}
