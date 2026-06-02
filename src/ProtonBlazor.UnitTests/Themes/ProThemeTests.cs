// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AwesomeAssertions;
using NUnit.Framework;
using ProtonBlazor.UnitTests.Dummy;

namespace ProtonBlazor.UnitTests.Themes;

#nullable enable
[TestFixture]
public class ProThemeTests
{
    [Test]
    public void ProTheme_STJ_SourceGen_Serialization()
    {
        var originalMudTheme = new ProTheme
        {
            ZIndex = new ZIndex
            {
                Drawer = 5000
            }
        };

        var mudThemeType = typeof(ProTheme);
        var context = ProThemeSerializerContext.Default;

        var jsonString = System.Text.Json.JsonSerializer.Serialize(originalMudTheme, mudThemeType, context);
        var deserializeMudTheme = (ProTheme)System.Text.Json.JsonSerializer.Deserialize(jsonString, mudThemeType, context)!;

        deserializeMudTheme.ZIndex.Drawer.Should().Be(originalMudTheme.ZIndex.Drawer);
        deserializeMudTheme.Should().NotBeSameAs(originalMudTheme, "Objects have same values, but instances are different and has on custom Equals");
    }
}
