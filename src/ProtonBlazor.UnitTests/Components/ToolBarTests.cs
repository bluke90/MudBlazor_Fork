// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using Bunit;
using NUnit.Framework;
using ProtonBlazor.UnitTests.TestComponents.ToolBar;

namespace ProtonBlazor.UnitTests.Components
{
    [TestFixture]
    public class ToolBarTests : BunitTest
    {
        [Test]
        public void ToolBarWrapContent()
        {
            var component = Context.Render<ToolBarWrapContentTest>();
            var mudToolBar = component.Find(".pro-toolbar");

            mudToolBar.ClassList.Should().Contain("pro-toolbar-wrap-content");
        }

        /// <summary>
        /// ToolBar's WrapContent should be false by default
        /// </summary>
        [Test]
        public void ToolBar_WrapContent_ShouldBeFalseByDefault()
        {
            var comp = Context.Render<ProToolBar>();
            comp.Instance.WrapContent.Should().Be(false);
        }
    }
}
